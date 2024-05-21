using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

namespace Application.Services;

public class SearchService : ISearchService
{

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

            var response = await client.GetAsync($"api/Search/getSearchResult?search={string.Join("&search=", search)}");

            response.EnsureSuccessStatusCode(); // Ensure a successful response status code

            var resultDTO = await response.Content.ReadFromJsonAsync<SearchResultDTO>();

            return resultDTO;
        }
        catch (HttpRequestException ex)
        {
            // Handle exception, log, or rethrow
            throw new Exception("Error while calling the API", ex);
        }
    }

    private async Task<string> GetNextEndpoint(string username)
    {
        using var client = new HttpClient
        {
            // Localhost endpoint for loadbalancer API
            BaseAddress = new Uri("http://localhost:5291")
        };

        var endpointResponse = await client.GetAsync($"api/Loadbalancer/GetNextEndpoint?username={username}");

        endpointResponse.EnsureSuccessStatusCode(); // Ensure a successful response status code

        var endpointURI = await endpointResponse.Content.ReadAsStringAsync();

        return endpointURI;

    }
    
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

            var response = await client.GetAsync($"api/Search/getFileContent?fileName={fileName}");

            response.EnsureSuccessStatusCode(); // Ensure a successful response status code

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
