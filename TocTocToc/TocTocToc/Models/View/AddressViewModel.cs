using PropertyChanged;

namespace TocTocToc.Models.View
{
    [AddINotifyPropertyChangedInterface]
    public class AddressViewModel : LanguageViewModel
    {

        public string AddressId { get; set; }

        public string Title { get; set; }

        public int IdHousingTypes { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string StreetNumber { get; set; }

        public string ResidenceName { get; set; }

        public string BuildingNumber { get; set; }

        public string BuildingName { get; set; }

        public string BuildingEntrance { get; set; }

        public string Floor { get; set; } = null;

        public string Zipcode { get; set; }

        public int IdCountries { get; set; }

        public string Country { get; set; }

        public int IdStates { get; set; }

        public string State { get; set; }

        public int IdCounties { get; set; }

        public string County { get; set; }

        public int IdCities { get; set; }

        public string City { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }

        public float DistanceWanted { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsEditMode { get; set; } = false;

        public string FullStreetAddress => $"{StreetNumber} {Address}";

        public string FullStreetAddressWithCity => $"{StreetNumber} {Address} {City}";

        public string FullPostCode => $"{Zipcode} {City} - {Country}";
    }
}