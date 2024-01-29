using System;
using TocTocToc.Models.Model;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgePopup : Xamarin.CommunityToolkit.UI.Views.Popup<AgeModel>
    {

        private readonly AgeModel _ageModel = new();

        public AgePopup(AgeModel age)
        {
            InitializeComponent();
            XNameValidated.IsEnabled = false;
            XNameAgeAlert.IsVisible = false;
            if (age != null)
            {
                _ageModel = age;
                if (age.IsAllAge && !string.IsNullOrEmpty(age.AgeMini) && !string.IsNullOrEmpty(age.AgeMaxi))
                 XNameValidated.IsEnabled = true;
                XNameIsAllAge.IsChecked = _ageModel.IsAllAge;
            }
            BindingContext = _ageModel;
        }

        private void OnIsAllAge(object sender, CheckedChangedEventArgs e)
        {
            if (sender is not CheckBox checkBox) return;
            var isChecked = checkBox.IsChecked;
            _ageModel.IsAllAge = isChecked;

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

            // _ageModel.IsAllAge = XNameIsAllAge.IsChecked;
        }




        private void OnValidated(object sender, EventArgs e)
        {
            Dismiss(_ageModel);
        }

        private void OnAgeMini(object sender, TextChangedEventArgs e)
        {
            var values = (Entry)sender;
            if (values == null) return;
            var age = values.Text;
            _ageModel.IsAgeMini = !string.IsNullOrEmpty(age);
            CheckValidation();
            
        }

        private void OnAgeMaxi(object sender, TextChangedEventArgs e)
        {
            var values = (Entry)sender;
            if (values == null) return;
            var age = values.Text;
            _ageModel.IsAgeMaxi = !string.IsNullOrEmpty(age);
            CheckValidation();
        }


        private void CheckValidation()
        {
            XNameValidated.IsEnabled = _ageModel.IsAllAge;

            if (!_ageModel.IsAgeMini && !_ageModel.IsAgeMaxi) return;
            XNameAgeAlert.IsVisible = NumberHandling.IsMiniGreaterThan(_ageModel.AgeMini, _ageModel.AgeMaxi);
            XNameValidated.IsEnabled = !XNameAgeAlert.IsVisible;
            _ageModel.IsAgeValid = XNameValidated.IsEnabled;

        }
    }
}