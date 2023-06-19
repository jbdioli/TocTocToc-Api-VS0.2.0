using PropertyChanged;

namespace TocTocToc.Models.View
{
    [AddINotifyPropertyChangedInterface]
    public class EPayPaymentViewModel
    {

        public EPayPaymentViewModel()
        {

        }

        public string CardNo { get; set; }

        public string ExpMonth { get; set; }

        public string ExpYear { get; set; }

        public string CardCvv { get; set; }

        public string Description { get; set; }

        public string Amount { get; set; }

        public string Currency { get; set; }

        public bool IsPayed { get; set; } = false;

        public double CurrencyValue => !string.IsNullOrEmpty(Amount) ? double.Parse(Amount) : 0;


    }
}