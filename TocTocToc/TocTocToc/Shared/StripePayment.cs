using System;
using System.Threading.Tasks;
using Stripe;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared
{
    public class StripePayment
    {
        private readonly EPayDetailsDtoModel _ePayDetails;

        private const string Key = "sk_test_51LdV8THuyqtQ6bYH4iprD861XwFzXHSTmhYOMDQEkTnLbHAgpFujUODb077mMuXn9XDr4xjml6jAvHxovytPp8Rp001wVOks6C";
        private readonly TokenCardOptions _cardDetails = new();
        private Token _token = new();
        private Source _source = new();
        private Customer _customer = new();
        private ChargeCreateOptions _chargeOptions;

        public StripePayment(EPayDetailsDtoModel ePayDetails)
        {
            _ePayDetails = ePayDetails;
            StripeConfiguration.ApiKey = Key;

        }


        public async Task<(EPayDetailsDtoModel, StripeError)> ProcessToPayment()
        {
            InitCard();
            var error = await InitToken();
            if (!string.IsNullOrEmpty(error.Type))
            {
                _ePayDetails.EPayPayment.Status = "fail";
                _ePayDetails.IsEPayValid = false;
                _ePayDetails.EPayPayment.IsPayed = false;
                return (_ePayDetails, error);
            }
            InitSource();
            CreateCustomer();
            InitCharge();
            Pay();

            return (_ePayDetails, error);
        }

        private void InitCard()
        {
            _cardDetails.Number = _ePayDetails.EPayPayment.CardNo;
            _cardDetails.ExpMonth = _ePayDetails.EPayPayment.ExpMonth;
            _cardDetails.ExpYear = _ePayDetails.EPayPayment.ExpYear;
            _cardDetails.Cvc = _ePayDetails.EPayPayment.CardCvv;
            _cardDetails.Currency = _ePayDetails.EPayPayment.Currency;
            _cardDetails.Name = _ePayDetails.EPayOrder.BillingAddress.Name;
            _cardDetails.AddressLine1 = _ePayDetails.EPayOrder.BillingAddress.Address1;
            _cardDetails.AddressLine2 = _ePayDetails.EPayOrder.BillingAddress.Address2;
            _cardDetails.AddressZip = _ePayDetails.EPayOrder.BillingAddress.Zipcode;
            _cardDetails.AddressCity = _ePayDetails.EPayOrder.BillingAddress.City;
            _cardDetails.AddressState = _ePayDetails.EPayOrder.BillingAddress.State;
            _cardDetails.AddressCountry = _ePayDetails.EPayOrder.BillingAddress.Country;
        }

        private async Task<StripeError> InitToken()
        {
            var tokenService = new TokenService();
            var error = new StripeError();

            var tokenOptions = new TokenCreateOptions
            {
                Card = _cardDetails
            };

            try
            {
                _token = await tokenService.CreateAsync(tokenOptions);
            }
            catch (StripeException e)
            {

                error.Message = e.StripeError.Message;
                error.Type = e.StripeError.Type;

                return error;
            }

            return error;

        }

        private void InitSource()
        {
            if (string.IsNullOrEmpty(_ePayDetails.EPayPayment.Currency))
                throw new Exception("[ Error in Module StripePayment ] : currency can't be null or empty");
            
            var sourceService = new SourceService();

            var option = new SourceCreateOptions
            {
                Type = SourceType.Card,
                Currency = _ePayDetails.EPayPayment.Currency,
                Token = _token.Id
            };

            _source = sourceService.Create(option);
        }

        private void CreateCustomer()
        {
            var customerService = new CustomerService();

            var customer = new CustomerCreateOptions
            {
                Name = _ePayDetails.EPayOrder.BillingAddress.Name,
                Email = _ePayDetails.EPayOrder.BillingAddress.Email,
                Description = _ePayDetails.EPayPayment.Description,
                Phone = _ePayDetails.EPayOrder.BillingAddress.PhoneNumber,
                Address = new AddressOptions
                {
                    City = _ePayDetails.EPayOrder.BillingAddress.City,
                    State = _ePayDetails.EPayOrder.BillingAddress.State,
                    Country = _ePayDetails.EPayOrder.BillingAddress.Country,
                    PostalCode = _ePayDetails.EPayOrder.BillingAddress.Zipcode,
                    Line1 = _ePayDetails.EPayOrder.BillingAddress.Address1,
                    Line2 = _ePayDetails.EPayOrder.BillingAddress.Address2
                }
            };

            _customer = customerService.Create(customer);
        }

        private void InitCharge()
        {
            _chargeOptions = new ChargeCreateOptions
            {
                Amount = _ePayDetails.EPayPayment.Amount * 100,
                Currency = _ePayDetails.EPayPayment.Currency,
                Description = _ePayDetails.EPayPayment.Description,
                ReceiptEmail = _ePayDetails.EPayOrder.BillingAddress.Email,
                Customer = _customer.Id,
                Source = _source.Id,
            };
        }

        private async void Pay()
        {
            var chargeService = new ChargeService();

            //var charge = chargeService.Create(_chargeOptions);
            var buffer = chargeService.CreateAsync(_chargeOptions);
            var charge = await buffer;

            _ePayDetails.EPayPayment.IsPayed = charge.Status == "succeeded";
            _ePayDetails.EPayPayment.Status = charge.Status;

            _ePayDetails.EPayPayment.TransactionId = charge.Id;
            _ePayDetails.EPayPayment.Invoice = charge.Invoice;
            _ePayDetails.EPayPayment.InvoiceId = charge.InvoiceId;
            _ePayDetails.EPayPayment.PaymentMethod = charge.PaymentMethod;
            _ePayDetails.EPayPayment.CustomerId = charge.CustomerId;
            _ePayDetails.EPayPayment.BalanceTransactionId = charge.BalanceTransactionId;


            _ePayDetails.EPayPayment.ReceiptUrl = charge.ReceiptUrl;
        }


    }
}