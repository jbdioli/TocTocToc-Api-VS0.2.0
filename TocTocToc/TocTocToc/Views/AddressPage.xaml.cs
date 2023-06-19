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
    public partial class AddressPage : ContentPage
    {
        private IDisposable _disposed;

        private readonly HttpRequestChannelHandler _httpRequestChannelHandler = new(new AddressStorageServiceChannel());
        private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());

        //private readonly AddressStorageService _addressStorageService;

        private List<AddressViewModel> _addressesViewModel;
        private AddressViewModel _addressViewModel;
        private List<AddressDtoModel> _addressesDto;


        public ObservableCollection<AddressViewModel> ObserverAddressesViewModels { get; set; }

        public AddressPage()
        {
            InitializeComponent();

            _addressesViewModel = new List<AddressViewModel>();
            _addressesDto = new List<AddressDtoModel>();

            Init();

        }

        protected override void OnAppearing()
        {
            //_disposed = _httpRequestChannelHandler.HandelHttpEvents< List<AddressDtoModel>, List <AddressDtoModel>>(EEventsHandler.ListenEvent, null).Subscribe(
            //    addresses =>
            //    {
            //        _addressesDto = addresses;
            //        CopyAddressesToCollection();

            //    },
            //    () =>
            //    {
            //        Console.WriteLine("[ completed ]");
            //    }
            //);
            _disposed = RxNetHandler.AddressesSubject.Subscribe(
                addresses =>
                {
                    _addressesDto = addresses;
                    CopyAddressesToCollection();

                },
                () =>
                {
                    Console.WriteLine("[ completed ]");
                }
            );
        }


        private async void Init()
        {
            await _httpRequestChannelHandler.GetHttpAsync<List<AddressDtoModel>>();
        }

        private void CopyAddressesToCollection()
        {
            _addressesViewModel = new List<AddressViewModel>();

            foreach (var address in _addressesDto)
            {
                var addressViewModel = new AddressViewModel();
                CopyModel.AddressCopyDtoToViewModel(address, addressViewModel);
                _addressesViewModel.Add(addressViewModel);
            }

            ObserverAddressesViewModels = new ObservableCollection<AddressViewModel>(_addressesViewModel);

            BindingContext = this;
        }

        //private async void OnAddAddress(object sender, EventArgs e)
        //{
        //    _addressViewModel = new addressViewModel();
        //    var result = await Navigation.ShowPopupAsync(new AddressPopup(_addressViewModel));
        //}

        private void OnNamed(object sender, EventArgs e)
        {

        }



        private void OnAdd(object sender, EventArgs e)
        {

        }


        private async void OnInvokedDelete(object sender, EventArgs e)
        {
            if (sender is not SwipeItem { BindingContext: AddressViewModel address }) return;
            var addressId = address.AddressId;
            var isActive = address.IsActive;
            if (isActive)
            {
                _notificationChannelHandler.SendNotification(ENotificationType.IsDeleteActiveAddressInvalid, null);
                return;
            }
                
            var index = _addressesViewModel.FindIndex(el => el.AddressId == addressId);
            switch (index)
            {
                case -1:
                    _notificationChannelHandler.SendNotification(ENotificationType.IsEmptyAddressInvalid, null);
                    return;
                case 0:
                    _notificationChannelHandler.SendNotification(ENotificationType.IsOneAddressNeeded, null);
                    return;
                default:
                    _addressesViewModel.RemoveAt(index);
                    await _httpRequestChannelHandler.DeleteHttpAsync<List<AddressDtoModel>>(addressId);
                    //_addressesDto = await _httpRequestChannelHandler.DeleteHttpAsync<List<AddressDtoModel>>(addressId);
                    //ObserverAddressesViewModels = new ObservableCollection<AddressViewModel>(_addressesViewModel);
                    break;
            }
        }

        private async void OnInvokedEdited(object sender, EventArgs e)
        {
            if (sender is not SwipeItem item) return;
            var address = item.BindingContext as AddressViewModel;

            await Navigation.PushAsync(new AddressAddOrModifyPage(address));
        }
        
        private async void OnDefine(object sender, EventArgs e)
        {
            var addressToActivate = new AddressDtoModel();
            if (addressToActivate == null) throw new ArgumentNullException(nameof(addressToActivate), "[ Error ] object AddressPage");

            if (sender is not SwipeItem item) return;
            if (item.BindingContext is not AddressViewModel address) return;
            
            var currentIndex = _addressesDto.FindIndex(el => el.AddressId.Equals(address.AddressId));
            _addressesDto[currentIndex].IsActive = true;

            addressToActivate = _addressesDto[currentIndex];

            await _httpRequestChannelHandler.GenericHttpRequestAsync<AddressDtoModel, List<AddressDtoModel>>(EAddressHttpRequest.IsActivePutRequest, addressToActivate);
            //_addressesDto = await _httpRequestChannelHandler.GenericHttpRequestAsync<AddressDtoModel, List<AddressDtoModel>>(EAddressHttpRequest.IsActivePutRequest, addressToActivate);
            //if (_addressesDto == null) return;
            //CopyAddressesToCollection();
            //_notificationChannelHandler.SendNotification(ENotificationType.IsActiveAddress, address.FullStreetAddressWithCity);
        }

        private async void OnAddAddress(object sender, EventArgs e)
        {
            _addressViewModel = new AddressViewModel();

            var addressAddOrModifyPage = new AddressAddOrModifyPage(_addressViewModel);

            //var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //addressAddOrModifyPage.Disappearing += (sender, e) =>
            //{
            //    waitHandle.Set();
            //};

            //await Navigation.PushAsync(addressAddOrModifyPage);

            //await Task.Run(() => waitHandle.WaitOne());
            //_addressesDto.Clear();
            //_addressesDto = await _httpRequestChannelHandler.GetHttpAsync<List<AddressDtoModel>>();
            //CopyAddressesToCollection();

            await Navigation.PushAsync(addressAddOrModifyPage);

        }

        protected override void OnDisappearing()
        {
            _disposed?.Dispose();
        }


    }
}