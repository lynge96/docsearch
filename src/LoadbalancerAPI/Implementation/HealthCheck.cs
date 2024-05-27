using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadbalancerAPI.Interfaces;

// Denne fil indeholder implementeringen af HealthCheck-klassen, som implementerer IHealthCheck-interfacet.
// Klassen er ansvarlig for at kontrollere statussen for de tilgængelige servere.

namespace LoadbalancerAPI.Implementation;

public class HealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, string> _endpoints;

    // Konstruktør der initialiserer HTTP-klienten og de kendte endepunkter
    public HealthCheck()
    {
        _endpoints = new Dictionary<string, string>
        {
            {"Allen", "https://localhost:7233"},
            {"Arnold", "https://localhost:7234"}
        };
        _httpClient = new HttpClient();
    }

    // Metode til at kontrollere statussen for de tilgængelige servere
    public async Task<Dictionary<string, string>> CheckServers()
    {
        // Dictionary til at gemme tilgængelige servere
        var availableServers = new Dictionary<string, string>();

        // Gennemløber de kendte endepunkter og kontrollerer deres status
        foreach (var serverUrl in _endpoints)
        {
            try
            {
                // Sender en HTTP GET-anmodning til sundhedstjek-endepunktet for hver server
                await _httpClient.GetAsync($"{serverUrl.Value}/api/Health/CheckHealth");

                // Hvis ingen fejl opstår, tilføjes serveren til listen over tilgængelige servere
                availableServers.Add(serverUrl.Key, serverUrl.Value);
            }
            catch (HttpRequestException ex)
            {
                // Hvis der opstår en fejl, logges den
                Console.WriteLine($"Error checking server {serverUrl}: {ex.Message}");
            }
        }

        // Returnerer listen over tilgængelige servere
        return availableServers;
    }
}
