using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadbalancerAPI.Interfaces;

namespace LoadbalancerAPI.Implementation;

public class HealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, string> _endpoints;

    public HealthCheck()
    {
        _endpoints = new Dictionary<string, string>
        {
            {"Allen", "https://localhost:7233"},
            {"Arnold", "https://localhost:7234"}
        };
        _httpClient = new HttpClient();
    }

    public async Task<Dictionary<string, string>> CheckServers()
    {
        var availableServers = new Dictionary<string, string>();

        foreach (var serverUrl in _endpoints)
        {
            try
            {
                await _httpClient.GetAsync($"{serverUrl.Value}/api/Health/CheckHealth");

                availableServers.Add(serverUrl.Key, serverUrl.Value);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error checking server {serverUrl}: {ex.Message}");
            }
        }
    
        return availableServers;
    }
}
