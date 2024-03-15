namespace Loadbalancer;

public class LoadBalancer : ILoadBalancer
{
    private readonly List<string> _endpoints;
    private int _currentIndex;

    public LoadBalancer()
    {
        _endpoints = new List<string>
        {
            "https://localhost:7233",
            "https://localhost:7234"
        };

        _currentIndex = 0;
    }

    public string GetNextEndpoint()
    {
        // Cycle through endpoints in round-robin fashion
        _currentIndex = (_currentIndex + 1) % _endpoints.Count;
        return _endpoints[_currentIndex];
    }

    public List<string> GetAllEndpoints()
    {
        return _endpoints;
    }
}
