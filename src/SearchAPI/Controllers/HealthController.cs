using Microsoft.AspNetCore.Mvc;

namespace SearchAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HealthController : ControllerBase
{
    [HttpGet(Name = "HealthCheck")]
    public ActionResult CheckHealth()
    {
        var isHealthy = true;

        if (isHealthy)
        {
            return Ok("API is healthy");
        }

        return StatusCode(500, "API is unhealthy");
    }
}

