using System;
using System.Globalization;
using Newtonsoft.Json;
using Stripe;

namespace TocTocToc.Models.Dto;

public class EPayPaymentDtoModel
{
    public EPayPaymentDtoModel()
    {
        Invoice = new Invoice();

        var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.Name);
        Currency = regionInfo.ISOCurrencySymbol;
    }

    [JsonProperty("idOrder")]
    public int IdOrders { get; set; }

    [JsonProperty("refsfp")]
    public string Refsfp { get; set; }

    [JsonProperty("cardNo")]
    public string CardNo { get; set; }

    [JsonProperty("expMonth")]
    public string ExpMonth { get; set; }

    [JsonProperty("expYear")]
    public string ExpYear { get; set; }

    [JsonProperty("cardCvv")]
    public string CardCvv { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("transactionId")]
    public string TransactionId { get; set; }

    [JsonProperty("paymentMethod")]
    public string PaymentMethod { get; set; }

    [JsonProperty("customerId")]
    public string CustomerId { get; set; }

    [JsonProperty("invoice")]
    public Invoice Invoice { get; set; }

    [JsonProperty("invoiceId")]
    public string InvoiceId { get; set; }

    [JsonProperty("balanceTransactionId")]
    public string BalanceTransactionId { get; set; }

    [JsonProperty("receiptUrl")]
    public string ReceiptUrl { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("isPayed")]
    public bool IsPayed { get; set; } = false;


}