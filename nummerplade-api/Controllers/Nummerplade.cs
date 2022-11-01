using Microsoft.AspNetCore.Mvc;
using nummerplade_api.Classes;

namespace nummerplade_api.Controllers;

// Import HTML agility
using HtmlAgilityPack;
using nummerplade_api.Functions;
using OpenQA.Selenium.DevTools.V104.Runtime;

[ApiController]
[Route("/api/forsikring")]
public class NummerpladeController : ControllerBase
{

    // Init misc functions
    MiscFunctions miscFunctions = new MiscFunctions();

    [HttpGet]
    public async Task<ActionResult<string>> Index()
    {

        // Return
        return $"Du skal indtaste nummerplade eller VIN!";

    }

    [HttpGet("plade/{nrplade}")]
    public async Task<ActionResult<string>> GetForsikringFromPlade(string nrplade)
    {

        // Init HTTPClient
        var httpClient = new HttpClient();

        // Download
        string carId = (await httpClient.GetFromJsonAsync<dmr_data>("https://www.tjekbil.dk/api/v3/dmr/regnr/DM44459")).debtData.carId;

        // Get insurance
        Insurance insurance = (await httpClient.GetFromJsonAsync<extendednew>($"https://www.tjekbil.dk/api/v3/dmr/kid/{carId}/extendednew")).insurance;

        // Return
        return $"Du søger efter køretøj med reg. nummer: {nrplade} - Med forsikring {insurance.selskab} - oprettet d. {insurance.oprettet}";

    }

}

