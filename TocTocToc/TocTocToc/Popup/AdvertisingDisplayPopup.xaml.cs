using TocTocToc.ViewModels;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingDisplayPopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {

        public AdvertisingViewModel AdvertisingView;

        public AdvertisingDisplayPopup(AdvertisingViewModel advertisingView)
        {
            InitializeComponent();
            AdvertisingView = advertisingView ?? new AdvertisingViewModel();
            BindingContext = AdvertisingView;
        }
    }
}