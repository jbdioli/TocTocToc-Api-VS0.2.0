
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class ValueDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _text;
}