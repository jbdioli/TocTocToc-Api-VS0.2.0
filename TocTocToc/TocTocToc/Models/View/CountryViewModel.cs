
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class CountryViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _country;

    [ObservableProperty]
    private string _isoAlpha2;

    [ObservableProperty]
    private int _phoneCode;

}