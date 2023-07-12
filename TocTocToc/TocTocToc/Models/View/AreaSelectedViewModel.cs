using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class AreaSelectedViewModel : ObservableObject
{
    [ObservableProperty]
    private CountryViewModel _countrySelected = new();

    [ObservableProperty]
    private List<StateViewModel> _statesSelected = new();

    [ObservableProperty]
    private List<CountyViewModel> _countiesSelected = new();

    [ObservableProperty]
    private List<CityViewModel> _citiesSelected = new();

    [ObservableProperty]
    private bool _isAllCountry = true;

    [ObservableProperty]
    private bool _isAllState = true;

    [ObservableProperty]
    private bool _isAllCounty = true;

    [ObservableProperty]
    private bool _isAllCity = true;

    [ObservableProperty]
    private double _km = new();


}