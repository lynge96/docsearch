using Loadbalancer;

public class LoadBalancer : ILoadBalancer
{
    private readonly List<string> _endpoints;
    private int _currentIndex;
    private DateTime _lastRedirectTime;

    public LoadBalancer()
    {
        _endpoints = new List<string>
        {
            "https://localhost:7233",
            "https://localhost:7234"
        };

        _currentIndex = 0;
        _lastRedirectTime = DateTime.MinValue;
    }

    public string GetNextEndpoint()
    {
        // Check if 2 seconds have passed since the last redirect
        if ((DateTime.Now - _lastRedirectTime).TotalSeconds >= 2)
        {
            _currentIndex = 0;
            _lastRedirectTime = DateTime.Now;
        }
        else
        {
            _currentIndex = (_currentIndex + 1) % _endpoints.Count;
        }

        return _endpoints[_currentIndex];
    }

    public List<string> GetAllEndpoints()
    {
        return _endpoints;
    }
}