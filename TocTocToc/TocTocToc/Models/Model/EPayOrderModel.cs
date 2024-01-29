using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class EPayOrderModel : BaseViewModel
{
    public EPayOrderModel()
    {
        _orderLines = new List<EPayOrderLinesModel>();
        _billingAddress = new Model.EPayAddressModel();
        _shippingAddress = new Model.EPayAddressModel();
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
    private List<EPayOrderLinesModel> _orderLines;

    // Billing address
    [ObservableProperty]
    private Model.EPayAddressModel _billingAddress;

    // Delivery address
    [ObservableProperty]
    private Model.EPayAddressModel _shippingAddress;

    [ObservableProperty]
    private bool _isShippingAsBilling = false;
}