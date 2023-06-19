using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class EPayOrderLinesDtoModel
{
    [JsonProperty("idOrders")]
    public int IdOrders { get; set; }

    [JsonProperty("productRef")]
    public string ProductRef { get; set; }

    [JsonProperty("wording")]
    public string Wording { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("priceUnitHt")]
    public float PriceUnitHt { get; set; }

    [JsonProperty("priceUnitTtc")]
    public float PriceUnitTtc { get; set; }

    [JsonProperty("priceBaseHt")]
    public float PriceBaseHt { get; set; }

    [JsonProperty("priceBaseTtc")]
    public float PriceBaseTtc { get; set; }

    [JsonProperty("tvaRate")]
    public float TvaRate { get; set; }

    [JsonProperty("discountPercent")]
    public int DiscountPercent { get; set; }

}