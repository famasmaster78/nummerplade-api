using Microsoft.AspNetCore.Mvc;
using nummerplade_api.Classes; 

namespace nummerplade_api.Controllers;

using System.Text.Json;

// Import HTML agility
using HtmlAgilityPack;
using Newtonsoft.Json;
using nummerplade_api.Functions;

[ApiController]
[Route("/api/insurance")]
public class NummerpladeController : ControllerBase
{

    // Init misc functions
    MiscFunctions miscFunctions = new MiscFunctions();

    [HttpGet]
    public async Task<ActionResult<string>> Index()
    {

        // Return
        return $"You need to supply eiter registration or VIN!";

    }

    /// <summary>
    /// Return insurance information regarding supplied registration.
    /// </summary>
    /// <param name="nrplade"></param>
    /// <returns></returns>
    [HttpGet("plade/{nrplade}")]
    public async Task<IActionResult> GetInsuranceFromRegistration(string nrplade)
    {

        InsuranceReturn returnObject = new InsuranceReturn();

        // Check if plate is 7 characters long
        if (nrplade.Length != 7)
        {
            returnObject.success = false;
            returnObject.status = "Incorrect plate length!";
            return Ok(new { status = returnObject.status, success = returnObject.success, is_police_vehicle = returnObject.is_police_vehicle, insurance = new Insurance() });
        }

        // Init HTTPClient
        var httpClient = new HttpClient();

        // Download
        string carId = (await httpClient.GetFromJsonAsync<dmr_data>($"https://www.tjekbil.dk/api/v3/dmr/regnr/{nrplade}")).debtData.carId;

        // Get insurance
        returnObject.Insurance = (await httpClient.GetFromJsonAsync<Extended>($"https://www.tjekbil.dk/api/v3/dmr/kid/{carId}/extendednew")).insurance;

        // Empty list
        returnObject.Insurance.historik = new List<Historik>();

        if (returnObject.Insurance is null) {
            return NotFound();
        }

        // Check if this vehicle is police-owned
        if (returnObject.Insurance.selskab == "SELVFORSIKRING")
        {
            // Update object
            returnObject.is_police_vehicle = true;
            returnObject.status = "Vehicle is owned by the police.";
            returnObject.success = true;
        }else
        {
            // Update object
            returnObject.is_police_vehicle = false;
            returnObject.status = "Vehicle is not owned by the police.";
            returnObject.success = true;
        }

        // Return
        return Ok(new { status = returnObject.status, success = returnObject.success, is_police_vehicle = returnObject.is_police_vehicle, insurance = returnObject.Insurance });

    }

}

