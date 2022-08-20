using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TocTocToc.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Logout(object sender, EventArgs e)
        {
            LocalStorageService.CleanAuthStorage();
            Navigation.PushAsync(new AuthPage());
        }
    }
}