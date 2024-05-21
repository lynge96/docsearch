using Application.Mapper;
using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;
using System.IO;
using Application.Interfaces;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchLogic _searchLogic;
    private readonly ICacheService _cacheService;

    public SearchController(ILogger<SearchController> logger, ISearchLogic searchLogic, ICacheService cacheService)
    {
        _logger = logger;
        _searchLogic = searchLogic;
        _cacheService = cacheService;
    }

    [HttpGet(Name = "GetSearchResult")]
    public ActionResult<SearchResultDTO> GetSearchResult([FromQuery] string[] search)
    {
        try
        {
            var result = _searchLogic.Search(search, AdvancedSettings.SearchResults);

            var dto = SearchResultMapper.SearchResultToDTO(result);

            _logger.LogInformation("Search Results for query: '{search}' - {hits} hits in {timeElapsed:F2} ms", search,
                dto.Hits, dto.TimeUsed.TotalMilliseconds);

            return Ok(dto);
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get search result", e);
        }
    }

    [HttpGet(Name = "GetFileContent")]
    public ActionResult<string> GetFileContent([FromQuery] string fileName)
    {
        try
        {
            var cacheData = _cacheService.GetData<string>(fileName);

            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            
            string filePath = _searchLogic.GetFilePath(fileName);

            if (System.IO.File.Exists(filePath))
            {
                string content = System.IO.File.ReadAllText(filePath);
                _cacheService.SetData<string>(fileName, content);
                return content;
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get file content", e);
        }
    }
}