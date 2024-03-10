using Core.DTOs;
using Core.Settings;
using SearchAPI.Interfaces;

namespace SearchAPI.Services;

public class UpdateSettings : IUpdateSettings
{
    public AdvancedSettingsDTO GetSettings()
    {
        return new AdvancedSettingsDTO()
        {
            IsCaseSensitive = AdvancedSettings.IsCaseSensitive,
            ViewTimeStamp = AdvancedSettings.ViewTimeStamp,
            SearchResults = AdvancedSettings.SearchResults
        };
    }

    public void ToggleCaseSensitive(bool state)
    {
        AdvancedSettings.IsCaseSensitive = state;
    }

    public void ToggleTimeStamps(bool state)
    {
        AdvancedSettings.ViewTimeStamp = state;
    }

    public void NoOfResults(int? count)
    {
        AdvancedSettings.SearchResults = count;
    }

}
