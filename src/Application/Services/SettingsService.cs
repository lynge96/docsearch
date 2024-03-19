using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

namespace Application.Services;

public class SettingsService : ISettingsService
{
    private readonly HttpClient _client;

    public SettingsService()
    {
        _client = new HttpClient();
        // Localhost endpoint for loadbalancer API
        _client.BaseAddress = new Uri("https://localhost:7068");
    }

    public async Task<AdvancedSettingsDTO?> GetAdvancedSettings()
    {
        try
        {
            var httpClients = await GetHttpClients();

            var resultDTOs = new List<AdvancedSettingsDTO>();

            foreach (var client in httpClients)
            {
                client.Timeout = TimeSpan.FromSeconds(2);

                var response = await client.GetAsync("api/Settings/GetSettings");

                response.EnsureSuccessStatusCode();

                var resultDTO = await response.Content.ReadFromJsonAsync<AdvancedSettingsDTO>();
                
                if (resultDTO != null)
                {
                    resultDTOs.Add(resultDTO);

                    return resultDTOs.FirstOrDefault();
                }
            }

            FlushHttpClients(httpClients);

            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Error while calling the API", e);
        }
    }

    public async Task SetSearchResults(int? noOfResults)
    {
        var httpClients = await GetHttpClients();

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
        var httpClients = await GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleCaseSensitive?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    public async Task ToggleTimeStamps(bool state)
    {
        var httpClients = await GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleTimeStamps?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    private async Task<List<HttpClient>> GetHttpClients()
    {
        var response = await _client.GetAsync("api/Loadbalancer/GetAllEndpoints");

        response.EnsureSuccessStatusCode();

        var endpoints = await response.Content.ReadFromJsonAsync<List<string>>();

        var httpClients = new List<HttpClient>();

        foreach (var endpoint in endpoints)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            httpClients.Add(client);
        }

        return httpClients;
    }

    private void FlushHttpClients(List<HttpClient> httpClients)
    {
        foreach (var client in httpClients)
        {
            client.Dispose();
        }
    }
}

