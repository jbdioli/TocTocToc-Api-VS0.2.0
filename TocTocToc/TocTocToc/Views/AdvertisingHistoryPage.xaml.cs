using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    public partial class AdvertisingHistoryPage : ContentPage
    {
        private AdvertisingStorageService _advertisingStorageService;
        private CopyModel _copyModel = new();
        //public event EventHandler<AdvertisingDto> Response;

        public ObservableCollection<AdvertisingViewModel> ObserverAdvertisingViewModels { get; set; }
        private List<AdvertisingDto> _advertisingList;
        private AdvertisingDto _advertising = new();
        private string _userId;
        private IDisposable _disposed = null;

        public AdvertisingHistoryPage()
        {
            InitializeComponent();

            _userId = LocalStorageService.GetUserId();
            _advertisingStorageService = new AdvertisingStorageService(_userId);

        }


        protected override void OnAppearing()
        {
            GetAdvertising();
            if (_disposed != null) return;
            SubscribeToData();
        }

        private async void OnInvokedEdited(object sender, EventArgs e)
        {
            var advertising = new AdvertisingDto();
            if (sender is not SwipeItem item) return;
            var buffer = item.BindingContext as AdvertisingViewModel;
            if (buffer != null)
                _copyModel.AdvertisingCopyViewModelToDto(buffer, advertising);

            //SendResponse(_advertising);
            _advertisingStorageService.AdvertisingSubject.OnNext(advertising);
            await Navigation.PopModalAsync();
        }

        private void OnInvokedPaused(object sender, EventArgs e)
        {

        }

        private void OnInvokedDeleted(object sender, EventArgs e)
        {

        }

        //protected virtual void SendResponse(AdvertisingDto e)
        //{
        //    EventHandler<AdvertisingDto> handler = Response;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}


        private async void GetAdvertising()
        {
            await _advertisingStorageService.GetAdvertising();
        }


        private void SubscribeToData()
        {
            _disposed = _advertisingStorageService.AdvertisingListSubject.Subscribe(
                advertisingList =>
                {
                    _advertisingList = advertisingList;
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
            var advertisingViews = new List<AdvertisingViewModel>();

            foreach (var advertising in _advertisingList)
            {
                var advertisingViewModel = new AdvertisingViewModel();
                _copyModel.AdvertisingCopyDtoToViewModel(advertising, advertisingViewModel);
                advertisingViews.Add(advertisingViewModel);
            }

            ObserverAdvertisingViewModels = new ObservableCollection<AdvertisingViewModel>(advertisingViews);

            BindingContext = this;
        }


        protected override void OnDisappearing()
        {
            _disposed?.Dispose();
        }

    }
}