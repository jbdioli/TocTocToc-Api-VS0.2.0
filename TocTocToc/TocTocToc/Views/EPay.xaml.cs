using System;
using System.ComponentModel;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Popup;
using TocTocToc.Shared;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EPay : ContentPage
    {
        private static readonly EPayDetailsViewModel EPayDetailsView = new();
        private static StripePayment _stripePayment;

        public EPay(EPayDetailsDtoModel ePayDetailsDto)
        {
            InitializeComponent();

            InitEntries();

            CopyModel.EPayDetailsCopyDtoToViewModel(ePayDetailsDto, EPayDetailsView);

            BindingContext = EPayDetailsView;
        }

        private void InitEntries()
        {
            XNameCardNumber.MaxLength = 16;
            XNameExpireYear.MaxLength = 2;
            XNameExpireMonth.MaxLength = 2;
            XNameCardCvv.MaxLength = 3;
        }

        private void OnCardNumber(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;
            
            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);

            if (XNameCardNumber.CursorPosition == 16)
                XNameExpireMonth.CursorPosition = 1;
        }

        private void OnExpMonth(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);

            if (XNameExpireMonth.CursorPosition == 2)
                XNameExpireYear.CursorPosition = 1;
        }

        private void OnExpYear(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);



            if (XNameExpireYear.CursorPosition == 2)
                XNameCardCvv.CursorPosition = 1;
        }

        private void OnCardCvv(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnFirstname(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnLastname(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnEmail(object sender, PropertyChangedEventArgs e)
        {
            XNameEmail.Keyboard = Keyboard.Email;

            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnPhoneNumber(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnAddress(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnZipcode(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnCity(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnState(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }

        private void OnCountry(object sender, PropertyChangedEventArgs e)
        {
            if (EPayDetailsView == null) return;

            EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);
        }


        private bool IsValid(EPayDetailsViewModel EPayDetails)
        {
            if (EPayDetails == null) return false;

            EPayDetails.IsCardNumber = !string.IsNullOrEmpty(EPayDetails.EPayPayment.CardNo) && (EPayDetails.EPayPayment.CardNo.Length == XNameCardNumber.MaxLength);
            EPayDetails.IsExpMonth = !string.IsNullOrEmpty(EPayDetails.EPayPayment.ExpMonth) && (EPayDetails.EPayPayment.ExpMonth.Length == XNameExpireMonth.MaxLength);
            EPayDetails.IsExpYear = !string.IsNullOrEmpty(EPayDetails.EPayPayment.ExpYear) && (EPayDetails.EPayPayment.ExpYear.Length == XNameExpireYear.MaxLength);
            EPayDetails.IsCardCvv = !string.IsNullOrEmpty(EPayDetails.EPayPayment.CardCvv) && (EPayDetails.EPayPayment.CardCvv.Length == XNameCardCvv.MaxLength);

            EPayDetails.IsFirstname = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Firstname);
            EPayDetails.IsLastname = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Lastname);
            EPayDetails.IsEmail = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Email);
            EPayDetails.IsPhoneNumber = !string.IsNullOrWhiteSpace(EPayDetails.EPayOrder.BillingAddress.PhoneNumber);

            EPayDetails.IsAddress1 = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Address1);
            EPayDetails.IsZipcode = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Zipcode);
            EPayDetails.IsCity = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.City);
            EPayDetails.IsState = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.State);
            EPayDetails.IsCountry = !string.IsNullOrEmpty(EPayDetails.EPayOrder.BillingAddress.Country);

            EPayDetails.IsEPayValid = (EPayDetails.IsCardNumber && EPayDetails.IsExpMonth && EPayDetails.IsExpYear && EPayDetails.IsCardCvv &&
                                EPayDetails.IsFirstname && EPayDetails.IsLastname && EPayDetails.IsEmail && EPayDetails.IsPhoneNumber &&
                                EPayDetails.IsAddress1 && EPayDetails.IsZipcode && EPayDetails.IsCity && EPayDetails.IsState && EPayDetails.IsCountry);

            return EPayDetails.IsEPayValid;
        }

        private void OnCardNumberEnterKey(object sender, EventArgs e)
        {
            XNameExpireMonth.CursorPosition = 1;
        }

        private void OnExpMonthEnterKey(object sender, EventArgs e)
        {
            XNameExpireYear.CursorPosition = 1;
        }

        private void OnExpYearEnterKey(object sender, EventArgs e)
        {
            XNameCardCvv.CursorPosition = 1;
        }

        private void OnCardCvvEnterKey(object sender, EventArgs e)
        {
            XNameFirstname.CursorPosition = 1;
        }

        private void OnFirstNameEnterKey(object sender, EventArgs e)
        {
            XNameLastname.CursorPosition = 1;
        }

        private void OnLastnameEnterKey(object sender, EventArgs e)
        {
            XNameEmail.CursorPosition = 1;
        }

        private void OnEmailEnterKey(object sender, EventArgs e)
        {
            XNamePhoneNumber.CursorPosition = 1;
        }

        private void OnPhoneNumberEnterKey(object sender, EventArgs e)
        {
            XNameAddress1.CursorPosition = 1;
        }

        private void OnAddress1EnterKey(object sender, EventArgs e)
        {
            XNameAddress2.CursorPosition = 1;
        }

        private void OnAddress2EnterKey(object sender, EventArgs e)
        {
            XNameZipcode.CursorPosition = 1;
        }

        private void OnZipcodeEnterKey(object sender, EventArgs e)
        {
            XNameCity.CursorPosition = 1;
        }

        private void OnCityEnterKey(object sender, EventArgs e)
        {
            XNameState.CursorPosition = 1;
        }

        private void OnStateEnterKey(object sender, EventArgs e)
        {
            XNameCountry.CursorPosition = 1;
        }

        private void OnEmailFocused(object sender, FocusEventArgs e)
        {
            //Application.Current.On<Android>().ShouldPreserveKeyboardOnResume(true);


        }

        private async void OnPaying(object sender, EventArgs e)
        {
            //EPayDetailsView.IsEPayValid = IsValid(EPayDetailsView);

            var ePayDetails = new EPayDetailsDtoModel();

            CopyModel.EPayDetailsCopyViewModelToDto(EPayDetailsView, ePayDetails);
            _stripePayment = new StripePayment(ePayDetails);
            var (ePayDetailsUpdated, error) = await _stripePayment.ProcessToPayment();
            if (ePayDetailsUpdated.EPayPayment.IsPayed)
            {
                Navigation.ShowPopup(new EPayInvoicePopup(ePayDetailsUpdated.EPayPayment.ReceiptUrl));
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert($"Payment {ePayDetailsUpdated.EPayPayment.Status}", error.Message, "OK"); 
            }
        }



    }
}