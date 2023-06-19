using System;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class StateDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("idCountries")]
    public int IdCountries { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }
}