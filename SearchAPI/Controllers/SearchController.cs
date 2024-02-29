using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;

    public SearchController(ILogger<SearchController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetSearchResult")]
    public ActionResult<SearchResult> GetSearchResult([FromQuery] string[] search)
    {
        // TODO: API logic here
        return null;
    }



}
