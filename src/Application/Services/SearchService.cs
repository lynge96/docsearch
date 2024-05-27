using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

// Denne klasse implementerer ISearchService-interfacet og giver en service til at udføre søgninger og
// hente filindhold ved hjælp af HTTP-anmodninger til en loadbalancer API.


namespace Application.Services;

public class SearchService : ISearchService
{
    // Metode til at udføre en asynkron søgning
    public async Task<SearchResultDTO?> SearchAsync(string[] search, string username)
    {
        try
        {
            var endpoint = await GetNextEndpoint(username);

            var uri = new Uri(endpoint);

            using var client = new HttpClient
            {
                BaseAddress = uri
            };

            // Søg efter resultater ved at foretage en HTTP GET-anmodning til API'et
            var response = await client.GetAsync($"api/Search/getSearchResult?search={string.Join("&search=", search)}");

            response.EnsureSuccessStatusCode(); // Ensure a successful response status code

            // Læs resultatet som JSON og konverter til SearchResultDTO-objekt
            var resultDTO = await response.Content.ReadFromJsonAsync<SearchResultDTO>();

            return resultDTO;
        }
        catch (HttpRequestException ex)
        {
            // Håndter fejl, log eller kast videre
            throw new Exception("Error while calling the API", ex);
        }
    }

    // Privat hjælpemetode til at få det næste endepunkt fra belastningsafbalanceren
    private async Task<string> GetNextEndpoint(string username)
    {
        using var client = new HttpClient
        {
            // Endepunkt for loadbalanceren
            BaseAddress = new Uri("http://localhost:5291")
        };

        // Foretag en HTTP GET-anmodning for at hente det næste endepunkt baseret på brugernavn
        var endpointResponse = await client.GetAsync($"api/Loadbalancer/GetNextEndpoint?username={username}");

        endpointResponse.EnsureSuccessStatusCode(); // Sikre en succesfuld HTTP-statuskode

        // Læs endepunktet som en streng
        var endpointURI = await endpointResponse.Content.ReadAsStringAsync();

        return endpointURI;

    }

    // Metode til at hente filindhold asynkront
    public async Task<string> GetFileContentAsync(string fileName, string username)
    {
        try
        {
            var endpoint = await GetNextEndpoint(username);

            var uri = new Uri(endpoint);

            using var client = new HttpClient
            {
                BaseAddress = uri
            };

            // Hent filindhold ved at foretage en HTTP GET-anmodning til API'et
            var response = await client.GetAsync($"api/Search/getFileContent?fileName={fileName}");

            response.EnsureSuccessStatusCode(); // Ensure a successful response status code

            // Læs filindhold som en streng
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        catch (HttpRequestException ex)
        {
            // Handle exception, log, or rethrow
            throw new Exception("Error while calling the API", ex);
        }
    }
}
