using Microsoft.AspNetCore.Mvc;

// Denne fil indeholder en API-controller til at kontrollere sundhedstilstanden af SearchAPI.
// HealthController-klassen håndterer HTTP GET-anmodninger til at kontrollere, om API'en kører korrekt.
// Når der sendes en GET-anmodning til endpointet "api/Health/CheckHealth", returnerer controlleren en 200 OK-statuskode med beskeden "API is running".

namespace SearchAPI.Controllers;

[ApiController]

// Definerer routing-skabelonen for controllerens handlinger
[Route("api/[controller]/[action]")]

// Definerer en controller, der håndterer API-anmodninger relateret til systemets sundhedstilstand
public class HealthController : ControllerBase
{
    // Angiver, at denne metode håndterer GET-anmodninger og navngives "CheckHealth"
    [HttpGet(Name = "CheckHealth")]

    // Definerer en metode, der tjekker systemets sundhedstilstand
    public IActionResult CheckHealth()
    {
        return Ok("API is running");
    }
}

