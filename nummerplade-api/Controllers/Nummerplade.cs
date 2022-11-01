using Microsoft.AspNetCore.Mvc;

namespace nummerplade_api.Controllers;

// Import HTML agility
using HtmlAgilityPack;

[ApiController]
[Route("/api/forsikring")]
public class NummerpladeController : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<string>> Index()
    {

        // Return
        return $"Du skal indtaste nummerplade eller VIN!";

    }

    [HttpGet("plade/{nrplade}")]
    public async Task<ActionResult<string>> GetForsikringFromPlade(string nrplade)
    {

        // Init HTTPScraper
        var httpClient = new HttpClient();

        // Download
        var responseHtml = await httpClient.GetStringAsync(await nummerpladeNetUrl(nrplade));

        // Parse html
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(responseHtml);

        // Find forsikring navn
        var forsikringNavn = htmlDoc.GetElementbyId("forsikring-selskab").InnerHtml;

        // Return
        return $"Du søger efter køretøj med reg. nummer: {nrplade} - Med forsikring {forsikringNavn}";

    }

    public async Task<string> nummerpladeNetUrl(string nrplade)
    {

        return $"https://www.nummerplade.net/nummerplade/{nrplade}.html";

    }

}

