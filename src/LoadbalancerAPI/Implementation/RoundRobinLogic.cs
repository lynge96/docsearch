using System.Collections.Generic;
using System.Linq;
using LoadbalancerAPI.Interfaces;

// Denne fil indeholder implementeringen af RoundRobinLogic-klassen, som implementerer ILoadbalancer-interfacet.
// Klassen er ansvarlig for at håndtere round-robin-logikken for tilgængelige API-endepunkter.

namespace LoadbalancerAPI.Implementation;

public class RoundRobinLogic : ILoadbalancer
{
    private static int _next;

    // Konstruktør der initialiserer den næste index til 0
    public RoundRobinLogic()
    {
        _next = 0;
    }

    // Metode til at finde det næste endepunkt baseret på round-robin-logik
    public string? NextEndpoint(string? username)
    {
        // Henter tilgængelige endepunkter fra StartupService
        var endpoints = StartupService.Endpoints;

        // Hvis der ikke er nogen tilgængelige endepunkter, returneres null
        if (endpoints == null || endpoints.Count == 0)
        {
            return null;
        }

        // Cykler gennem endepunkterne i round-robin-stil
        _next = (_next + 1) % endpoints.Count;

        // Returnerer det næste endepunkt baseret på den næste index
        return endpoints.ElementAt(_next).Value;
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