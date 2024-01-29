
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class EPayDetailsModel : BaseViewModel
{

    public EPayDetailsModel()
    {

        _ePayPayment = new Model.EPayPaymentModel();
        _ePayOrder = new EPayOrderModel();
    }

    // Bank payment
    [ObservableProperty]
    private Model.EPayPaymentModel _ePayPayment;

    // Order
    [ObservableProperty]
    private EPayOrderModel _ePayOrder;

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