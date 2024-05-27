using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

// Denne klasse implementerer ISettingsService-interfacet og giver en service til at hente
// og indstille avancerede indstillinger ved hjælp af HTTP-anmodninger til en loadbalancer API.


namespace Application.Services;

public class SettingsService : ISettingsService
{
    private readonly HttpClient _client;

    // Konstruktør, der opsætter HTTP-klienten med belastningsafbalancerens endpoint
    public SettingsService()
    {
        _client = new()
        {
            // Lokalhost-endepunkt for loadbalancer API
            BaseAddress = new Uri("http://localhost:5291")
        };
    }

    // Metode til at hente avancerede indstillinger asynkront
    public async Task<AdvancedSettingsDTO?> GetAdvancedSettings()
    {
        try
        {
            var httpClients = await GetHttpClients();

            var resultDTOs = new List<AdvancedSettingsDTO>();

            // Loop gennem hver HTTP-klient for at hente avancerede indstillinger
            foreach (var client in httpClients)
            {
                client.Timeout = TimeSpan.FromSeconds(2);

                var response = await client.GetAsync("api/Settings/GetSettings");

                response.EnsureSuccessStatusCode();

                var resultDTO = await response.Content.ReadFromJsonAsync<AdvancedSettingsDTO>();
                
                if (resultDTO != null)
                {
                    resultDTOs.Add(resultDTO);

                    // Returner det første sæt af avancerede indstillinger
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

    // Metode til at indstille antallet af søgeresultater asynkront
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

    // Metode til at skifte til/fra sagsfølsomme søgninger asynkront
    public async Task ToggleCaseSensitive(bool state)
    {
        var httpClients = await GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleCaseSensitive?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    // Metode til at skifte til/fra tidsstempler i søgeresultater asynkront
    public async Task ToggleTimeStamps(bool state)
    {
        var httpClients = await GetHttpClients();

        var putTasks = httpClients.Select(client => client.PutAsync($"api/Settings/ToggleTimeStamps?state={state}", null));

        await Task.WhenAll(putTasks);

        FlushHttpClients(httpClients);
    }

    // Privat hjælpemetode til at hente HTTP-klienter til hvert endpoint fra loadbalanceren
    private async Task<List<HttpClient>> GetHttpClients()
    {
        var response = await _client.GetAsync("api/Loadbalancer/GetAllEndpoints");

        response.EnsureSuccessStatusCode();

        var endpoints = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

        var httpClients = new List<HttpClient>();

        foreach (var endpoint in endpoints)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(endpoint.Value)
            };

            httpClients.Add(client);
        }

        return httpClients;
    }

    // Privat hjælpemetode til at frigive ressourcer ved at afslutte HTTP-klienter
    private void FlushHttpClients(List<HttpClient> httpClients)
    {
        foreach (var client in httpClients)
        {
            client.Dispose();
        }
    }
}

