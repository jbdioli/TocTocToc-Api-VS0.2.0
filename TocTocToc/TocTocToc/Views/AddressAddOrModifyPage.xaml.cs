using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TocTocToc.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TocTocToc.Shared;
using PropertyChanged;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressAddOrModifyPage : ContentPage
    {
        private readonly GeolocationHandler _geolocationHandler;
        private readonly LocationDtoModel _locationDto = new();

        private readonly ItemRequestChannelHandler _itemRequestHousingTypeHandler = new(new HousingTypesItemRequest());
        //private readonly AddressStorageService _addressStorageService;
        private readonly HttpRequestChannelHandler _httpRequestChannelHandler = new(new AddressStorageServiceChannel());
        private AddressViewModel _addressViewModel;
        private readonly AddressDtoModel _addressDto = new();

        private List<ItemDtoModel> _housingTypesItem;

        private bool _isAddressNamed;
        private bool _isHousingType;
        private bool _isAddress1;
        private bool _isCity;
        private bool _isCountry;


        public AddressAddOrModifyPage(AddressViewModel address)
        {
            InitializeComponent();

            InitGeolocation();
            _geolocationHandler = new GeolocationHandler(_locationDto);

            var userId = LocalStorageService.GetUserId();
            SaveButton.IsEnabled = false;
            //_addressStorageService = new AddressStorageService(userId);

            InitEntries();
            ImportData(address);
            BodyApp();
        }


        private void InitGeolocation()
        {
            _locationDto.XNameMap = XNameMap;
            _locationDto.Distance = 500;
            XNameMap.HasZoomEnabled = false;
        }


        private async void BodyApp()
        {
            _housingTypesItem = await _itemRequestHousingTypeHandler.GetItemsAsync(null);

            InitPicker();

            if (_addressViewModel.IsEditMode)
            {
                _geolocationHandler.DisplayPositionWithPin();
                PopulatePicker();
            }
            else
            {
                await _geolocationHandler.DisplayAndGetLocationDetailsAsync();
                LocationDtoCopyAddressDto();
                CopyModel.AddressCopyDtoToViewModel(_addressDto, _addressViewModel);
                _addressViewModel.Floor = string.Empty;

            }

            BindingContext = _addressViewModel;
        }

        private void LocationDtoCopyAddressDto()
        {
            _addressDto.Lat = _locationDto.Lat;
            _addressDto.Lon = _locationDto.Lon;
            _addressDto.Address = _locationDto.Address;
            _addressDto.Zipcode = _locationDto.ZipCode;

            _addressDto.Country = _locationDto.Country;
            _addressDto.State = _locationDto.State;
            _addressDto.County = _locationDto.County;
            _addressDto.City = _locationDto.City;
        }


        private void InitEntries()
        {
            XNameCityEntry.IsReadOnly = true;
            XNameStateEntry.IsReadOnly = true;
            XNameCountryEntry.IsReadOnly = true;


            _isAddressNamed = false;
            _isHousingType = false;
            _isAddress1 = false;
            _isCity = false;
            _isCountry = false;

        }

        private void ImportData(AddressViewModel address)
        {
            var addressId = address.AddressId;
            if (!string.IsNullOrEmpty(addressId))
            {
                _addressViewModel = address;
                _addressViewModel.IsEditMode = true;
                _locationDto.Lat = _addressViewModel.Lat;
                _locationDto.Lon = _addressViewModel.Lon;
                _locationDto.LocationName = _addressViewModel.Title;
            }
            else
            {
                _addressViewModel = new AddressViewModel();
            }
            
        }

        [SuppressPropertyChangedWarnings]
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonApproval();
        }

        private void OnAddressType(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var addressTypeDetails = (ItemDtoModel)picker.SelectedItem;
            _addressViewModel.Type = addressTypeDetails.Item;
            _addressViewModel.IdHousingTypes = addressTypeDetails.Id;

            ButtonApproval();

        }

        private void ButtonApproval()
        {
            if (!string.IsNullOrEmpty(_addressViewModel.Title))
                _isAddressNamed = true;
            if (!string.IsNullOrEmpty(_addressViewModel.Type))
                _isHousingType = true;
            if (!string.IsNullOrEmpty(_addressViewModel.Address))
                _isAddress1 = true;
            if (!string.IsNullOrEmpty(_addressViewModel.City))
                _isCity = true;
            if (!string.IsNullOrEmpty(_addressViewModel.Country))
                _isCountry = true;

            if (_isAddressNamed && _isHousingType && _isAddress1 && _isCity && _isCountry)
                SaveButton.IsEnabled = true;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            
            LocalStorageService.SaveIsAddresses(true);
            _addressViewModel.IsActive = true;
            _addressViewModel.DistanceWanted = 0;

            CopyModel.AddressCopyViewModelToDto(_addressViewModel, _addressDto);
            if (_addressViewModel.IsEditMode)
            {
                _addressDto.AddressId = _addressViewModel.AddressId;
                await _httpRequestChannelHandler.UpdateHttpAsync<AddressDtoModel, List<AddressDtoModel>>(_addressDto);
            }
            else
            {
                await _httpRequestChannelHandler.SaveHttpAsync<AddressDtoModel, List<AddressDtoModel>>(_addressDto);
            }
            await Navigation.PopAsync();

        }


        private void InitPicker()
        {
            XNameHousingTypePicker.ItemsSource = _housingTypesItem;
        }

        private void PopulatePicker()
        {
            XNameHousingTypePicker.SelectedItem = ((List<ItemDtoModel>)XNameHousingTypePicker.ItemsSource)
                .FirstOrDefault(element => element.Id == _addressViewModel.IdHousingTypes);
        }




    }
}