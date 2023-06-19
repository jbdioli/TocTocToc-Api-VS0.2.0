using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Resx;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TocTocToc.Shared
{
    public class GeolocationHandler
    {
        private readonly ResourceManager _translate = new(typeof(AppResources));

        private readonly Geocoder _geocoder = new();
        private readonly LocationDtoModel _locationDto;


        public GeolocationHandler(LocationDtoModel locationDto)
        {
            _locationDto = locationDto;
        }


        public async Task GetLocationDetailsAsync()
        {
            await GetPositionAsync();
            await GetAddressDetailsByPositionAsync();

        }

        public async Task DisplayAndGetLocationDetailsAsync()
        {
            await GetPositionAsync();
            DisplayPosition();
            await GetAddressDetailsByPositionAsync();

        }


        public async Task GetPositionByAddressAsync()
        {
            //var addresses = await GetAddressesByPositionAsync();

            //await Application.Current.MainPage.DisplayAlert("Address", addresses.FirstOrDefault()?.ToString(), "OK");
            //_locationDto.FullAddress = addresses.FirstOrDefault()?.ToString();

            if (string.IsNullOrWhiteSpace(_locationDto.FullAddress))
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in street, zipcode, city and country", "OK");


            var position = (List<Position>)await _geocoder.GetPositionsForAddressAsync(_locationDto.FullAddress);
            if (position != null)
            {
                _locationDto.Lat = position.First().Latitude;
                _locationDto.Lon = position.First().Longitude;
            }

            await Application.Current.MainPage.DisplayAlert("Position", $"Lat: {_locationDto.Lat}, Lon: {_locationDto.Lat} ", "OK");

        }

        public void DisplayPosition()
        {
            var position = new Position(_locationDto.Lat, _locationDto.Lon);
            // move to position
            _locationDto.XNameMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(_locationDto.Distance)));

        }



        public void DisplayPositionWithPin()
        {
            if (_locationDto.XNameMap == null)
                throw new ArgumentNullException( nameof(_locationDto.XNameMap), "[ERROR] - In function DisplayPosition - Object GeolocationHandler");


            var position = new Position(_locationDto.Lat, _locationDto.Lon);
            var distance = new Distance(_locationDto.Distance);
            var locationName = string.IsNullOrWhiteSpace(_locationDto.LocationName) ? _translate.GetString("LabelGeolocPosition") : _locationDto.LocationName;

            var pin = new Pin
            {
                Position = position,
                Label = locationName
            };


            var mapSpan = MapSpan.FromCenterAndRadius(position, distance);

            _locationDto.XNameMap.MoveToRegion(mapSpan);
            _locationDto.XNameMap.Pins.Add(pin);
        }


        private async Task<List<string>> GetAddressesByPositionAsync()
        {
            var position = new Position(_locationDto.Lat, _locationDto.Lon);

            var address = await _geocoder.GetAddressesForPositionAsync(position) as List<string>;

            return address;
        }


        private async Task GetAddressDetailsByPositionAsync()
        {
            try
            {
                var placeMarks = await Geocoding.GetPlacemarksAsync(_locationDto.Lat, _locationDto.Lon);

                var placeMark = placeMarks?.FirstOrDefault();
                if (placeMark != null)
                {
                    _locationDto.BuildingNumber = placeMark.SubThoroughfare;
                    _locationDto.BuildingName = placeMark.FeatureName;
                    _locationDto.Address = placeMark.SubThoroughfare + ' ' + placeMark.Thoroughfare;
                    _locationDto.Country = placeMark.CountryName;
                    _locationDto.State = placeMark.AdminArea;
                    _locationDto.County = placeMark.SubAdminArea;
                    _locationDto.City = placeMark.Locality;
                    _locationDto.ZipCode = placeMark.PostalCode;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine("[ Error fnsEx ] :", fnsEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ Error fnsEx ] :", ex);

                // Handle exception that may have occurred in geocoding
            }
        }
        

        private async Task GetPositionAsync()
        {
            try
            {
                var geolocation = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (geolocation == null)
                    await Application.Current.MainPage.DisplayAlert("Waring", "No GPS signal detected", "OK");
                else
                {
                    _locationDto.Lat = geolocation.Latitude;
                    _locationDto.Lon = geolocation.Longitude;
                }

            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error message from geolocation: {exception.Message}");
            }
        }






    }
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
