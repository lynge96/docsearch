using Core.DTOs;

namespace SearchAPI.Interfaces;

public interface IUpdateSettings
{
    AdvancedSettingsDTO GetSettings();

    void ToggleCaseSensitive(bool state);

    void ToggleTimeStamps(bool state);

    void NoOfResults(int? count);

}
