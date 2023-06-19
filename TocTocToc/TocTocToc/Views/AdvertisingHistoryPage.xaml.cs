using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AdvertisingHistoryPage : ContentPage
    {
        private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());
        private readonly HttpRequestChannelHandler _httpRequestChannelHandler = new(new AdvertisingStorageServiceChannel());

        public ObservableCollection<AdvertisingViewModel> ObserverAdvertisingViewModels { get; set; }
        private List<AdvertisingViewModel> _advertisingView = new();
        private List<AdvertisingDtoModel> _advertisementsDto = new();
        private AdvertisingDtoModel _advertising = new();
        private IDisposable _disposed = null;

        public AdvertisingHistoryPage()
        {
            InitializeComponent();

        }


        protected override void OnAppearing()
        {
            GetAdvertising();
            if (_disposed != null) return;
            SubscribeToData();
        }

        private async void OnInvokedEdited(object sender, EventArgs e)
        {
            var advertising = new AdvertisingDtoModel();
            if (sender is not SwipeItem item) return;

            if (item.BindingContext is AdvertisingViewModel advertisingViewModel)
            {
                CopyModel.AdvertisingCopyViewModelToDto(advertisingViewModel, advertising);
                advertising.IsEditMode = true;
                if (!string.IsNullOrWhiteSpace(advertisingViewModel.Image))
                    advertising.IsImage = true;
            }
            RxNetHandler.AdvertisingSubject.OnNext(advertising);
            await Navigation.PopAsync();
        }

        private async void OnInvokedPaused(object sender, EventArgs e)
        {
            if (sender is not SwipeItem { BindingContext: AdvertisingViewModel advertisingView }) return;
            if (advertisingView.IsPayed == false)
            {
                _notificationChannelHandler.SendNotification(ENotificationType.IsPayedNeed, null);
                return;
            }

            advertisingView.IsPayed = !advertisingView.IsPayed;

            var advertisingDto = new AdvertisingDtoModel();
            CopyModel.AdvertisingCopyViewModelToDto(advertisingView, advertisingDto);

            await _httpRequestChannelHandler.UpdateHttpAsync<AdvertisingDtoModel, AdvertisingDtoModel>(advertisingDto);
        }

        private async void OnInvokedDeleted(object sender, EventArgs e)
        {
            if (sender is not SwipeItem { BindingContext: AdvertisingViewModel advertisement }) return;
            var advertisementId = advertisement.AdvertisingId;
            var index = _advertisingView.FindIndex(el => el.AdvertisingId.Equals(advertisementId));
            _advertisingView.RemoveAt(index);
            await _httpRequestChannelHandler.DeleteHttpAsync<List<AdvertisingDtoModel>>(advertisementId);

        }


        private async void GetAdvertising()
        {
            await _httpRequestChannelHandler.GetHttpAsync<List<AdvertisingDtoModel>>();
        }


        private void SubscribeToData()
        {
            _disposed = RxNetHandler.AdvertisementsSubject.Subscribe(
                advertisements =>
                {
                    _advertisementsDto = advertisements;
                    CopyAdvertisingToCollection();

                },
                () =>
                {
                    Console.WriteLine("[ completed ]");
                }
            );
        }


        private void CopyAdvertisingToCollection()
        {
            _advertisingView = new List<AdvertisingViewModel>();

            foreach (var advertising in _advertisementsDto)
            {
                var advertisingViewModel = new AdvertisingViewModel();
                CopyModel.AdvertisingCopyDtoToViewModel(advertising, advertisingViewModel);
                advertisingViewModel.FullPathImage = WebConstants.Url + advertising.Path + advertising.Image;
                _advertisingView.Add(advertisingViewModel);
            }

            ObserverAdvertisingViewModels = new ObservableCollection<AdvertisingViewModel>(_advertisingView);

            BindingContext = this;
        }


        protected override void OnDisappearing()
        {
            _disposed?.Dispose();
            _disposed = null;
        }

    }
}