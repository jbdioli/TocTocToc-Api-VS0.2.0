using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class CountryModel : BaseViewModel
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