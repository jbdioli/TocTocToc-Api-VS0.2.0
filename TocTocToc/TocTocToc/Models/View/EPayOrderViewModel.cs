using PropertyChanged;
using System.Collections.Generic;
using System;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class EPayOrderViewModel
{
    public EPayOrderViewModel()
    {
        OrderLines = new List<EPayOrderLinesViewModel>();
        BillingAddress = new EPayAddressViewModel();
        ShippingAddress = new EPayAddressViewModel();
    }

    // Order
    public int Id { get; set; }

    public string OrderId { get; set; }

    public int IdUsers { get; set; }

    public int IdBillAddress { get; set; }

    public int IdShipAddress { get; set; }

    public string Reference { get; set; }

    public string OrderState { get; set; }

    public string InvoiceNum { get; set; }

    public float AmountTotalHt { get; set; }

    public float AmountTotalTtc { get; set; }

    public float AmountTotalTva { get; set; }

    public float PortPriceTtc { get; set; }

    public float AmountToPay { get; set; }

    public DateTime DateInvoice { get; set; }

    public DateTime DateSendOrder { get; set; }

    public DateTime DateSendInvoice { get; set; }

    // Order lines
    public List<EPayOrderLinesViewModel> OrderLines { get; set; }

    // Billing address
    public EPayAddressViewModel BillingAddress { get; set; }

    // Delivery address
    public EPayAddressViewModel ShippingAddress { get; set; }

    public bool IsShippingAsBilling { get; set; } = false;
}