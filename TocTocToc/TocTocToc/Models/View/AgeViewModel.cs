
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class AgeViewModel : ObservableObject
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