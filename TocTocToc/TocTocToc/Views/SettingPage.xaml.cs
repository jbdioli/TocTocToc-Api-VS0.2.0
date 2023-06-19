using System;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private readonly HttpRequestChannelHandler _httpRequestSettingChannelHandler = new(new SettingStorageServiceChannel());
        private readonly LanguageViewModel _languageHandler = new();

        private readonly SettingDtoModel _settingDto = new();

        public SettingPage()
        {
            InitializeComponent();

            //BindingContext = _languageHandler;
        }


        private async void OnAppLanguage(object sender, EventArgs e)
        {
            //_languageHandler.CurrentLanguage();
            var language = await _languageHandler.ChangeLanguage();
            _settingDto.Language = language;
            if (string.IsNullOrEmpty(language)) return;
            await _httpRequestSettingChannelHandler.GenericHttpRequestAsync<SettingDtoModel, SettingDtoModel>(
                ESettingHttpRequest.UpdateLanguageRequest, _settingDto);
        }



        private void Logout(object sender, EventArgs e)
        {
            LocalStorageService.CleanAuthStorage();
            Navigation.PushAsync(new AuthPage());
        }


    }
}