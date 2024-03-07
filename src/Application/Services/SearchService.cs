using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;
using Core.Settings;

public class SearchService : ISearchService
{
    private readonly HttpClient _httpClient;

    public SearchService()
    {
        _httpClient = new HttpClient();

        _httpClient.BaseAddress = new Uri(ConnectionStrings.ApiUrl);
    }

    public async Task<SearchResultDTO?> SearchAsync(string[] search)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Search/getSearchResult?search={string.Join("&search=", search)}");

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