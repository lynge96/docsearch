using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadbalancerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoadbalancerAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LoadbalancerController : Controller
{
    private readonly ILogger<LoadbalancerController> _logger;
    private readonly ILoadbalancer _loadbalancer;

    public LoadbalancerController(ILogger<LoadbalancerController> logger, ILoadbalancer loadbalancer)
    {
        _logger = logger;
        _loadbalancer = loadbalancer;
    }

    [HttpGet(Name = "NextEndpoint")]
    public async Task<ActionResult<string>> GetNextEndpointAsync(string username)
    {
        var endpoint = _loadbalancer.NextEndpoint(username);

        if (endpoint == null)
        {
            _logger.LogError("No endpoints were found: {endpoint}", endpoint);

            return NotFound("No SearchAPI endpoints was available");
        }

        _logger.LogInformation("Returning next API endpoint: {endpoint}", endpoint);

        return Ok(endpoint);
    }

    [HttpGet(Name = "GetAllEndpoints")]
    public async Task<ActionResult<Dictionary<string, string>>> GetAllEndpointsAsync()
    {
        var endpoints = _loadbalancer.AllEndpoints();

        if (endpoints == null || endpoints.Count == 0)
        {
            _logger.LogError("No endpoints were found: {endpoints}", endpoints.Count);

            return NotFound("No SearchAPI endpoints was available");
        }

        _logger.LogInformation("Returning all available API endpoints {endpoints}", string.Join(", ", endpoints.Select(x => x.Key + ": " + x.Value).ToList()));       
        
        return Ok(endpoints);
    }

}
