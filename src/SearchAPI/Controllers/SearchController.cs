using Application.Mapper;
using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;
using Serilog;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchLogic _searchLogic;

    public SearchController(ILogger<SearchController> logger, ISearchLogic searchLogic)
    {
        _logger = logger;
        _searchLogic = searchLogic;
    }

    [HttpGet(Name = "GetSearchResult")]
    public ActionResult<SearchResultDTO> GetSearchResult([FromQuery] string[] search)
    {
        try
        {
            var result = _searchLogic.Search(search, AdvancedSettings.SearchResults);

            var dto = SearchResultMapper.SearchResultToDTO(result);
            
            _logger.LogInformation("Search Results for query: '{search}' - {hits} hits in {timeElapsed:F2} ms", search, dto.Hits, dto.TimeUsed.TotalMilliseconds);

            return Ok(dto);
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get search result", e);
        }
    }

}
