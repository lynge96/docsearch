using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class RoundRobinLogic : ILoadbalancer
{
    private readonly List<string> _endpoints;
    private int _next;
    private DateTime _lastRedirectTime;

    public RoundRobinLogic()
    {
        _endpoints = new List<string>
        {
            "https://localhost:7233",
            "https://localhost:7234"
        };

        _next = 0;
        _lastRedirectTime = DateTime.MinValue;
    }

    public string GetNextEndpoint()
    {
        // Check if 2 seconds have passed since the last redirect
        if ((DateTime.Now - _lastRedirectTime).TotalSeconds >= 2)
        {
            _next = 0;
            _lastRedirectTime = DateTime.Now;
        }
        else
        {
            _next = (_next + 1) % _endpoints.Count;
        }

        return _endpoints[_next];
    }

    public List<string> GetAllEndpoints()
    {
        return _endpoints;
    }

    public void CheckAvailableServers()
    {
        // TODO: Logic for server check
        throw new NotImplementedException();
    }
}
