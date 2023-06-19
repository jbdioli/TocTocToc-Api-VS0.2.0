using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EPayInvoicePopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {
        public EPayInvoicePopup(string url)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //Browser.On<Android>().EnableZoomControls(true);
            //Browser.On<Android>().DisplayZoomControls(false);

            Browser.ScaleX = 0.95;
            Browser.TranslationX = 1;

            if (url != null)
                Browser.Source = url;

            
        }
    }
}