
// Import HTML agility
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using nummerplade_api.Functions;
using nummerplade_api.Model;

[ApiController]
[Route("/api")]
public class PingApiController : ControllerBase
{

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        // Return ping
        return Ok(new {success = true, status = "Hello world!"});
    }

}