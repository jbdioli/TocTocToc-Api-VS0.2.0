using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class EPayOrderLinesViewModel
{
    public int IdOrders { get; set; }

    public string ProductRef { get; set; }

    public string Wording { get; set; }

    public int Quantity { get; set; }

    public float PriceUnitHt { get; set; }

    public float PriceUnitTtc { get; set; }

    public float PriceBaseHt { get; set; }

    public float PriceBaseTtc { get; set; }

    public float TvaRate { get; set; }

    public int DiscountPercent { get; set; }

}