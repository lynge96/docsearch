using Application.Interfaces;
using Core.Models;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;

    private readonly ISearchLogic _searchLogic;

    public SearchController(ILogger<SearchController> logger, ISearchLogic searchLogic)
    {
        _logger = logger;
        _searchLogic = searchLogic;
    }

    [HttpGet("getSearchResult")]
    public ActionResult<SearchResult> GetSearchResult([FromQuery] string[] search)
    {
        try
        {
            var result = _searchLogic.Search(search, AdvancedSettings.SearchResults);

            return Ok(result);
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get search result", e);
        }
    }

}
