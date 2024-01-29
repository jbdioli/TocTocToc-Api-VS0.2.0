
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class EPayOrderLinesModel : BaseViewModel
{
    [ObservableProperty]
    private int _idOrders;

    [ObservableProperty]
    private string _productRef;

    [ObservableProperty]
    private string _wording;

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    private float _priceUnitHt;

    [ObservableProperty]
    private float _priceUnitTtc;

    [ObservableProperty]
    private float _priceBaseHt;

    [ObservableProperty]
    private float _priceBaseTtc;

    [ObservableProperty]
    private float _tvaRate;

    [ObservableProperty]
    private int _discountPercent;

}