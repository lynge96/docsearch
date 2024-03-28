using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SettingsController : ControllerBase
{
    private readonly ILogger<SettingsController> _logger;
    private readonly IUpdateSettings _updateSettings;

    public SettingsController(ILogger<SettingsController> logger, IUpdateSettings updateSettings)
    {
        _logger = logger;
        _updateSettings = updateSettings;
    }

    [HttpGet(Name = "GetSettings")]
    public ActionResult<AdvancedSettingsDTO> GetSettings()
    {
        try
        {
            var settings = _updateSettings.GetSettings();

            _logger.LogInformation("Fetching settings: {@settings}", settings);

            return Ok(settings);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    [HttpPut(Name = "ToggleCaseSensitive")]
    public IActionResult ToggleCaseSensitive(bool state)
    {
        try
        {
            _updateSettings.ToggleCaseSensitive(state);

            _logger.LogInformation("Toggled Casesensitive: {state}", state);

            return Ok(new { Message = "Toggle successful", State = AdvancedSettings.IsCaseSensitive });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    [HttpPut(Name = "ToggleTimeStamps")]
    public IActionResult ToggleTimeStamps(bool state)
    {
        try
        {
            _updateSettings.ToggleTimeStamps(state);

            _logger.LogInformation("Toggled Timestamps: {state}", state);

            return Ok(new { Message = "Toggle successful", State = AdvancedSettings.ViewTimeStamp });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

    [HttpPut(Name = "SetNoOfResults")]
    public IActionResult SetNoOfResults(int? noOfResults)
    {
        try
        {
            _updateSettings.NoOfResults(noOfResults);

            _logger.LogInformation("Number of search results has been set to {results}", noOfResults);

            return Ok(new { Message = "Search results updated", State = AdvancedSettings.SearchResults });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

}

