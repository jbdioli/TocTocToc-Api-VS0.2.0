using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TocTocToc.DtoModels;
using TocTocToc.Services;
using TocTocToc.Shared;
using TocTocToc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPage : ContentPage
    {
        private IDisposable _disposed;

        private readonly AddressStorageService _addressStorageService;
        private readonly CopyModel copyModel;

        private List<AddressViewModel> _addressesViewModel;
        private AddressViewModel _addressViewModel;
        private List<AddressDto> _addressesDto;


        public ObservableCollection<AddressViewModel> ObserverAddressesViewModels { get; set; }

        public AddressPage()
        {
            InitializeComponent();
            copyModel = new CopyModel();
            var userId = LocalStorageService.GetUserId();
            _addressStorageService = new AddressStorageService(userId);

            _addressesViewModel = new List<AddressViewModel>();
            //_addressViewModel = new AddressViewModel();
            _addressesDto = new List<AddressDto>();

            Init();

        }

        protected override void OnAppearing()
        {
            _disposed = _addressStorageService.AddressesSubject.Subscribe(
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
            await _addressStorageService.GetAddress();
        }

        private void CopyAddressesToCollection()
        {
            _addressesViewModel = new List<AddressViewModel>();

            foreach (var address in _addressesDto)
            {
                var addressViewModel = new AddressViewModel();
                copyModel.AddressCopyDtoToViewModel(address, addressViewModel);
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
            var isActived = address.IsActived;
            if (isActived)
            {
                await DisplayAlert("Alert", "You can't delete an activated address", "OK");
                return;
            }
                
            var index = _addressesViewModel.FindIndex(elmt => elmt.AddressId == addressId);
            switch (index)
            {
                case -1:
                    await DisplayAlert("Alert", "You can't delete an empty address list", "OK");
                    return;
                case 0:
                    await DisplayAlert("Alert", "Impossible to delete, You need at list one address", "OK");
                    return;
                default:
                    _addressesViewModel.RemoveAt(index);
                    ObserverAddressesViewModels = new ObservableCollection<AddressViewModel>(_addressesViewModel);
                    break;
            }
        }

        private async void OnInvokedEdited(object sender, EventArgs e)
        {
            if (sender is not SwipeItem item) return;
            var address = item.BindingContext as AddressViewModel;

            //_addressViewModel = new addressViewModel();
            //_addressViewModel = _addressesViewModel[0];
            // var result = await Navigation.ShowPopupAsync(new AddressPopup(address));
            await Navigation.PushAsync(new AddressAddOrModifyPage(address));
        }

        private async void OnDefine(object sender, EventArgs e)
        {
            var fullAddress = "";

            if (sender is not SwipeItem item) return;
            var address = item.BindingContext as AddressViewModel;
            if (address != null) address.IsActived = true;
            if (address != null)
                fullAddress = address.Address + " " + address.City;

            await DisplayAlert("Alert", fullAddress + " " + "has been activated", "OK");
        }

        private async void OnAddAddress(object sender, EventArgs e)
        {
            _addressViewModel = new AddressViewModel();
            //var result = await Navigation.ShowPopupAsync(new AddressPopup(_addressViewModel));

            await Navigation.PushAsync(new AddressAddOrModifyPage(_addressViewModel));

            //new NavigationPage(new AdvertisingPage());
        }


    }
}