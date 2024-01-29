using TocTocToc.Models.Model;
using TocTocToc.Models.View;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaPopup : Xamarin.CommunityToolkit.UI.Views.Popup<ValueDetailsModel>
    {
        private readonly ValueDetailsModel _valueDetails = new();

        public TextAreaPopup(ValueDetailsModel valueDetails)
        {
            InitializeComponent();

            if (valueDetails != null)
                _valueDetails = valueDetails;

            BindingContext = _valueDetails;
        }

        protected override ValueDetailsModel GetLightDismissResult()
        {
            return _valueDetails;
        }
    }
}