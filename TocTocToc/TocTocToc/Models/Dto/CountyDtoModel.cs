using Newtonsoft.Json;
using System;

namespace TocTocToc.Models.Dto;

public class CountyDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("idStates")]
    public int IdStates { get; set; }

    [JsonProperty("county")]
    public string County { get; set; }
}