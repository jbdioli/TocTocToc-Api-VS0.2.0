namespace TocTocToc.Models.Dto;

public class EPayDetailsDtoModel
{
    public EPayDetailsDtoModel()
    {

        EPayPayment = new EPayPaymentDtoModel();
        EPayOrder = new EPayOrderDtoModel();
    }

    // Bank payment
    public EPayPaymentDtoModel EPayPayment { get; set; }

    // Order
    public EPayOrderDtoModel EPayOrder { get; set; }

    public bool IsEPayValid { get; set; } = false;
}