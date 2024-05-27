using Core.DTOs;

// Denne fil indeholder definitionen af IUpdateSettings-interfacet, som specificerer de metoder,
// der skal implementeres af enhver klasse, der håndterer opdatering af avancerede indstillinger i applikationen.
// Interfacet indeholder metoder til at hente aktuelle indstillinger, skifte følsomhed overfor store/små bogstaver,
// vise tidsstempler, og opdatere antallet af søgeresultater.


namespace SearchAPI.Interfaces;

public interface IUpdateSettings
{
    // Henter de aktuelle avancerede indstillinger og returnerer dem som en AdvancedSettingsDTO
    AdvancedSettingsDTO GetSettings();

    // Skifter indstillingen for om søgningen skal være følsom overfor store/små bogstaver
    void ToggleCaseSensitive(bool state);

    // Skifter indstillingen for om tidsstempler skal vises
    void ToggleTimeStamps(bool state);

    // Opdaterer antallet af søgeresultater der skal returneres
    void NoOfResults(int? count);

}
