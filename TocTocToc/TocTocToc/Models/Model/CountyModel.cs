using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class CountyModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private int _idStates;
    
    [ObservableProperty]
    private string _county;

}