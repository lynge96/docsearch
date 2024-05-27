using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadbalancerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// Denne fil indeholder implementeringen af LoadbalancerController-klassen, som er ansvarlig for
// at håndtere anmodninger relateret til load balancer-funktionalitet. Controlleren tillader
// klienter at hente næste tilgængelige API-endepunkt eller alle tilgængelige endepunkter.


namespace LoadbalancerAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class LoadbalancerController : Controller
{
    private readonly ILogger<LoadbalancerController> _logger;
    private readonly ILoadbalancer _loadbalancer;

    // Constructor der injicerer en logger og en ILoadbalancer-implementering
    public LoadbalancerController(ILogger<LoadbalancerController> logger, ILoadbalancer loadbalancer)
    {
        _logger = logger;
        _loadbalancer = loadbalancer;
    }

    // Handler for HTTP GET-anmodning til at hente næste API-endepunkt
    [HttpGet(Name = "NextEndpoint")]
    //Den returnerer en Task af type ActionResult<string>, hvilket indikerer, at den kan udføres asynkront og returnere et resultat af typen string.
    public async Task<ActionResult<string>> GetNextEndpointAsync(string username)
    {
        // Henter det næste endepunkt baseret på brugernavnet
        var endpoint = _loadbalancer.NextEndpoint(username);

        // Hvis der ikke blev fundet noget endepunkt, returneres en fejlmeddelelse
        if (endpoint == null)
        {
            _logger.LogError("No endpoints were found: {endpoint}", endpoint);

            return NotFound("No SearchAPI endpoints was available");
        }

        // Logning af det næste API-endepunkt, der skal returneres
        _logger.LogInformation("Returning next API endpoint: {endpoint}", endpoint);

        // Returnerer det næste endepunkt
        return Ok(endpoint);
    }


    // Handler for HTTP GET-anmodning til at hente alle tilgængelige API-endepunkter
    [HttpGet(Name = "GetAllEndpoints")]
    //Den returnerer en Task af typen ActionResult<Dictionary<string, string>>, hvilket indikerer, at den kan udføres asynkront og returnere et resultat i form af en ordbog, der mapper brugernavne til endepunkts-URL'er.
    public async Task<ActionResult<Dictionary<string, string>>> GetAllEndpointsAsync()
    {
        // Henter alle tilgængelige API-endepunkter
        var endpoints = _loadbalancer.AllEndpoints();

        // Hvis der ikke blev fundet nogen endepunkter, returneres en fejlmeddelelse
        if (endpoints == null || endpoints.Count == 0)
        {
            _logger.LogError("No endpoints were found: {endpoints}", endpoints.Count);

            return NotFound("No SearchAPI endpoints was available");
        }

        // Logning af alle tilgængelige API-endepunkter
        _logger.LogInformation("Returning all available API endpoints {endpoints}", string.Join(", ", endpoints.Select(x => x.Key + ": " + x.Value).ToList()));

        // Returnerer alle tilgængelige API-endepunkter
        return Ok(endpoints);
    }

}
