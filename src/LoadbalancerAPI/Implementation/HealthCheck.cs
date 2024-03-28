using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class HealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;
    private readonly List<string> _endpoints;

    public HealthCheck()
    {
        _endpoints = new List<string>
        {
            "https://localhost:7233",
            "https://localhost:7234"
        };
        _httpClient = new HttpClient();
    }

    public async Task<List<string>> CheckServers()
    {
        var availableServers = new List<string>();

        foreach (var serverUrl in _endpoints)
        {
            try
            {
                await _httpClient.GetAsync($"{serverUrl}/api/Health/CheckHealth");

                availableServers.Add(serverUrl);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error checking server {serverUrl}: {ex.Message}");
            }
        }
    
        return availableServers;
    }
}
