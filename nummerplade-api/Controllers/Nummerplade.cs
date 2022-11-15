﻿using Microsoft.AspNetCore.Mvc;
using nummerplade_api.Classes; 

namespace nummerplade_api.Controllers;

using System.Text.Json;

// Import HTML agility
using HtmlAgilityPack;
using Newtonsoft.Json;
using nummerplade_api.Functions;
using nummerplade_api.Model;

[ApiController]
[Route("/api/insurance")]
public class NummerpladeController : ControllerBase
{

    // Init misc functions
    MiscFunctions miscFunctions = new MiscFunctions();

    // Init mailservice
    MailService mailService = new MailService();

    // DbContext
    private NummerpladeApiContext db;

    // Initialization function
    public NummerpladeController(NummerpladeApiContext context)
    {
        db = context;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Index()
    {

        // Return
        return $"You need to supply either registration or VIN!";

    }

    /// <summary>
    /// Return insurance information regarding supplied registration.
    /// </summary>
    /// <param name="nrplade"></param>
    /// <returns></returns>
    [HttpPost("plade/{nrplade}")]
    public async Task<IActionResult> GetInsuranceFromRegistration(string nrplade)
    {
        // Iitiate object to return at end of code block
        InsuranceReturn returnObject = new InsuranceReturn();

        // Check if plate is 7 characters long
        if (nrplade.Length != 7)
        {
            returnObject.success = false;
            returnObject.status = "Incorrect plate length!";
            return Ok(new { returnObject, Insurance = new Insurance(), General = new General() });
        }

        // Init HTTPClient
        var httpClient = new HttpClient();

        // Download
        string carId = (await httpClient.GetFromJsonAsync<dmr_data>($"https://www.tjekbil.dk/api/v3/dmr/regnr/{nrplade}")).debtData.carId;

        // Get insurance
        var response = (await httpClient.GetFromJsonAsync<Extended>($"https://www.tjekbil.dk/api/v3/dmr/kid/{carId}/extendednew"));
        returnObject.Insurance = response.insurance;
        returnObject.General = response.general;

        // Empty list
        returnObject.Insurance.historik = new List<Historik>();

        if (returnObject.Insurance is null) {
            return NotFound();
        }

        // Check if this vehicle is police-owned
        if (returnObject.Insurance.selskab == "SELVFORSIKRING" || true)
        {
            // Update object
            returnObject.is_police_vehicle = true;
            returnObject.status = "Vehicle is owned by the police.";
            returnObject.success = true;

            // Create new spotting
            Spotting spot = new Spotting()
            {
                Nrplade = nrplade,
                Location = "This is the location",
                ValidUntilDate = DateTime.Now.AddHours(3)
            };

            // Update db
            db.Spottings.Add(spot);
            db.SaveChanges();

            // Send mail
            mailService.sendMail("Politi er spottet i nærheden!", $"Der er spottet politi i nærheden af dig! Nummerplade: {nrplade}", "jxras11@hotmail.com", "Jonas Rasmussen");

        }
        else
        {
            // Update object
            returnObject.is_police_vehicle = false;
            returnObject.status = "Vehicle is not owned by the police.";
            returnObject.success = true;
        }

        // Return
        return Ok(new { returnObject.Insurance, returnObject.General, returnObject.is_police_vehicle, returnObject.status, returnObject.success });

    }

}

