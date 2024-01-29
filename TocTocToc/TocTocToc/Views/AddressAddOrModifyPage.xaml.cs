using System;
using System.Collections.Generic;
using System.Linq;
using TocTocToc.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TocTocToc.Shared;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

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
        private AddressModel _addressModel;
        private readonly AddressDtoModel _addressDto = new();

        private List<ItemDtoModel> _housingTypesItem;

        private bool _isAddressNamed;
        private bool _isHousingType;
        private bool _isAddress1;
        private bool _isCity;
        private bool _isCountry;


        public AddressAddOrModifyPage(AddressModel address)
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

            if (_addressModel.IsEditMode)
            {
                _geolocationHandler.DisplayPositionWithPin();
                PopulatePicker();
            }
            else
            {
                await _geolocationHandler.DisplayAndGetLocationDetailsAsync();
                LocationDtoCopyAddressDto();
                CopyModel.AddressCopyDtoToModel(_addressDto, _addressModel);
                _addressModel.Floor = string.Empty;

            }

            BindingContext = _addressModel;
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

        private void ImportData(AddressModel address)
        {
            var addressId = address.AddressId;
            if (!string.IsNullOrEmpty(addressId))
            {
                _addressModel = address;
                _addressModel.IsEditMode = true;
                _locationDto.Lat = _addressModel.Lat;
                _locationDto.Lon = _addressModel.Lon;
                _locationDto.LocationName = _addressModel.Title;
            }
            else
            {
                _addressModel = new AddressModel();
            }
            
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonApproval();
        }

        private void OnAddressType(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var addressTypeDetails = (ItemDtoModel)picker.SelectedItem;
            _addressModel.Type = addressTypeDetails.Item;
            _addressModel.IdHousingTypes = addressTypeDetails.Id;

            ButtonApproval();

        }

        private void ButtonApproval()
        {
            if (!string.IsNullOrEmpty(_addressModel.Title))
                _isAddressNamed = true;
            if (!string.IsNullOrEmpty(_addressModel.Type))
                _isHousingType = true;
            if (!string.IsNullOrEmpty(_addressModel.Address))
                _isAddress1 = true;
            if (!string.IsNullOrEmpty(_addressModel.City))
                _isCity = true;
            if (!string.IsNullOrEmpty(_addressModel.Country))
                _isCountry = true;

            if (_isAddressNamed && _isHousingType && _isAddress1 && _isCity && _isCountry)
                SaveButton.IsEnabled = true;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            
            LocalStorageService.SaveIsAddresses(true);
            _addressModel.IsActive = true;
            _addressModel.DistanceWanted = 0;

            CopyModel.AddressCopyModelToDto(_addressModel, _addressDto);
            if (_addressModel.IsEditMode)
            {
                _addressDto.AddressId = _addressModel.AddressId;
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
                .FirstOrDefault(element => element.Id == _addressModel.IdHousingTypes);
        }




    }
}