using System;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.DateTime;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);


        }

        private void OnBlogPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BlogPage());
        }

        private void OnNeighborPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NeighborPage());
        }

        private void OnMessagingPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagingPage());
        }

        private void OnServiceExchangePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ServiceExchangePage());
        }

        private void OnOfferPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OfferPage());
        }

        private void OnRentalPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RentalPage());
        }

        private void OnEventPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EventPage());
        }

        private void OnForumPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForumPage());
        }

        private void OnSolidarityPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SolidarityPage());
        }

        private void OnDirectoryPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DirectoryPage());
        }

        private void OnSettingPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingPage());
        }

        private void OnAdvertisementPage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnProfilePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
    }
}