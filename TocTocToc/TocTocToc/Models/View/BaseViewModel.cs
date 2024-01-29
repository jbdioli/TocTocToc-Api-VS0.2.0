using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy = false;
}