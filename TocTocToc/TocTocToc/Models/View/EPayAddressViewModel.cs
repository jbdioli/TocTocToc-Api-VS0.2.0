using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class EPayAddressViewModel
{
    public int Id { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Company { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string Address3 { get; set; }

    public int IdCities { get; set; }

    public string City { get; set; }

    public string Zipcode { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public int IdCountries { get; set; }

}