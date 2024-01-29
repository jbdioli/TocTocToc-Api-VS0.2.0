
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class ValueDetailsModel : BaseViewModel
{
    [ObservableProperty]
    private string _text;
}