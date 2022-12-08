using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Return insurance information regarding supplied registration.
    /// </summary>
    /// <param name="nrplade"></param>
    /// <returns></returns>
    [HttpPost("plade/{nrplade}")]
    public async Task<IActionResult> GetInsuranceFromRegistration(string nrplade, [FromBody]ApiPladePost body)
    {

        // Iitiate object to return at end of code block
        InsuranceReturn returnObject = new InsuranceReturn();

        // Check body
        if (body.Email is null || body.Location is null)
        {
            returnObject.success = false;
            returnObject.status = $"All parameters are not fulfilled!";
            return BadRequest(new {success = returnObject.success, status = returnObject.status});
        }

        // Bool to check if plate is found in any police-vehicle source
        bool policeCarFound = false;

        // Check if plate is 7 characters long
        if (nrplade.Length != 7)
        {
            returnObject.success = false;
            returnObject.status = "Incorrect plate length!";
            return BadRequest(new { status = returnObject.status, success = returnObject.success });
        }

        // Validate email
        if (!miscFunctions.validateEmail(body.Email))
        {
            returnObject.success = false;
            returnObject.status = "Invalid email supplied!";
            return BadRequest(new { status = returnObject.status, success = returnObject.success });
        }

        // Check if car exists
        if (!(await miscFunctions.CarExists(nrplade)))
        {
            return NotFound(new {status = "Car not found", success = false});
        }

        // Get information about car
        var response = await miscFunctions.GetCarInformation(nrplade);

        returnObject.Insurance = response.insurance;
        returnObject.General = response.general;

        // Empty list
        returnObject.Insurance.historik = new List<Historik>();

        if (returnObject.Insurance is null)
        {
            return NotFound(new {status = "Insurance not found", success = false});
        }

        if (returnObject.Insurance.selskab == "SELVFORSIKRING")
        {
            // Update boolean
            policeCarFound = true;

            // Add new car to policeCars table
            // Create new police car
            Policecar car = new Policecar()
            {
                Nrplade = nrplade
            };

            // Add to db
            if (db.Policecars.Any(e => e.Nrplade == nrplade))
            {
                // Add car
                db.Policecars.Add(car);
            }


        }

        // Check if this vehicle is police-owned
        if (policeCarFound)
        {
            // Update object
            returnObject.is_police_vehicle = true;
            returnObject.status = "Vehicle is owned by the police.";
            returnObject.success = true;

            // Create new spotting
            Spotting spot = new Spotting()
            {
                Nrplade = nrplade,
                LocationX = "X coordinate",
                LocationY = "Y coordinate",
                ValidUntilDate = DateTime.Now.AddHours(3)
            };

            // Update db
            db.Spottings.Add(spot);

            // Send mail
            mailService.sendMail("Politi er spottet i nærheden!", $"Der er spottet politi i nærheden af dig! Nummerplade: {nrplade}", body.Email, "Jonas Rasmussen");

        }
        else
        {
            // Update object
            returnObject.is_police_vehicle = false;
            returnObject.status = "Vehicle is not owned by the police.";
            returnObject.success = true;
        }

        // Save changes made to db
        db.SaveChanges();

        // Return
        return Ok(new { returnObject.Insurance, returnObject.General, returnObject.is_police_vehicle, returnObject.status, returnObject.success });

    }

}

