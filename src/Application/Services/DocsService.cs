using System.Net.Http.Json;
using Application.Interfaces;
using Core.DTOs;

namespace Application.Services;

public class DocsService : IDocsService
{
    public async Task<string?> GetDocumentContent(string documentPath)
    {
        try
        {
            var endpoint = await GetNextEndpoint();

            var uri = new Uri(endpoint);

            using var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = await client.GetAsync($"api/Docs/GetDocumentContent?documentPath={documentPath}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;

        }
        catch (HttpRequestException ex)
        {
            // Handle exception, log, or rethrow
            throw new Exception("Error while calling the API", ex);
        }
    }

    private async Task<string> GetNextEndpoint()
    {
        using var client = new HttpClient
        {
            // Localhost endpoint for loadbalancer API
            BaseAddress = new Uri("http://localhost:5291")
        };

        var endpointResponse = await client.GetAsync("api/Loadbalancer/GetNextEndpoint");

        endpointResponse.EnsureSuccessStatusCode(); // Ensure a successful response status code

        var endpointURI = await endpointResponse.Content.ReadAsStringAsync();

        return endpointURI;

    }

}
