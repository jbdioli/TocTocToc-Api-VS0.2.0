using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class ItemDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("idParents")]
    public int IdParents { get; set; }
    [JsonProperty("item")]
    public string Item { get; set; }
}