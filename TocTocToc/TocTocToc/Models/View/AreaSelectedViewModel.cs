using PropertyChanged;
using System.Collections.Generic;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class AreaSelectedViewModel
{
    public CountryViewModel CountrySelected { get; set; } = new();
    public List<StateViewModel> StatesSelected { get; set; } = new();
    public List<CountyViewModel> CountiesSelected { get; set; } = new();
    public List<CityViewModel> CitiesSelected { get; set; } = new();
    public bool IsAllCountry { get; set; } = true;
    public bool IsAllState { get; set; } = true;
    public bool IsAllCounty { get; set; } = true;
    public bool IsAllCity { get; set; } = true;
    public double Km { get; set; } = new();


}