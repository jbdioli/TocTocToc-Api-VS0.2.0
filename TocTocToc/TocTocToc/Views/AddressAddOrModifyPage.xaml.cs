using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Collections;
using TocTocToc.DtoModels;
using TocTocToc.Shared;
using TocTocToc.ViewModels;
using Geolocation = Xamarin.Essentials.Geolocation;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressAddOrModifyPage : ContentPage
    {
        private readonly CopyModel copyModel;
        private ItemsStorageService _itemsStorageService;
        private AddressStorageService _addressStorageService;
        private AddressViewModel _addressViewModel;
        private readonly Geocoder _geocoder = new Geocoder();
        private AddressDto _addressDto = new AddressDto();
        private IList<HousingTypeDto> _housingTypes;
        private string _settingId;
        private string _userId;

        private bool _isAddressNamed;
        private bool _isHousingType;
        private bool _isAddress1;
        private bool _isCity;
        private bool _isCountry;


        public AddressAddOrModifyPage(AddressViewModel address)
        {
            InitializeComponent();
            copyModel = new CopyModel();
            
            _userId = LocalStorageService.GetUserId();
            SaveButton.IsEnabled = false;
            _addressStorageService = new AddressStorageService(_userId);
            _itemsStorageService = new ItemsStorageService();

            InitEntries();
            GetDataFromDB(address);
            BodyApp();
        }


        private async void BodyApp()
        {
            _addressViewModel = await _itemsStorageService.SetHousingTypes(_addressViewModel);

            InitPicker();

            if (_addressViewModel.IsEditMode)
            {
                SetGeolocationDetails(_addressViewModel);
                PopulatePicker();
            }
            else
            {
                _addressDto = await GetGeolocationDetails(_addressDto);
                copyModel.AddressCopyDtoToViewModel(_addressDto, _addressViewModel);
                //_addressViewModel.Lon = _addressDto.Lon;
                //_addressViewModel.Lat = _addressDto.Lat;
                //_addressViewModel.Address = _addressDto.Address;
                //_addressViewModel.Zipcode = _addressDto.Zipcode;
                //_addressViewModel.City = _addressDto.City;
                //_addressViewModel.County = _addressDto.County;
                //_addressViewModel.State = _addressDto.State;
                //_addressViewModel.Country = _addressDto.Country;
            }

            BindingContext = _addressViewModel;
        }

        private void SetGeolocationDetails(AddressViewModel addressViewModel)
        {
            var lat = addressViewModel.Lat;
            var lon = addressViewModel.Lon;
            var locationName = addressViewModel.Title;

            var position = new Position(lat, lon);
            var distance = new Distance(500);

            var pin = new Pin()
            {
                Position = position,
                Label = locationName,
            };

            var mapSpan = MapSpan.FromCenterAndRadius(position, distance);

            XNameMap.MoveToRegion(mapSpan);
            XNameMap.Pins.Add(pin);
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

        private void GetDataFromDB(AddressViewModel address)
        {
            _settingId = LocalStorageService.GetSettingId();
            // _addressViewModel = address ?? new AddressViewModel();
            var addressId = address.AddressId;
            if (!string.IsNullOrEmpty(addressId))
            {
                _addressViewModel = address;
                _addressViewModel.IsEditMode = true;
            }
            else
            {
                _addressViewModel = new AddressViewModel();
            }
            
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonApproval();
        }

        private void OnAddressType(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var addressTypeDetails = (HousingTypeDto)picker.SelectedItem;
            _addressViewModel.Type = addressTypeDetails.Type;
            _addressViewModel.IdHousingTypes = addressTypeDetails.Id;

            ButtonApproval();

        }

        private void ButtonApproval()
        {
            if (!String.IsNullOrEmpty(_addressViewModel.Title))
                _isAddressNamed = true;
            if (!String.IsNullOrEmpty(_addressViewModel.Type))
                _isHousingType = true;
            if (!String.IsNullOrEmpty(_addressViewModel.Address))
                _isAddress1 = true;
            if (!String.IsNullOrEmpty(_addressViewModel.City))
                _isCity = true;
            if (!String.IsNullOrEmpty(_addressViewModel.Country))
                _isCountry = true;

            if (_isAddressNamed && _isHousingType && _isAddress1 && _isCity && _isCountry)
                SaveButton.IsEnabled = true;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            LocalStorageService.SaveIsAddresses(true);
            _addressViewModel.IsActived = true;
            _addressViewModel.DistanceWanted = 0;

            copyModel.AddressCopyViewModelToDto(_addressViewModel, _addressDto);
            if (_addressViewModel.IsEditMode)
            {
                var addressId = _addressViewModel.AddressId;
                await _addressStorageService.UpdateAddress(addressId, _addressDto);
            }
            else
                await _addressStorageService.SaveAddress(_addressDto);
            await this.Navigation.PopAsync();

        }



        private async Task<AddressDto> GetGeolocationDetails(AddressDto address)
        { 
            address = await GetCoordinate(address);
            await GetAddressCoordinate();
            address = await GetAddressByPosition(address);

            return address;
        }


        private async Task GetAddressCoordinate()
        {
            var position = SizeScale();

            var address = await _geocoder.GetAddressesForPositionAsync(position);
        }

        private Position SizeScale()
        {

            var position = new Position(_addressDto.Lat, _addressDto.Lon);
            // move to position
            XNameMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));

            return position;
        }


        private async Task<AddressDto> GetCoordinate(AddressDto addressDto)
        {
            try
            {
                var geolocation = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (geolocation == null)
                    await DisplayAlert("Waring", "No GPS signal detected", "OK");
                else
                {
                    addressDto.Lat = geolocation.Latitude;
                    addressDto.Lon = geolocation.Longitude;
                }

            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error message from geolocation: {exception.Message}");
            }

            return addressDto;
        }

        private async Task<AddressDto> GetAddressByPosition(AddressDto address)
        {
            // var returnAddress = new AddressDto();

            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(address.Lat, address.Lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    address.Address = placemark.SubThoroughfare + ' ' + placemark.Thoroughfare;
                    address.Country = placemark.CountryName;
                    address.State = placemark.AdminArea;
                    address.County = placemark.SubAdminArea;
                    address.City = placemark.Locality;
                    address.Zipcode = placemark.PostalCode;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

            //var position = new Position(addressDto.Lat,addressDto.Lon);
            //var addresses = await _geocoder.GetAddressesForPositionAsync(position);

            //var address = addresses.FirstOrDefault();
            //string[] addressSplit = address.Split(',');
            //addressSplit[0] = addressSplit[0].Substring(1);
            //addressSplit[1] = addressSplit[1].Substring(1);
            //string[] zipcodeAndcity = addressSplit[1].Split(' ');

            //returnAddress.Address1 = addressSplit[0];
            //if(addressSplit.Length >2)
            //  returnAddress.Country = addressSplit[2];
            //returnAddress.Zipcode = zipcodeAndcity[0];
            //returnAddress.City = zipcodeAndcity[1];

            return address;
        }

        private async void InitPicker()
        {
            XNameHousingTypePicker.ItemsSource = (IList)_addressViewModel.HousingTypes;
        }

        private void PopulatePicker()
        {
            XNameHousingTypePicker.SelectedItem = ((List<HousingTypeDto>)XNameHousingTypePicker.ItemsSource)
                .FirstOrDefault(element => element.Id == _addressViewModel.IdHousingTypes);
        }




    }
}