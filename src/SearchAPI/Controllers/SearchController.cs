using Application.Mapper;
using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;
using System.IO;
using Application.Interfaces;

// Denne fil indeholder en API-controller til håndtering af søgefunktionalitet og filindhold i SearchAPI.
// SearchController-klassen håndterer HTTP GET-anmodninger for at få søgeresultater og filindhold.
// Controlleren bruger logning, søgelogik og cache-service til at levere de nødvendige data.

namespace SearchAPI.Controllers;

[ApiController]

// Definerer routing-skabelonen for controllerens handlinger
[Route("api/[controller]/[action]")]

// Definerer en controller, der håndterer API-anmodninger relateret til søgning og filindhold
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchLogic _searchLogic;
    private readonly ICacheService _cacheService;

    // Constructor der initialiserer logger, søgelogik og cache-service via dependency injection
    public SearchController(ILogger<SearchController> logger, ISearchLogic searchLogic, ICacheService cacheService)
    {
        _logger = logger;
        _searchLogic = searchLogic;
        _cacheService = cacheService;
    }

    // Håndterer GET-anmodninger for at få søgeresultater baseret på en søgeforespørgsel
    [HttpGet(Name = "GetSearchResult")]
    public ActionResult<SearchResultDTO> GetSearchResult([FromQuery] string[] search)
    {
        try
        {
            // Udfører søgningen og mapper resultaterne til en DTO
            var result = _searchLogic.Search(search, AdvancedSettings.SearchResults);

            var dto = SearchResultMapper.SearchResultToDTO(result);

            _logger.LogInformation("Search Results for query: '{search}' - {hits} hits in {timeElapsed:F2} ms", search,
                dto.Hits, dto.TimeUsed.TotalMilliseconds);

            return Ok(dto);
        }
        catch (Exception e)
        {
            // Kaster en undtagelse hvis noget går galt under søgningen
            throw new Exception("Error while trying to get search result", e);
        }
    }

    // Håndterer GET-anmodninger for at få indholdet af en fil baseret på filnavn
    [HttpGet(Name = "GetFileContent")]
    public ActionResult<string> GetFileContent([FromQuery] string fileName)
    {
        try
        {
            // Forsøger at hente filindhold fra cachen
            var cacheData = _cacheService.GetData<string>(fileName);

            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            // Henter filsti via søgelogik
            string filePath = _searchLogic.GetFilePath(fileName);

            if (System.IO.File.Exists(filePath))
            {
                // Læser og returnerer filindholdet, samt gemmer det i cachen
                string content = System.IO.File.ReadAllText(filePath);
                _cacheService.SetData<string>(fileName, content);
                return content;
            }
            else
            {
                // Returnerer 404 hvis filen ikke findes
                return NotFound();
            }
        }
        catch (Exception e)
        {
            // Kaster en undtagelse hvis noget går galt under hentning af filindhold
            throw new Exception("Error while trying to get file content", e);
        }
    }
}