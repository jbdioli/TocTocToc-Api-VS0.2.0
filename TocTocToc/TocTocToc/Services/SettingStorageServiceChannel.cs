using System;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class SettingStorageServiceChannel : IStorageServiceChannel
{
    private readonly string _url;
    private readonly string _settingId;

    public SettingStorageServiceChannel()
    {
        _settingId = LocalStorageService.GetSettingId();
        _url = WebConstants.Url;
    }

    public async Task<T> GetDataAsync<T>()
    {
        var url = _url + WebConstants.GetSetting + _settingId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is SettingDtoModel settingDetails)
        {
            RxNetHandler.SettingSubject.OnNext(settingDetails);
        }

        return result;
    }

    public Task<TB> SaveDataAsync<TA, TB>(TA data)
    {
        return Task.FromResult(default(TB));
    }

    public async Task<TB> UpdateDataAsync<TA, TB>(TA data)
    {
        var url = _url + WebConstants.UpdateSetting + _settingId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, data);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is SettingDtoModel settingDetails)
        {
            RxNetHandler.SettingSubject.OnNext(settingDetails);
        }

        return result;
    }

    public Task<T> DeleteDataAsync<T>(string publicId)
    {
        return Task.FromResult(default(T));
    }

    public Task<T> SaveMediaAsync<T>(string image)
    {
        return Task.FromResult(default(T));
    }

    public Task<T> UpdateMediaAsync<T>(string image, string publicId)
    {
        return Task.FromResult(default(T));
    }

    public async Task<TB> GenericStorageRequestAsync<TA, TB>(Enum eNum, TA value)
    {
        var result = default(TB);

        switch (eNum is ESettingHttpRequest num ? num : 0)
        {
            case ESettingHttpRequest.UpdateLanguageRequest:
                result = await UpdateLanguageSettingAsync<TA, TB>(value);
                if (result is SettingDtoModel settingLanguage)
                {
                    RxNetHandler.SettingSubject.OnNext(settingLanguage);
                }

                return result;
            case ESettingHttpRequest.UpdateIsAgeRequest:
                result = await UpdateIsAgeSettingAsync<TA, TB>(value);
                if (result is SettingDtoModel settingIsAge)
                {
                    RxNetHandler.SettingSubject.OnNext(settingIsAge);
                }

                return result;
            case ESettingHttpRequest.UpdateIsFloorRequest:
                result = await UpdateIsFloorSettingAsync<TA, TB>(value);
                if (result is SettingDtoModel settingIsFloor)
                {
                    RxNetHandler.SettingSubject.OnNext(settingIsFloor);
                }

                return result;
            case ESettingHttpRequest.UpdateIsJobRequest:
                result = await UpdateIsJobSettingAsync<TA, TB>(value);
                if (result is SettingDtoModel settingIsJob)
                {
                    RxNetHandler.SettingSubject.OnNext(settingIsJob);
                }

                return result;
            case ESettingHttpRequest.UpdateIsMaritalStatusRequest:
                result = await UpdateIsMaritalStatusSettingAsync<TA, TB>(value);
                if (result is SettingDtoModel settingIsMaritalStatus)
                {
                    RxNetHandler.SettingSubject.OnNext(settingIsMaritalStatus);
                }

                break;
        }

        return result;
    }

    private async Task<TB> UpdateIsMaritalStatusSettingAsync<TA, TB>(TA value)
    {
        var url = _url + WebConstants.UpdateSettingIsMaritalStatus + _settingId;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, value);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is not SettingDtoModel settingDetails) return result;

        LocalStorageService.SaveSettingIsMaritalStatus(settingDetails);

        return result;
    }

    private async Task<TB> UpdateIsJobSettingAsync<TA, TB>(TA value)
    {
        var url = _url + WebConstants.UpdateSettingIsJob + _settingId;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, value);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is not SettingDtoModel settingDetails) return result;

        LocalStorageService.SaveSettingIsJob(settingDetails);

        return result;
    }

    private async Task<TB> UpdateIsFloorSettingAsync<TA, TB>(TA value)
    {
        var url = _url + WebConstants.UpdateSettingIsFloor + _settingId;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, value);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is not SettingDtoModel settingDetails) return result;

        LocalStorageService.SaveSettingIsFloor(settingDetails);

        return result;
    }

    private async Task<TB> UpdateIsAgeSettingAsync<TA, TB>(TA value)
    {
        var url = _url + WebConstants.UpdateSettingIsAge + _settingId;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, value);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is not SettingDtoModel settingDetails) return result;

        LocalStorageService.SaveSettingIsAge(settingDetails);

        return result;
    }

    private async Task<TB> UpdateLanguageSettingAsync<TA, TB>(TA value)
    {
        var url = _url + WebConstants.UpdateSettingLanguage + _settingId;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, value);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is not SettingDtoModel settingDetails) return result;

        LocalStorageService.SaveSettingLanguage(settingDetails);

        return result;
    }
}