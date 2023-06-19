using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class EPayOrderDtoModel
{

    public EPayOrderDtoModel()
    {
        OrderLines = new List<EPayOrderLinesDtoModel>();
        BillingAddress = new EPayAddressDtoModel();
        ShippingAddress = new EPayAddressDtoModel();
    }

    // Order
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("orderId")]
    public string OrderId { get; set; }

    [JsonProperty("idUsers")]
    public int IdUsers { get; set; }

    [JsonProperty("idBillAddress")]
    public int IdBillAddress { get; set; }

    [JsonProperty("isShipAddress")]
    public int IdShipAddress { get; set; }

    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("orderState")]
    public string OrderState { get; set; }

    [JsonProperty("invoiceNum")]
    public string InvoiceNum { get; set; }

    [JsonProperty("amountTotalHt")]
    public float AmountTotalHt { get; set; }

    [JsonProperty("amountTotalTtc")]
    public float AmountTotalTtc { get; set; }

    [JsonProperty("amountTotalTva")]
    public float AmountTotalTva { get; set; }

    [JsonProperty("portPriceTtc")]
    public float PortPriceTtc { get; set; }

    [JsonProperty("amountToPay")]
    public float AmountToPay { get; set; }

    [JsonProperty("dateInvoice")]
    public DateTime DateInvoice { get; set; }

    [JsonProperty("dateSendOrder")]
    public DateTime DateSendOrder { get; set; }

    [JsonProperty("dateSendInvoice")]
    public DateTime DateSendInvoice { get; set; }

    // Order lines
    [JsonProperty("orderLines")]
    public List<EPayOrderLinesDtoModel> OrderLines { get; set; }

    // Billing address
    [JsonProperty("billingAddress")]
    public EPayAddressDtoModel BillingAddress { get; set; }

    // Delivery address
    [JsonProperty("shippingAddress")]
    public EPayAddressDtoModel ShippingAddress { get; set; }
}