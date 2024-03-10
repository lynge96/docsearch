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
    private readonly IUpdateSettings _settingsService;

    public SettingsController(ILogger<SettingsController> logger, IUpdateSettings settingsService)
    {
        _logger = logger;
        _settingsService = settingsService;
    }

    [HttpGet(Name = "GetSettings")]
    public ActionResult<AdvancedSettingsDTO> GetSettings()
    {
        try
        {
            var settings = _settingsService.GetSettings();

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
            _settingsService.ToggleCaseSensitive(state);

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
            _settingsService.ToggleTimeStamps(state);

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
            _settingsService.NoOfResults(noOfResults);

            return Ok(new { Message = "Search results updated", State = AdvancedSettings.SearchResults });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { ErrorMessage = "Internal Server Error", ExceptionMessage = e.Message });
        }
    }

}

