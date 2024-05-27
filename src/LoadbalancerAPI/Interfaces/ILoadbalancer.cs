using System.Collections.Generic;

// Dette interface definerer en kontrakt for en load balancer.
// Implementeringen af dette interface skal indeholde metoder til at finde det næste endepunkt baseret på en brugersnavn og til at hente alle tilgængelige endepunkter.

namespace LoadbalancerAPI.Interfaces;

public interface ILoadbalancer
{
    // Metode til at finde det næste endepunkt baseret på brugernavn
    //Metoden tager et brugernavn som parameter og returnerer det næste tilgængelige endepunkt baseret på dette brugernavn.
    string? NextEndpoint(string? username);

    // Metode til at hente alle tilgængelige endepunkter
    //Metoden returnerer en dictionary med alle tilgængelige endepunkter.
    Dictionary<string, string> AllEndpoints();

}
