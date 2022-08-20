using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Services;
using TocTocToc.Shared;
using TocTocToc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingPage : ContentPage
    {

        private Auth _auth = new();

        private readonly UserStorageService _userStorageService = new();
        private AdvertisingStorageService _advertisingStorageService;
        private CopyModel _copyModel = new ();

        private IDisposable _disposed = null;
        private string _userId = null;
        private List<AdvertisingDto> _adsDto = new ();
        private List<AdvertisingViewModel> _adsViewModel = new ();
        public ObservableCollection<AdvertisingViewModel> AdsCollection { get; set; }

        private ActivityIndicator _activityIndicator = new();

        private bool _isInit;

        public AdvertisingPage()
        {
            InitializeComponent();
            _isInit = false;
            Init();
        }

        protected override void OnAppearing()
        {
            if (String.IsNullOrEmpty(_userId) && !_isInit) return;
            FindAds();
            if (_disposed != null) return;
            SubscribeToData();
        }

        private async void Init()
        {
            var value = RequestNewToken();
            var token = await value;
            if (String.IsNullOrEmpty(token)) return;
            
            var userId = GetUserAccessInfo();
            _activityIndicator.IsRunning = true;
            _userId = await userId;
            _activityIndicator.IsRunning = false;

            ListView_AdvertisingList.ItemSelected += AdvertisingListOnItemSelected;

            if (string.IsNullOrEmpty(_userId)) return;
            _advertisingStorageService = new AdvertisingStorageService(_userId);
            _isInit = true;
            OnAppearing();
        }

        private void OnAddAdvertising(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdvertisingAddPage());
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }



        private async Task<string> GetUserAccessInfo()
        {
            var value = _userStorageService.GetUserRegistrationDetails();
            var userDetails = await value;
            var userId = userDetails.UserId;

            if (userDetails.Addresses == null || userDetails.Addresses.Length == 0)
            {
                LocalStorageService.SaveIsAddresses(false);
                await Navigation.PushAsync(new ProfilePage());
            }
            else
            {
                LocalStorageService.SaveIsAddresses(true);
            }
            await _userStorageService.GetUserIdsDetails(userId);
            return userId;
        }



        private async void FindAds()
        {
            await _advertisingStorageService.FindAds();
        }



        private void SubscribeToData()
        {
            _disposed = _advertisingStorageService.AdSubject.Subscribe(
                ads =>
                {
                    _adsDto = ads;
                    CopyAdvertisingToCollection();

                    BindingContext = this;
                },
                () =>
                {
                    Console.WriteLine("[ completed ]");
                }
            );
        }



        private void CopyAdvertisingToCollection()
        {
            _adsViewModel = new List<AdvertisingViewModel>();

            foreach (var ad in _adsDto)
            {
                var adViewModel = new AdvertisingViewModel();
                _copyModel.AdvertisingCopyDtoToViewModel(ad, adViewModel);
                _adsViewModel.Add(adViewModel);
            }

            AdsCollection = new ObservableCollection<AdvertisingViewModel>(_adsViewModel);
        }


        private void AdvertisingListOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ListView_AdvertisingList.SelectedItem == null) return;
            var item = ListView_AdvertisingList.SelectedItem as AdvertisingViewModel;
            DisplayAlert("Publicité Info", $"{item.Name}", "OK");
            ListView_AdvertisingList.SelectedItem = null;
        }



        /*
         * Authentication
         */

        public async Task<string> RequestNewToken()
        {
            var isToken = LocalStorageService.IsToken();
            var ctrlExpiredTokens = _auth.CtrlExpiredTokens();
            string token = null;

            if (!isToken || (ctrlExpiredTokens.IsExpiredToken && ctrlExpiredTokens.IsExpiredRefreshToken))
            {
                var value = NewToken();
                token = await value;
            }
            else
            {
                token = LocalStorageService.GetAccessToken();
            }

            return token;
        }


        private async Task<string> NewToken()
        {
            var authPage = new AuthPage();
            string token = null;

            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            authPage.Disappearing += (sender, e) =>
            {
                waitHandle.Set();
            };

            await Navigation.PushModalAsync(authPage);
            await Task.Run(() => waitHandle.WaitOne());
            token = LocalStorageService.GetAccessToken();
            return token;
        }

    }
}