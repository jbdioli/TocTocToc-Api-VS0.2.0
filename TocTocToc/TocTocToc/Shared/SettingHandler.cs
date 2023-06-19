using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class SettingHandler
{
    private readonly HttpRequestChannelHandler _httpSettingRequestChannelHandler = new(new SettingStorageServiceChannel());
    //private readonly LanguageViewModel _languageHandler = new();


    public async Task ApplicationSetup()
    {
        var setting = await GetSetting();
        SetSetting(setting);
    }

    private async Task<SettingDtoModel> GetSetting()
    {
        var settingStorage = LocalStorageService.GetSetting();

        if (settingStorage == null || !string.IsNullOrWhiteSpace(settingStorage.Language)) return settingStorage;

        var setting = await _httpSettingRequestChannelHandler.GetHttpAsync<SettingDtoModel>();
        return setting;

    }


    private static void SetSetting(SettingDtoModel setting)
    {
        LanguageViewModel.SetLanguage(setting.Language);
    }

}