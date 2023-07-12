
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class EPayDetailsViewModel : ObservableObject
{

    public EPayDetailsViewModel()
    {

        _ePayPayment = new EPayPaymentViewModel();
        _ePayOrder = new EPayOrderViewModel();
    }

    // Bank payment
    [ObservableProperty]
    private EPayPaymentViewModel _ePayPayment;

    // Order
    [ObservableProperty]
    private EPayOrderViewModel _ePayOrder;

    [ObservableProperty]
    private bool _isCardNumber = false;

    [ObservableProperty]
    private bool _isExpMonth = false;

    [ObservableProperty]
    private bool _isExpYear = false;

    [ObservableProperty]
    private bool _isCardCvv = false;

    [ObservableProperty]
    private bool _isFirstname = false;

    [ObservableProperty]
    private bool _isLastname = false;

    [ObservableProperty]
    private bool _isEmail = false;

    [ObservableProperty]
    private bool _isPhoneNumber = false;

    [ObservableProperty]
    private bool _isAddress1 = false;

    [ObservableProperty]
    private bool _isZipcode = false;

    [ObservableProperty]
    private bool _isState = false;

    [ObservableProperty]
    private bool _isCity = false;

    [ObservableProperty]
    private bool _isCountry = false;

    [ObservableProperty]
    private bool _isEPayValid = false;
}