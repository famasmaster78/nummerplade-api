using Microsoft.AspNetCore.Mvc;
using nummerplade_api.Classes; 

namespace nummerplade_api.Controllers;

// Import HTML agility
using HtmlAgilityPack;
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
            return Ok(returnObject);
        }

        // Init HTTPClient
        var httpClient = new HttpClient();

        // Download
        string carId = (await httpClient.GetFromJsonAsync<dmr_data>("https://www.tjekbil.dk/api/v3/dmr/regnr/DM44459")).debtData.carId;

        // Get insurance
        Insurance insurance = (await httpClient.GetFromJsonAsync<extendednew>($"https://www.tjekbil.dk/api/v3/dmr/kid/{carId}/extendednew")).insurance;
        returnObject.Insurance = insurance;

        // Check if this vehicle is police-owned
        if (insurance is not null && insurance.selskab == "SELVFORSIKRING")
        {
            returnObject.is_police_vehicle = true;
            returnObject.status = "Vehicle is owned by police.";
            returnObject.success = true;
        }

        // Return
        return Ok(returnObject);

    }

}

