
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class AgeModel : BaseViewModel
{
    [ObservableProperty]
    private string _ageMaxi;

    [ObservableProperty]
    private string _ageMini;

    [ObservableProperty]
    private bool _isAllAge = false;

    [ObservableProperty]
    private bool _isAgeMini = false;

    [ObservableProperty]
    private bool _isAgeMaxi = false;

    [ObservableProperty]
    private bool _isAgeValid = false;
}