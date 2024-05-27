using Core.Models;

// Denne fil indeholder definitionen af IDatabase-interfacet, som specificerer de metoder,
// der skal implementeres af enhver klasse, der fungerer som en database i søgeapplikationen.
// Interfacet indeholder metoder til at hente ord-ID'er, dokumentdetaljer, dokumenter,
// manglende ord i dokumenter, og ord fra ID'er.

namespace SearchAPI.Interfaces;

public interface IDatabase
{
    // Henter ord-ID'er baseret på en forespørgsel og returnerer også ignorerede ord
    List<int> GetWordIds(string[] query, out List<string> outIgnored);

    // Henter detaljer for en liste af dokument-ID'er
    List<BEDocument> GetDocDetails(List<int> docIds);

    // Henter dokumenter og antallet af søgeord i hvert dokument baseret på en liste af ord-ID'er
    List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds);

    // Returnerer en liste af ord-ID'er, der er i forespørgslen, men ikke i det angivne dokument
    List<int> GetMissing(int docId, List<int> wordIds);

    // Henter en liste af ord baseret på deres ID'er
    List<string> WordsFromIds(List<int> wordIds);
}
