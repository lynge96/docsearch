using System.Net.Http.Json;
using Application.Interfaces;
using Core.DTOs;
using Loadbalancer;

namespace Application.Services;

public class SettingsService : ISettingsService
{
    private readonly ILoadBalancer _loadBalancer;

    public SettingsService(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    public async Task<AdvancedSettingsDTO> GetAdvancedSettings()
    {
        try
        {
            var httpClients = GetHttpClients();

            var resultDTOs = new List<AdvancedSettingsDTO>();

            foreach (var client in httpClients)
            {
                var response = await client.GetAsync("api/Settings/GetSettings");

                // response.EnsureSuccessStatusCode();

                var resultDTO = await response.Content.ReadFromJsonAsync<AdvancedSettingsDTO>();

                resultDTOs.Add(resultDTO);
            }

            FlushHttpClients(httpClients);

            return resultDTOs.FirstOrDefault();
        }
        catch (Exception e)
        {
            throw new Exception("Error while calling the API", e);
        }
    }

    public async Task SetSearchResults(int? noOfResults)
    {
        var httpClients = GetHttpClients();

        if (noOfResults <= 0)
        {
            throw new ArgumentException("Number of results must be a positive integer.", nameof(noOfResults));
        }

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/SetNoOfResults?noOfResults={noOfResults}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    public async Task ToggleCaseSensitive(bool state)
    {
        var httpClients = GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleCaseSensitive?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    public async Task ToggleTimeStamps(bool state)
    {
        var httpClients = GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleTimeStamps?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    private List<HttpClient> GetHttpClients()
    {
        var allEndpoints = _loadBalancer.GetAllEndpoints();

        var httpClients = new List<HttpClient>();

        foreach (var endpoint in allEndpoints)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            httpClients.Add(client);
        }

        return httpClients;
    }

    private void FlushHttpClients(IEnumerable<HttpClient> httpClients)
    {
        foreach (var client in httpClients)
        {
            client.Dispose();
        }
    }
}

