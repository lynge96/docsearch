using Application.Mapper;
using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;

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

            return Ok(dto);
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get search result", e);
        }
    }

}
