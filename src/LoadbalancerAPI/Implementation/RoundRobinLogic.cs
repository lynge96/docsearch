using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class RoundRobinLogic : ILoadbalancer
{
    private static int _next;

    public RoundRobinLogic()
    {
        _next = 0;
    }

    public async Task<string?> NextEndpoint()
    {
        var endpoints = GetEndpoints();

        if (endpoints == null || endpoints.Count == 0)
        {
            return null;
        }

        // Cycle through Endpoints in round-robin fashion
        _next = (_next + 1) % endpoints.Count;

        return endpoints[_next];
    }

    public async Task<List<string>> AllEndpoints()
    {
        return GetEndpoints();
    }

    private List<string> GetEndpoints()
    {
        return StartupService.Endpoints;
    }

}