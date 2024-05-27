using Core.DTOs;
using Core.Settings;
using SearchAPI.Interfaces;

// Denne fil indeholder implementeringen af UpdateSettings-klassen, som bruges til at hente
// og opdatere forskellige avancerede indstillinger i applikationen. Klassen implementerer
// IUpdateSettings-interfacet og muliggør at få aktuelle indstillinger, skifte følsomhed overfor store/små bogstaver,
// vise tidsstempler, og opdatere antallet af søgeresultater.


namespace SearchAPI.Services;

public class UpdateSettings : IUpdateSettings
{
    // Henter de nuværende indstillinger og returnerer dem som en AdvancedSettingsDTO
    public AdvancedSettingsDTO GetSettings()
    {
        return new AdvancedSettingsDTO()
        {
            IsCaseSensitive = AdvancedSettings.IsCaseSensitive,
            ViewTimeStamp = AdvancedSettings.ViewTimeStamp,
            SearchResults = AdvancedSettings.SearchResults
        };
    }

    // Skifter indstillingen for om søgningen er følsom overfor store/små bogstaver
    public void ToggleCaseSensitive(bool state)
    {
        AdvancedSettings.IsCaseSensitive = state;
    }

    // Skifter indstillingen for om tidsstempler skal vises
    public void ToggleTimeStamps(bool state)
    {
        AdvancedSettings.ViewTimeStamp = state;
    }

    // Opdaterer antallet af søgeresultater der skal returneres
    public void NoOfResults(int? count)
    {
        AdvancedSettings.SearchResults = count;
    }

}
