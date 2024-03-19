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

    [HttpGet(Name = "GetNextEndpoint")]
    public string GetNextEndpoint()
    {
        var endpoint = _loadbalancer.GetNextEndpoint();

        return endpoint;
    }

    [HttpGet(Name = "GetAllEndpoints")]
    public List<string> GetAllEndpoints()
    {
        var endpoints = _loadbalancer.GetAllEndpoints();

        return endpoints;
    }


}
