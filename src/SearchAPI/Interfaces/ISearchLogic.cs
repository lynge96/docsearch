using Core.Models;

// Denne fil indeholder definitionen af ISearchLogic-interfacet, som specificerer de metoder,
// der skal implementeres af enhver klasse, der indeholder logikken til at udføre søgninger i applikationen.
// Interfacet indeholder metoder til at udføre en søgning og til at generere en filsti baseret på en lokal sti.


namespace SearchAPI.Interfaces;

public interface ISearchLogic
{
    // Udfører en søgning baseret på en forespørgsel og returnerer søgeresultatet
    SearchResult Search(string[] query, int? maxAmount);

    // Genererer en filsti baseret på en lokal sti
    String GetFilePath(string localPath);
}
