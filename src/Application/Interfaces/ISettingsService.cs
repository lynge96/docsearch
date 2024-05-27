using Core.DTOs;

// Dette interface definerer metoder til at hente og ændre avancerede indstillinger.


namespace Application.Interfaces;

public interface ISettingsService
{
    /// <summary>
    /// Henter de avancerede indstillinger asynkront.
    /// </summary>
    /// <returns>En asynkron opgave, der repræsenterer de avancerede indstillinger.</returns>
    /// enne metode henter de avancerede indstillinger asynkront. Den returnerer en asynkron opgave, der repræsenterer de avancerede indstillinger som en AdvancedSettingsDTO.
    public Task<AdvancedSettingsDTO> GetAdvancedSettings();

    /// <summary>
    /// Ændrer tilstand for om søgning skal være følsom over for store og små bogstaver asynkront.
    /// </summary>
    /// <param name="state">Tilstanden for om søgningen skal være følsom over for store og små bogstaver.</param>
    /// <returns>En asynkron opgave, der repræsenterer status for ændringen.</returns>
    ///  Denne metode ændrer tilstanden for om søgning skal være følsom over for store og små bogstaver asynkront. Den returnerer en asynkron opgave, der repræsenterer status for ændringen.
    public Task ToggleCaseSensitive(bool state);

    /// <summary>
    /// Ændrer tilstand for om tidsstempler skal vises i søgeresultaterne asynkront.
    /// </summary>
    /// <param name="state">Tilstanden for om tidsstempler skal vises i søgeresultaterne.</param>
    /// <returns>En asynkron opgave, der repræsenterer status for ændringen.</returns>
    /// Denne metode ændrer tilstanden for om tidsstempler skal vises i søgeresultaterne asynkront. Den returnerer en asynkron opgave, der repræsenterer status for ændringen.
    public Task ToggleTimeStamps(bool state);

    /// <summary>
    /// Ændrer antallet af søgeresultater, der skal returneres asynkront.
    /// </summary>
    /// <param name="amount">Antallet af søgeresultater, der skal returneres.</param>
    /// <returns>En asynkron opgave, der repræsenterer status for ændringen.</returns>
    /// Denne metode ændrer antallet af søgeresultater, der skal returneres asynkront. Den returnerer en asynkron opgave, der repræsenterer status for ændringen.
    public Task SetSearchResults(int? amount);

}
