using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class InterestModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _interest;

}