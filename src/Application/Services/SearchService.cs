using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;
using Loadbalancer;

namespace Application.Services;

public class SearchService : ISearchService
{
    private readonly ILoadBalancer _loadBalancer;

    public SearchService(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }
    
    public async Task<SearchResultDTO?> SearchAsync(string[] search)
    {
        try
        {
            var nextEndpoint = _loadBalancer.GetNextEndpoint();
            var uri = new Uri(nextEndpoint);

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
}