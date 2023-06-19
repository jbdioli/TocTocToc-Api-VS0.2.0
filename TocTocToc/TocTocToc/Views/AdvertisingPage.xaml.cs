using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    public partial class AdvertisingPage : ContentPage
    {

        private readonly TokenHandler _auth = new(new KeycloakServerChannel());

        private readonly HttpRequestChannelHandler _httpAdvertisingRequestChannelHandler = new(new AdvertisingStorageServiceChannel());
        private readonly HttpRequestChannelHandler _httpUserRequestChannelHandler = new(new UserStorageServiceChannel());

        private List<AdvertisingDtoModel> _adsDto = new ();
        private List<AdvertisingViewModel> _adsViewModel = new ();
        
        public ObservableCollection<AdvertisingViewModel> AdsCollection { get; set; }

        private readonly ActivityIndicator _activityIndicator = new();
        private readonly SettingHandler _settingHandler = new();

        private bool _isInit;
        private IDisposable _disposed;
        private string _userId;


        public AdvertisingPage()
        {
            InitializeComponent();
            _isInit = false;
        }

        protected override async void OnAppearing()
        {
            XNameActivityIndicator.IsRunning = true;

            if (_disposed == null)
            {
                SubscribeToData();
            }

            if (string.IsNullOrEmpty(_userId) && !_isInit)
            {
                Init();
            }
            if (!_isInit) return;
            await _httpAdvertisingRequestChannelHandler.GenericHttpRequestAsync<List<AdvertisingDtoModel>, List<AdvertisingDtoModel>>(EAdvertisingHttpRequest.AdsGetRequest, null);

            XNameActivityIndicator.IsRunning = false;
        }

        private async void Init()
        {
            var bearer = await _auth.GetBearerAsync();
            Console.WriteLine("[ Bearer ] " + bearer);

            if (string.IsNullOrEmpty(bearer))
            {
                _isInit = false;
                return;
            }

            _userId = await GetUserAccessInfo();
            await _settingHandler.ApplicationSetup();

            XNameAdvertisingListView.ItemSelected += AdvertisingListOnItemSelected;

            if (string.IsNullOrEmpty(_userId))
            {
                _isInit = false;
                return;
            }
            _isInit = true;
            OnAppearing();
        }


        private void OnAddAdvertising(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdvertisingAddOrModifyPage());
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }



        private async Task<string> GetUserAccessInfo()
        {
            var userDetails =
                await _httpUserRequestChannelHandler.GenericHttpRequestAsync<UserDtoModel, UserDtoModel>(EUserHttpRequest.UserRegistrationDetailsGetRequest, null);
            if (userDetails == null) return null;
            if (string.IsNullOrWhiteSpace(userDetails.UserId)) return null;

            var userId = userDetails.UserId;

            if (userDetails.Addresses == null || userDetails.Addresses.Count == 0)
            {
                LocalStorageService.SaveIsAddresses(false);
                await Navigation.PushAsync(new ProfilePage());
            }
            else
            {
                LocalStorageService.SaveIsAddresses(true);
            }
            await _httpUserRequestChannelHandler.GenericHttpRequestAsync<UserInfoDtoModel, UserInfoDtoModel>(EUserHttpRequest.UserIdsDetailsGetRequest, null);
            return userId;
        }



        private void SubscribeToData()
        {
            _disposed = RxNetHandler.AdsSubject.Subscribe(
                ads =>
                {
                    _adsDto = ads;
                    CopyAdvertisingToCollection();

                    BindingContext = this;
                },
                () =>
                {
                    Console.WriteLine("[ Completed ]");
                }
            );
        }



        private void CopyAdvertisingToCollection()
        {
            _adsViewModel = new List<AdvertisingViewModel>();

            foreach (var ad in _adsDto)
            {
                var adViewModel = new AdvertisingViewModel();
                CopyModel.AdvertisingCopyDtoToViewModel(ad, adViewModel);
                _adsViewModel.Add(adViewModel);
            }

            AdsCollection = new ObservableCollection<AdvertisingViewModel>(_adsViewModel);
        }


        private void AdvertisingListOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (ListView_AdvertisingList.SelectedItem == null) return;
            //var item = ListView_AdvertisingList.SelectedItem as AdvertisingViewModel;
            //DisplayAlert("Publicité Info", $"{item.Name}", "OK");
            //ListView_AdvertisingList.SelectedItem = null;
        }


        protected override void OnDisappearing()
        {
            _disposed?.Dispose();
            _disposed = null;
        }


    }
}