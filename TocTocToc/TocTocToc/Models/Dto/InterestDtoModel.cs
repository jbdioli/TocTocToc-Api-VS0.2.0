using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class InterestDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("interest")]
    public string Interest { get; set; }
}