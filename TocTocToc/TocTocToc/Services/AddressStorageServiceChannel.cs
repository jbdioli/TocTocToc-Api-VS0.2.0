using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class AddressStorageServiceChannel : IStorageServiceChannel
{
    private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());

    private readonly string _url;
    private readonly string _userId;

    public AddressStorageServiceChannel()
    {
        _userId = LocalStorageService.GetUserId();
        _url = WebConstants.Url;

    }


    public async Task<T> GetDataAsync<T>()
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.GetAddress + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is List<AddressDtoModel> addresses)
        {
            RxNetHandler.AddressesSubject.OnNext(addresses);
        }

        return result;
    }

    public async Task<TB> SaveDataAsync<TA, TB>(TA data)
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.PostAddress + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPostAsync<TA, TB>(url, token, data, false);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is List<AddressDtoModel> addresses)
        {
            RxNetHandler.AddressesSubject.OnNext(addresses);
        }

        return result;
    }

    public async Task<TB> UpdateDataAsync<TA, TB>(TA data)
    {
        //var httpMethods = new HttpMethods(false);
        
        var addressId = string.Empty;
        if (data is AddressDtoModel addressDto)
        {
            addressId = addressDto.AddressId;
        }

        var url = _url + WebConstants.UpdateAddress + addressId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, data);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is List<AddressDtoModel> addresses)
        {
            RxNetHandler.AddressesSubject.OnNext(addresses);
        }

        return result;
    }

    public async Task<T> DeleteDataAsync<T>(string publicId)
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.DeleteAddress + publicId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpDeleteAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is List<AddressDtoModel> addresses)
        {
            RxNetHandler.AddressesSubject.OnNext(addresses);
        }

        return result;
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

        switch (eNum is EAddressHttpRequest num ? num : 0)
        {
            case EAddressHttpRequest.IsActivePutRequest:
                result = await IsActivateAddress<TA, TB>(value);
                break;
        }

        if (result is List<AddressDtoModel> addresses)
        {
            RxNetHandler.AddressesSubject.OnNext(addresses);
        }

        return result;
    }


    //public ReplaySubject<TB> HandelStorageEvents<TA, TB>(EEventsHandler eEventsHandler, TA value)
    //{
    //    switch (eEventsHandler)
    //    {
    //        case EEventsHandler.ListenEvent when typeof(List<AddressDtoModel>) == typeof(TB):
    //            return RxNetHandler.AddressesSubject as ReplaySubject<TB>;
    //        case EEventsHandler.SendEvent:
    //        {
    //            if (value is List<AddressDtoModel> addresses)
    //                RxNetHandler.AddressesSubject.OnNext(addresses);
    //            break;
    //        }
    //    }

    //    return new ReplaySubject<TB>();
    //}

    private async Task<TB> IsActivateAddress<TA, TB>(TA addresses)
    {
        //var httpMethods = new HttpMethods(false);

        var url = _url + WebConstants.ActivateAddress;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, addresses);


        if (result != null)
        {
            if (result is not List<AddressDtoModel> buffer) return result;
            var address = buffer.Find(el => el.IsActive.Equals(true));
            _notificationChannelHandler.SendNotification(ENotificationType.IsActiveAddress, address.Address + " " + address.City);
        }

        return result;
    }
}