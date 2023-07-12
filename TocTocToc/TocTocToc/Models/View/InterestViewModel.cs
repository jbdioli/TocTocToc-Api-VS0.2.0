
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class InterestViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _interest;
}