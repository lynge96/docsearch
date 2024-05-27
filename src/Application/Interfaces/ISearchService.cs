using Core.DTOs;

// Dette interface definerer metoder til at udføre søgninger og hente filindhold fra en fjernservice.

namespace Application.Interfaces;

public interface ISearchService
{
    /// <summary>
    /// Udfører en asynkron søgning baseret på en række søgeord og en brugernavn.
    /// </summary>
    /// <param name="query">En række søgeord, der skal anvendes i søgningen.</param>
    /// <param name="username">Brugernavnet, der identificerer den specifikke bruger, der udfører søgningen.</param>
    /// <returns>En asynkron opgave, der repræsenterer resultatet af søgningen.</returns>
    /// enne metode udfører en asynkron søgning baseret på en række søgeord og et brugernavn. Den returnerer en asynkron opgave, der repræsenterer resultatet af søgningen som en SearchResultDTO.
    public Task<SearchResultDTO?> SearchAsync(string[] query, string username);

    /// <summary>
    /// Henter filindholdet asynkront baseret på filnavn og brugernavn.
    /// </summary>
    /// <param name="fileName">Navnet på filen, hvis indhold skal hentes.</param>
    /// <param name="username">Brugernavnet, der identificerer den specifikke bruger, der anmoder om filindholdet.</param>
    /// <returns>En asynkron opgave, der repræsenterer filindholdet.</returns>
    /// Denne metode henter filindholdet asynkront baseret på filnavn og brugernavn. Den returnerer en asynkron opgave, der repræsenterer filindholdet som en streng.
    public Task<String> GetFileContentAsync(string fileName, string username);
}
