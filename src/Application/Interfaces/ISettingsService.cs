using Core.DTOs;

namespace Application.Interfaces;

public interface ISettingsService
{
    public Task<AdvancedSettingsDTO> GetAdvancedSettings();

    public Task ToggleCaseSensitive(bool state);

    public Task ToggleTimeStamps(bool state);

    public Task SetSearchResults(int? amount);

}
