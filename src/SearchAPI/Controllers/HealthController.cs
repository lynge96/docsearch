using Microsoft.AspNetCore.Mvc;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HealthController : ControllerBase
{
    [HttpGet(Name = "CheckHealth")]
    public IActionResult CheckHealth()
    {
        return Ok("API is running");
    }
}

