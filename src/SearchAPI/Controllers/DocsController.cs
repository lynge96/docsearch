using Application.Mapper;
using Core.DTOs;
using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Interfaces;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DocsController : ControllerBase
{
    private readonly ILogger<DocsController> _logger;
    private readonly IDocsReader _docsReader;

    public DocsController(ILogger<DocsController> logger, IDocsReader docsReader)
    {
        _logger = logger;
        _docsReader = docsReader;
    }

    [HttpGet(Name = "GetDocumentContent")]
    public ActionResult<string> GetDocumentContent(string documentPath)
    {
        try
        {
            var result = _docsReader.CopyContent(documentPath);

            _logger.LogInformation("Fetching document at path: '{path}'\nAmount of characters in the file: {length}\nFirst sentence in the file: {words}", documentPath, result.Length, result.Substring(0, 16));

            return Ok(result);
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to find the file at the given path.", e);
        }
    }

}