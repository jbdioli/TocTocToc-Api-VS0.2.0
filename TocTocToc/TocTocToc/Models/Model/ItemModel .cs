
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class ItemModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _idParents;

    [ObservableProperty]
    private string _item;

}