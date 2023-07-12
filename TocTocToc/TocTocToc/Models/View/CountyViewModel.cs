
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class CountyViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private int _idStates;

    [ObservableProperty]
    private string _county;

}