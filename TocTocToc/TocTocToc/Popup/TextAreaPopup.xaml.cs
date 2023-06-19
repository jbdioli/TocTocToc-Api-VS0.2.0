using TocTocToc.Models.View;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaPopup : Xamarin.CommunityToolkit.UI.Views.Popup<ValueDetailsViewModel>
    {
        private readonly ValueDetailsViewModel _valueDetails = new();

        public TextAreaPopup(ValueDetailsViewModel valueDetails)
        {
            InitializeComponent();

            if (valueDetails != null)
                _valueDetails = valueDetails;

            BindingContext = _valueDetails;
        }

        protected override ValueDetailsViewModel GetLightDismissResult()
        {
            return _valueDetails;
        }
    }
}