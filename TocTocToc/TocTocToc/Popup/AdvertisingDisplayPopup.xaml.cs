using TocTocToc.Models.View;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingDisplayPopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {
        public AdvertisingDisplayPopup(AdvertisingViewModel advertising)
        {
            InitializeComponent();
            var advertisingView = advertising ?? new AdvertisingViewModel();
            BindingContext = advertisingView;
        }
    }
}