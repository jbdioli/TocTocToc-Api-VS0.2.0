using System;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class CityDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("idCounties")]
    public int IdCounties { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }

    [JsonProperty("lat")]
    public double Lat { get; set; }
}