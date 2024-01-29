
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{
    public partial class EPayPaymentModel : BaseViewModel
    {

        [ObservableProperty]
        private string _cardNo;

        [ObservableProperty]
        private string _expMonth;

        [ObservableProperty]
        private string _expYear;

        [ObservableProperty]
        private string _cardCvv;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrencyValue))]
        private string _amount;

        [ObservableProperty]
        private string _currency;

        [ObservableProperty]
        private bool _isPayed = false;

        public double CurrencyValue => !string.IsNullOrEmpty(_amount) ? double.Parse(_amount) : 0;


    }
}