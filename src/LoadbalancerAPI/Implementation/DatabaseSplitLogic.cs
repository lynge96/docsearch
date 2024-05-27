using System.Collections.Generic;
using System.Linq;
using LoadbalancerAPI.Interfaces;

// Denne fil indeholder implementeringen af DatabaseSplitLogic-klassen, som implementerer ILoadbalancer-interfacet.
// Klassen er ansvarlig for at håndtere opdelingslogikken for tilgængelige API-endepunkter baseret på brugernavn.

namespace LoadbalancerAPI.Implementation;

public class DatabaseSplitLogic : ILoadbalancer
{
    private static int _next;

    // Konstruktør der initialiserer den næste index til 0
    public DatabaseSplitLogic()
    {
        _next = 0;
    }

    // Metode til at finde det næste endepunkt baseret på brugernavn
    public string? NextEndpoint(string username)
    {
        // Henter tilgængelige endepunkter fra StartupService
        var availableEndpoints = StartupService.Endpoints;

        // Hvis endepunkterne indeholder et endepunkt for det angivne brugernavn, returneres det
        if (availableEndpoints.TryGetValue(username, out string endpoint))
        {
            return endpoint;
        }
        else
        {
            return null;
        }
    }

    // Metode til at hente alle tilgængelige endepunkter
    public Dictionary<string, string>? AllEndpoints()
    {
        // Hvis der ikke er nogen tilgængelige endepunkter, returneres null
        if (StartupService.Endpoints == null)
        {
            return null;
        }

        // Returnerer alle tilgængelige endepunkter
        return StartupService.Endpoints;
    }
}