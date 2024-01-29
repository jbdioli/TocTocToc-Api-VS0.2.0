using TocTocToc.Models.Model;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingDisplayPopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {
        public AdvertisingDisplayPopup(AdvertisingModel advertising)
        {
            InitializeComponent();
            var advertisingView = advertising ?? new AdvertisingModel();
            BindingContext = advertisingView;
        }
    }
}