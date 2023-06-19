using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class EPayDetailsViewModel
{

    public EPayDetailsViewModel()
    {

        EPayPayment = new EPayPaymentViewModel();
        EPayOrder = new EPayOrderViewModel();
    }

    // Bank payment
    public EPayPaymentViewModel EPayPayment { get; set; }

    // Order
    public EPayOrderViewModel EPayOrder { get; set; }

    public bool IsCardNumber { get; set; } = false;

    public bool IsExpMonth { get; set; } = false;

    public bool IsExpYear { get; set; } = false;

    public bool IsCardCvv { get; set; } = false;

    public bool IsFirstname { get; set; } = false;

    public bool IsLastname { get; set; } = false;

    public bool IsEmail { get; set; } = false;

    public bool IsPhoneNumber { get; set; } = false;

    public bool IsAddress1 { get; set; } = false;

    public bool IsZipcode { get; set; } = false;

    public bool IsState { get; set; } = false;

    public bool IsCity { get; set; } = false;

    public bool IsCountry { get; set; } = false;

    public bool IsEPayValid { get; set; } = false;
}