using System;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class UserStorageServiceChannel : IStorageServiceChannel
{
    private readonly string _url;
    private readonly string _userId;

    public UserStorageServiceChannel()
    {
        _userId = LocalStorageService.GetUserId();
        _url = WebConstants.Url;

    }


    public async Task<T> GetDataAsync<T>()
    {
        var url = _url + WebConstants.GetUserDetails + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is UserDtoModel userDetails)
        {
            RxNetHandler.UserSubject.OnNext(userDetails);
        }

        return result;
    }

    public Task<TB> SaveDataAsync<TA, TB>(TA data)
    {
        return Task.FromResult(default(TB));
    }

    public async Task<TB> UpdateDataAsync<TA, TB>(TA data)
    {
        var url = _url + WebConstants.UpdateUserContext + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPutAsync<TA, TB>(url, token, data);

        result ??= (TB)Activator.CreateInstance(typeof(TB));

        if (result is UserDtoModel userDetails)
        {
            RxNetHandler.UserSubject.OnNext(userDetails);
        }

        return result;

    }

    public Task<T> DeleteDataAsync<T>(string publicId)
    {
        return Task.FromResult(default(T));

    }

    public async Task<T> SaveMediaAsync<T>(string image)
    {
        var url = _url + WebConstants.PostPhotoProfile + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpPostAsync<string, T>(url, token, image, true);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is UserDtoModel userDetails)
        {
            RxNetHandler.UserSubject.OnNext(userDetails);
        }


        return result;

    }

    public Task<T> UpdateMediaAsync<T>(string image, string publicId)
    {
        return Task.FromResult(default(T));
    }

    public async Task<TB> GenericStorageRequestAsync<TA, TB>(Enum eNum, TA value)
    {
        var result = default(TB);

        switch (eNum is EUserHttpRequest num ? num : 0)
        {
            case EUserHttpRequest.UserRegistrationDetailsGetRequest:
                result = await FindUserRegistrationDetailsAsync<TB>();
                if (result is UserDtoModel userDetails)
                {
                    RxNetHandler.UserSubject.OnNext(userDetails);
                }

                return result;
            case EUserHttpRequest.UserIdsDetailsGetRequest:
                await FindUserIdsDetailsAsync<TB>();
                break;
        }

        return result;
    }

    private async Task<T> FindUserRegistrationDetailsAsync<T>()
    {
        var publicIds = new UserInfoDtoModel();

        var url = _url + WebConstants.CheckUserRegistrationContext;
        var token = LocalStorageService.GetAccessToken();
        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is not UserDtoModel userDetails) return result;
        
        publicIds.UserId = userDetails.UserId;
        LocalStorageService.SavePublicId(publicIds);

        return result;
    }


    private async Task FindUserIdsDetailsAsync<T>()
    {
        var url = _url + WebConstants.UserInfoContext + _userId;
        var token = LocalStorageService.GetAccessToken();

        var result = await HttpMethods.HttpGetAsync<T>(url, token);

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is UserInfoDtoModel publicIds)
        {
            LocalStorageService.SavePublicId(publicIds);
        };

    }



}