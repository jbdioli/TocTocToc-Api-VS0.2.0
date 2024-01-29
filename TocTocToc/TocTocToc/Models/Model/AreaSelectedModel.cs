using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class AreaSelectedModel : BaseViewModel
{
    [ObservableProperty]
    private CountryModel _countryDetails = new();
    
    [ObservableProperty]
    private List<StateModel> _statesDetails = [];
    
    [ObservableProperty]
    private List<CountyModel> _countiesDetails = [];
    
    [ObservableProperty]
    private List<CityModel> _citiesDetails = [];
    
    [ObservableProperty]
    private bool _isAllCountry = false;
    
    [ObservableProperty]
    private bool _isAllState = false;
    
    [ObservableProperty]
    private bool _isAllCounty = false;
    
    [ObservableProperty]
    private bool _isAllCity = false;
    
    [ObservableProperty]
    private double _km = new();

}