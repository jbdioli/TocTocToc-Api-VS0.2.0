
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class EPayAddressModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _firstname;

    [ObservableProperty]
    private string _lastname;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _company;

    [ObservableProperty]
    private string _address1;

    [ObservableProperty]
    private string _address2;

    [ObservableProperty]
    private string _address3;

    [ObservableProperty]
    private int _idCities;

    [ObservableProperty]
    private string _city;

    [ObservableProperty]
    private string _zipcode;

    [ObservableProperty]
    private string _state;

    [ObservableProperty]
    private string _country;

    [ObservableProperty]
    private int _idCountries;

}