using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class AdvertisingStorageServiceChannel : IStorageServiceChannel
{
    private readonly string _url = WebConstants.Url;
    private readonly string _userId = LocalStorageService.GetUserId();

    public async Task<T> GetDataAsync<T>()
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.GetAdvertising + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is List<AdvertisingDtoModel> advertisement)
        {
            RxNetHandler.AdvertisementsSubject.OnNext(advertisement);
        }

        return result;
    }

    public Task<TB> SaveDataAsync<TA, TB>(TA data)
    {
        // No Save for a Advertising, First upload the image and then update the advertising

        return Task.FromResult(default(TB));
    }

    public async Task<TB> UpdateDataAsync<TA, TB>(TA data)
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.UpdateAdvertising + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, data);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is AdvertisingDtoModel advertisement)
        {
            RxNetHandler.AdvertisingSubject.OnNext(advertisement);
        }

        return result;
    }

    public async Task<T> DeleteDataAsync<T>(string publicId)
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.DeleteAdvertising + publicId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpDeleteAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is List<AdvertisingDtoModel> advertisement)
        {
            RxNetHandler.AdvertisementsSubject.OnNext(advertisement);
        }

        return result;
    }



    public async Task<T> SaveMediaAsync<T>(string image)
    {
        //var httpMethods = new HttpMethods(true);

        var url = _url + WebConstants.UploadMediaAdvertising + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPostAsync<string, T>(url, token, image, true);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is AdvertisingDtoModel advertisement)
        {
            RxNetHandler.AdvertisingSubject.OnNext(advertisement);
        }

        return result;

    }

    public async Task<T> UpdateMediaAsync<T>(string image, string publicId)
    {
        //var httpMethods = new HttpMethods(true);

        var url = _url + WebConstants.UploadUpdateMediaAdvertising + publicId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPostAsync<string, T>(url, token, image, true);

        result ??= (T)Activator.CreateInstance(typeof(T));

        return result;
    }


    public async Task<TB> GenericStorageRequestAsync<TA, TB>(Enum eNum, TA value)
    {
        var result = default(TB);

        switch (eNum is EAdvertisingHttpRequest num ? num : 0)
        {
            case EAdvertisingHttpRequest.AdsGetRequest:
                result = await FindAdsAsync<TB>();
                if (result is List<AdvertisingDtoModel> ads)
                {
                    RxNetHandler.AdsSubject.OnNext(ads);
                }

                return result;
        }

        return result;
    }


    //public ReplaySubject<TB> HandelStorageEvents<TA, TB>(EEventsHandler eEventsHandler, TA value)
    //{
    //    switch (eEventsHandler)
    //    {
    //        case EEventsHandler.ListenEvent when typeof(List<AdvertisingDtoModel>) == typeof(TB):
    //            return RxNetHandler.AdvertisementsSubject as ReplaySubject<TB>;

    //        case EEventsHandler.ListenEvent when typeof(AdvertisingDtoModel) == typeof(TB):
    //            return RxNetHandler.AdvertisingSubject as ReplaySubject<TB>;
    //    }

    //    return new ReplaySubject<TB>();
    //}


    private async Task<T> FindAdsAsync<T>()
    {
        //const bool isFile = false;
        //var httpMethods = new HttpMethods(isFile);

        var url = _url + WebConstants.FindAdvertising + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        return result;
    }
}