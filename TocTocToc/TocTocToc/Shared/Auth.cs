using System;
using System.Threading.Tasks;
using TocTocToc.Services;
using Xamarin.Forms;
using System.Threading;
using TocTocToc.DtoModels;
using TocTocToc.Views;

namespace TocTocToc.Shared
{
    public class Auth
    {

        private readonly Keycloak _keycloak = new();

        public ExpiredTokensDto CtrlExpiredTokens()
        {
            var expiredTokens = new ExpiredTokensDto
            {
                IsExpiredToken = false,
                IsExpiredRefreshToken = false
            };


            var tokenDateTime = LocalStorageService.GetTokenDateTime();

            if (tokenDateTime == new DateTime()) return expiredTokens;

            expiredTokens.IsExpiredToken = IsExpireToken(tokenDateTime);
            expiredTokens.IsExpiredRefreshToken = IsExpiredRefreshToken(tokenDateTime);

            return expiredTokens;
        }

        private static bool IsExpireToken(DateTime tokenDateTime)
        {
            var ctrl = false;
            var currentTime = DateTime.Now;
            var seconds = Convert.ToInt32(LocalStorageService.GetExpiresIn());

            var time = new TimeSpan(0, 0, 0, seconds);

            var endingTokenTime = tokenDateTime + time;

            if (endingTokenTime < currentTime) ctrl = true;
            return ctrl;
        }


        private static bool IsExpiredRefreshToken(DateTime tokenDateTime)
        {
            var ctrl = false;
            var currentTime = DateTime.Now;
            var seconds = Convert.ToInt32(LocalStorageService.GetRefreshExpiersIn());

            var time = new TimeSpan(0, 0, 0, seconds);

            var endingTokenTime = tokenDateTime + time;

            if (endingTokenTime < currentTime) ctrl = true;

            return ctrl;
        }


        public async Task<string> RequestToken()
        {
            var isToken = LocalStorageService.IsToken();
            var ctrlExpiredTokens = CtrlExpiredTokens();
            string token = null;

            if (!isToken || (ctrlExpiredTokens.IsExpiredToken && ctrlExpiredTokens.IsExpiredRefreshToken))
            {
                var value = NewToken();
                token = await value;
            }

            else if (ctrlExpiredTokens.IsExpiredToken && !ctrlExpiredTokens.IsExpiredRefreshToken)
            {
                var value = RefereshToken();
                token = await value;
            }

            // Console.WriteLine("[ Refresh Token ] " + LocalStorageService.GetRefreshToken());
            Console.WriteLine("[ New Token ] " + LocalStorageService.GetAccessToken());
            return token;
        }


        public async Task<string> RefereshToken()
        {
            var tokendetails = await _keycloak.RenewToken();
            return tokendetails.AccessToken;
        }


        private static async Task<string> NewToken()
        {
            var authPage = new AuthPage();
            string token = null;

            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            authPage.Disappearing += (sender, e) =>
            {
                waitHandle.Set();
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(authPage);
            await Task.Run(() => waitHandle.WaitOne());
            token = LocalStorageService.GetAccessToken();
            return token;
        }


        public void Logout()
        {
            LocalStorageService.CleanAuthStorage();
        }

    }
}