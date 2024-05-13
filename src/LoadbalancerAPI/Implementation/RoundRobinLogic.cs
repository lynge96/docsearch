using System.Collections.Generic;
using System.Linq;
using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class RoundRobinLogic : ILoadbalancer
{
    private static int _next;

    public RoundRobinLogic()
    {
        _next = 0;
    }

    public string? NextEndpoint(string? username)
    {
        var endpoints = StartupService.Endpoints;

        if (endpoints == null || endpoints.Count == 0)
        {
            return null;
        }

        // Cycle through Endpoints in round-robin fashion
        _next = (_next + 1) % endpoints.Count;

        return endpoints.ElementAt(_next).Value;
    }

    public Dictionary<string, string>? AllEndpoints()
    {
        if (StartupService.Endpoints == null)
        {
            return null;
        }

        return StartupService.Endpoints;
    }
}