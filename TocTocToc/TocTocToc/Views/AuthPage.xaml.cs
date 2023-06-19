using System;
using System.Web;
using TocTocToc.Models.Dto;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        //private bool _isAuth = false;

        public AuthPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var url = oAuth2();
            if (url != null)
                Browser.Source = url;
            else
                CloseCurrentView();

        }


        private string oAuth2()
        {
            var k = new Keycloak(30);
            var auth = new AuthDtoModel();

            var clientId = AuthConstants.ClientId;
            var redirectUri = AuthConstants.RedirectUri;
            var scopes = new string[3];
            scopes[0] = "openid";
            scopes[1] = "email";
            scopes[2] = "profile";

            var codeVerifier = k.GenerateCodeVerifier();
            auth.CodeVerifier = codeVerifier;
            var stateValue = k.GenerateState();
            auth.State = stateValue;
            LocalStorageService.SaveAuth(auth);

            var codeChallengeValue = k.GenerateCodeChallenge(codeVerifier);

            var url = k.GetAuthCode(stateValue, codeChallengeValue, clientId, redirectUri, scopes);

            return url;
        }


        private void WebNavigating(object sender, WebNavigatingEventArgs e)
        {
            var header = e.Url.Split('?')[0];

            if (!header.Contains(AuthConstants.RedirectUri)) return;
            var url = e.Url;

            Browser.Source = "about:blank";

            GetToken(url);

        }

        private async void GetToken(string url)
        {
            var keycloak = new Keycloak();
            var auth = new AuthDtoModel();
            var redirectUrl = new Uri(url);
            var state = "";
            var code = "";
            var queryString = redirectUrl.Query;
            var queryDictionary = HttpUtility.ParseQueryString(queryString);

            foreach (var param in queryDictionary.AllKeys)
            {
                if (param.Contains("state"))
                    state = queryDictionary.Get("state");
                if (param.Contains("code"))
                {
                    code = queryDictionary.Get("code");
                    auth.Code = code;
                }
            }

            LocalStorageService.SaveAuth(auth);

            if (string.IsNullOrEmpty(code)) return;
            var tokenDetails = await keycloak.PostAuthorize(state, code);
            LocalStorageService.SaveTokenDetails(tokenDetails);

            if (tokenDetails != null) CloseCurrentView();
        }


        private async void CloseCurrentView()
        {
            await Navigation.PopModalAsync();
        }

    }

}