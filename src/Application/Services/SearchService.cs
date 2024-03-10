﻿using Application.Interfaces;
using Core.DTOs;
using System.Net.Http.Json;

public class SearchService : ISearchService
{
    private readonly HttpClient _httpClient;

    public SearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("http://localhost:5139");
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