using TocTocToc.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingHistoryPage : ContentPage
    {
        //private object _advertisementContext;

        public AdvertisingHistoryPage()
        {
            InitializeComponent();
            //_advertisementContext  = XNameContentAdvertisingHistoryPage.BindingContext;
        }


        protected override void OnAppearing()
        {
            
        }



        protected override void OnDisappearing()
        {
            //var context = (AdvertisementsViewModel)_advertisementContext;
            
            //context.Disposed.Dispose();
            //context.Disposed = null;
        }

    }
}