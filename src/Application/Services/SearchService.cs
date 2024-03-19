using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

namespace Application.Services;

public class SearchService : ISearchService
{
    private static readonly HttpClient _client = new();

    public async Task<SearchResultDTO?> SearchAsync(string[] search)
    {
        try
        {
            var nextEndpoint = await _client.GetStringAsync("api/Loadbalancer/GetNextEndpoint");

            var uri = new Uri(nextEndpoint);

            _client.BaseAddress = uri;

            var response = await _client.GetAsync($"api/Search/getSearchResult?search={string.Join("&search=", search)}");

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
