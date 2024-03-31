using LoadbalancerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<string>> GetNextEndpointAsync()
    {
        var endpoint = await _loadbalancer.NextEndpoint();

        if (endpoint == null)
        {
            _logger.LogError("No endpoints were found: {endpoint}", endpoint);

            return NotFound("No SearchAPI endpoints was available");
        }

        _logger.LogInformation("Returning next API endpoint: {endpoint}", endpoint);

        return Ok(endpoint);
    }

    [HttpGet(Name = "GetAllEndpoints")]
    public async Task<ActionResult<List<string>>> GetAllEndpointsAsync()
    {
        var endpoints = await _loadbalancer.AllEndpoints();

        if (endpoints == null || endpoints.Count == 0)
        {
            _logger.LogError("No endpoints were found: {endpoints}", endpoints);

            return NotFound("No SearchAPI endpoints was available");
        }

        _logger.LogInformation("Returning all available API endpoints {endpoints}", endpoints);

        return Ok(endpoints);
    }

}
