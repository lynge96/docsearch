using System.Collections.Generic;
using System.Linq;
using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class DatabaseSplitLogic : ILoadbalancer
{
    private static int _next;

    public DatabaseSplitLogic()
    {
        _next = 0;
    }

    public string? NextEndpoint(string username)
    {
        var availableEndpoints = StartupService.Endpoints;

        if (availableEndpoints.TryGetValue(username, out string endpoint))
        {
            return endpoint;
        }
        else
        {
            return null;
        }
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