using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;
using TocTocToc.Views;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class KeycloakServerChannel: IAuthServer
{
    private readonly Keycloak _keycloak = new();



    public async Task<string> RequestTokenAsync()
    {
        string token;
        var isToken = LocalStorageService.IsToken();
        var ctrlExpiredTokens = CtrlExpiredTokens();

        if (!isToken || ctrlExpiredTokens.IsExpiredRefreshToken)
        {
            token = await NewTokenAsync();
            return token;
        }

        if (ctrlExpiredTokens.IsExpiredToken && !ctrlExpiredTokens.IsExpiredRefreshToken)
        {
            token = await RefreshTokenAsync();
            return token;
        }

        // Console.WriteLine("[ Refresh Token ] " + LocalStorageService.GetRefreshToken());
        Console.WriteLine("[ New Token ] " + LocalStorageService.GetAccessToken());

        token = LocalStorageService.GetAccessToken();
        return token;
    }


    private static ExpiredTokensDtoModel CtrlExpiredTokens()
    {
        var expiredTokens = new ExpiredTokensDtoModel
        {
            IsExpiredToken = true,
            IsExpiredRefreshToken = true
        };


        var tokenDateTime = LocalStorageService.GetTokenDateTime();

        //if (tokenDateTime == new DateTime()) return expiredTokens;
        if (DateTime.Compare(tokenDateTime, new DateTime()) == 0) return expiredTokens;

        expiredTokens.IsExpiredToken = IsExpireToken(tokenDateTime);
        expiredTokens.IsExpiredRefreshToken = IsExpiredRefreshToken(tokenDateTime);

        return expiredTokens;
    }


    private static bool IsExpireToken(DateTime tokenDateTime)
    {
        var isExpireToken = false;
        var currentTime = DateTime.Now;
        var seconds = Convert.ToInt32(LocalStorageService.GetExpiresIn());

        var time = new TimeSpan(0, 0, 0, seconds);

        var endingTokenTime = tokenDateTime + time;

        //if (endingTokenTime < currentTime) isExpireToken = true;
        if (DateTime.Compare(endingTokenTime, currentTime) < 0) isExpireToken = true;
        return isExpireToken;    
    }


    private static bool IsExpiredRefreshToken(DateTime tokenDateTime)
    {
        var isExpiredRefreshToken = false;
        var currentTime = DateTime.Now;
        var seconds = Convert.ToInt32(LocalStorageService.GetRefreshExpiresIn());

        var time = new TimeSpan(0, 0, 0, seconds);

        var endingTokenTime = tokenDateTime + time;

        //if (endingTokenTime < currentTime) ctrl = true;
        if (DateTime.Compare(endingTokenTime, currentTime) < 0) isExpiredRefreshToken = true;

        return isExpiredRefreshToken;
    }


    private async Task<string> RefreshTokenAsync()
    {
        var tokenDetails = await _keycloak.RenewToken();
        return tokenDetails.AccessToken;
    }


    private static async Task<string> NewTokenAsync()
    {
        var authPage = new AuthPage();
        //string token = null;

        var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

        authPage.Disappearing += (sender, e) =>
        {
            waitHandle.Set();
        };

        await Application.Current.MainPage.Navigation.PushModalAsync(authPage);
        await Task.Run(() => waitHandle.WaitOne());
        var token = LocalStorageService.GetAccessToken();
        return token;
    }


    public void Logout()
    {
        LocalStorageService.CleanAuthStorage();
    }
}