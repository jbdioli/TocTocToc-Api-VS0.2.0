
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{
    public partial class AddressModel : LanguageViewModel
    {
        [ObservableProperty] 
        private string _addressId;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private int _idHousingTypes;

        [ObservableProperty]
        private string _type;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullStreetAddress))]
        [NotifyPropertyChangedFor(nameof(FullStreetAddressWithCity))]
        private string _address;

        [ObservableProperty]
        private string _address1;

        [ObservableProperty]
        private string _address2;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullStreetAddress))]
        [NotifyPropertyChangedFor(nameof(FullStreetAddressWithCity))]
        private string _streetNumber;

        [ObservableProperty]
        private string _residenceName;

        [ObservableProperty]
        private string _buildingNumber;

        [ObservableProperty]
        private string _buildingName;

        [ObservableProperty]
        private string _buildingEntrance;

        [ObservableProperty]
        private string _floor = null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullPostCode))]
        private string _zipcode;

        [ObservableProperty]
        private int _idCountries;    

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullPostCode))]
        private string _country;

        [ObservableProperty]
        private int _idStates;

        [ObservableProperty]
        private string _state;

        [ObservableProperty]
        private int _idCounties;

        [ObservableProperty]
        private string _county;

        [ObservableProperty]
        private int _idCities;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullStreetAddressWithCity))]
        [NotifyPropertyChangedFor(nameof(FullPostCode))]
        private string _city;

        [ObservableProperty]
        private double _lon;

        [ObservableProperty]
        private double _lat;

        [ObservableProperty]
        private float _distanceWanted;

        [ObservableProperty]
        private bool _isActive = false;

        [ObservableProperty]
        private bool _isEditMode = false;

        public string FullStreetAddress => $"{_streetNumber} {_address}";

        public string FullStreetAddressWithCity => $"{_streetNumber} {_address} {_city}";

        public string FullPostCode => $"{_zipcode} {_city} - {_country}";
    }
}