using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;

// Denne fil indeholder en API-controller til håndtering af indstillinger i SearchAPI.
// SettingsController-klassen håndterer HTTP GET- og PUT-anmodninger for at hente og opdatere forskellige indstillinger.
// Controlleren bruger logning og en service til opdatering af indstillingerne for at levere og ændre data.


namespace SearchAPI.Controllers;

[ApiController]

// Definerer routing-skabelonen for controllerens handlinger
[Route("api/[controller]/[action]")]

// Definerer en controller, der håndterer API-anmodninger relateret til indstillinger
public class SettingsController : ControllerBase
{
    private readonly ILogger<SettingsController> _logger;
    private readonly IUpdateSettings _updateSettings;

    // Constructor der initialiserer logger og updateSettings service via dependency injection
    public SettingsController(ILogger<SettingsController> logger, IUpdateSettings updateSettings)
    {
        _logger = logger;
        _updateSettings = updateSettings;
    }

    // Håndterer GET-anmodninger for at hente de aktuelle indstillinger
    [HttpGet(Name = "GetSettings")]
    public ActionResult<AdvancedSettingsDTO> GetSettings()
    {
        try
        {
            // Henter indstillingerne fra updateSettings service
            var settings = _updateSettings.GetSettings();

            // Logger de hentede indstillinger
            _logger.LogInformation("Fetching settings: {@settings}", settings);

            return Ok(settings);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    // Håndterer PUT-anmodninger for at ændre indstillingen for store/små bogstaver (case sensitivity)
    [HttpPut(Name = "ToggleCaseSensitive")]
    public IActionResult ToggleCaseSensitive(bool state)
    {
        try
        {
            // Opdaterer indstillingen for case sensitivity
            _updateSettings.ToggleCaseSensitive(state);

            // Logger ændringen
            _logger.LogInformation("Toggled Casesensitive: {state}", state);

            return Ok(new { Message = "Toggle successful", State = AdvancedSettings.IsCaseSensitive });
        }
        catch (Exception e)
        {
            // Returnerer en 500 statuskode hvis noget går galt
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    // Håndterer PUT-anmodninger for at ændre indstillingen for tidsstempler
    [HttpPut(Name = "ToggleTimeStamps")]
    public IActionResult ToggleTimeStamps(bool state)
    {
        try
        {
            // Opdaterer indstillingen for tidsstempler
            _updateSettings.ToggleTimeStamps(state);

            // Logger ændringen
            _logger.LogInformation("Toggled Timestamps: {state}", state);

            return Ok(new { Message = "Toggle successful", State = AdvancedSettings.ViewTimeStamp });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    // Håndterer PUT-anmodninger for at sætte antallet af søgeresultater
    [HttpPut(Name = "SetNoOfResults")]
    public IActionResult SetNoOfResults(int? noOfResults)
    {
        try
        {
            // Opdaterer indstillingen for antallet af søgeresultater
            _updateSettings.NoOfResults(noOfResults);

            // Logger ændringen
            _logger.LogInformation("Number of search results has been set to {results}", noOfResults);

            return Ok(new { Message = "Search results updated", State = AdvancedSettings.SearchResults });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

}

