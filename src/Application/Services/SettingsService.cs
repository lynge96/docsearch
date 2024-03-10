using System.Net.Http.Json;
using Application.Interfaces;
using Core.DTOs;

namespace Application.Services;

public class SettingsService : ISettingsService
{
    private readonly HttpClient _httpClient;

    public SettingsService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("http://localhost:5139");
    }

    public async Task<AdvancedSettingsDTO> GetAdvancedSettings()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Settings/GetSettings");

            response.EnsureSuccessStatusCode();

            var resultDTO = await response.Content.ReadFromJsonAsync<AdvancedSettingsDTO>();

            return resultDTO;
        }
        catch (Exception e)
        {
            throw new Exception("Error while calling the API", e);
        }
    }

    public async Task SetSearchResults(int? noOfResults)
    {
        if (noOfResults <= 0)
        {
            throw new ArgumentException("Number of results must be a positive integer.", nameof(noOfResults));
        }

        var response = await _httpClient.PutAsync($"api/Settings/SetNoOfResults?noOfResults={noOfResults}", null);

        //if (!response.IsSuccessStatusCode)
        //{
        //    throw new HttpRequestException($"Error setting search results. Status code: {response.StatusCode}");
        //}
    }

    public async Task ToggleCaseSensitive(bool state)
    {
        await _httpClient.PutAsync($"api/Settings/ToggleCaseSensitive?state={state}", null);
    }

    public async Task ToggleTimeStamps(bool state)
    {
        await _httpClient.PutAsync($"api/Settings/ToggleTimeStamps?state={state}", null);
    }
}
