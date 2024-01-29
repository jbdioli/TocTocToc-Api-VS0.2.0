using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class StateModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private int _idCountries;
    
    [ObservableProperty]
    private string _state;

}