using System.Collections.Generic;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class EPayOrderViewModel : ObservableObject
{
    public EPayOrderViewModel()
    {
        _orderLines = new List<EPayOrderLinesViewModel>();
        _billingAddress = new EPayAddressViewModel();
        _shippingAddress = new EPayAddressViewModel();
    }

    // Order
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _orderId;

    [ObservableProperty]
    private int _idUsers;

    [ObservableProperty]
    private int _idBillAddress;

    [ObservableProperty]
    private int _idShipAddress;

    [ObservableProperty]
    private string _reference;

    [ObservableProperty]
    private string _orderState;

    [ObservableProperty]
    private string _invoiceNum;

    [ObservableProperty]
    private float _amountTotalHt;

    [ObservableProperty]
    private float _amountTotalTtc;

    [ObservableProperty]
    private float _amountTotalTva;

    [ObservableProperty]
    private float _portPriceTtc;

    [ObservableProperty]
    private float _amountToPay;

    [ObservableProperty]
    private DateTime _dateInvoice;

    [ObservableProperty]
    private DateTime _dateSendOrder;

    [ObservableProperty]
    private DateTime _dateSendInvoice;

    // Order lines
    [ObservableProperty]
    private List<EPayOrderLinesViewModel> _orderLines;

    // Billing address
    [ObservableProperty]
    private EPayAddressViewModel _billingAddress;

    // Delivery address
    [ObservableProperty]
    private EPayAddressViewModel _shippingAddress;

    [ObservableProperty]
    private bool _isShippingAsBilling = false;
}