using System;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgePopup : Xamarin.CommunityToolkit.UI.Views.Popup<AgeViewModel>
    {

        private readonly AgeViewModel _ageViewModel = new();

        public AgePopup(AgeViewModel age)
        {
            InitializeComponent();
            XNameValidated.IsEnabled = false;
            XNameAgeAlert.IsVisible = false;
            if (age != null)
            {
                _ageViewModel = age;
                if (age.IsAllAge && !string.IsNullOrEmpty(age.AgeMini) && !String.IsNullOrEmpty(age.AgeMaxi))
                 XNameValidated.IsEnabled = true;
            }
            BindingContext = _ageViewModel;
        }

        private void OnIsAllAge(object sender, CheckedChangedEventArgs e)
        {
            if (sender is not CheckBox checkBox) return;
            var isChecked = checkBox.IsChecked;
            _ageViewModel.IsAllAge = isChecked;

            if (isChecked)
            {
                XNameAgeMini.IsVisible = false;
                XNameAgeMaxi.IsVisible = false;
                XNameLabelTo.IsVisible = false;
            }
            else
            {
                if (XNameAgeMini.IsVisible == false) XNameAgeMini.IsVisible = true;
                if (XNameAgeMaxi.IsVisible == false) XNameAgeMaxi.IsVisible = true;
                if (XNameLabelTo.IsVisible == false) XNameLabelTo.IsVisible = true;
            }

            CheckValidation();

            // _ageViewModel.IsAllAge = XNameIsAllAge.IsChecked;
        }




        private void OnValidated(object sender, EventArgs e)
        {
            Dismiss(_ageViewModel);
        }

        private void OnAgeMini(object sender, TextChangedEventArgs e)
        {
            var values = (Entry)sender;
            if (values == null) return;
            var age = values.Text;
            _ageViewModel.IsAgeMini = !string.IsNullOrEmpty(age);
            CheckValidation();
            
        }

        private void OnAgeMaxi(object sender, TextChangedEventArgs e)
        {
            var values = (Entry)sender;
            if (values == null) return;
            var age = values.Text;
            _ageViewModel.IsAgeMaxi = !string.IsNullOrEmpty(age);
            CheckValidation();
        }


        private void CheckValidation()
        {
            XNameValidated.IsEnabled = _ageViewModel.IsAllAge;

            if (!_ageViewModel.IsAgeMini && !_ageViewModel.IsAgeMaxi) return;
            XNameAgeAlert.IsVisible = NumberHandling.IsMiniGreaterThan(_ageViewModel.AgeMini, _ageViewModel.AgeMaxi);
            XNameValidated.IsEnabled = !XNameAgeAlert.IsVisible;
            _ageViewModel.IsAgeValid = XNameValidated.IsEnabled;

        }
    }
}