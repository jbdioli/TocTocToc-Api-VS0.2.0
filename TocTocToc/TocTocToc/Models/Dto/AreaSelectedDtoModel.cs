using System.Collections.Generic;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class AreaSelectedDtoModel
{

    [JsonProperty("country")]
    public CountryDtoModel CountrySelected { get; set; } = new();

    [JsonProperty("states")]
    public List<StateDtoModel> StatesSelected { get; set; } = new();

    [JsonProperty("counties")]
    public List<CountyDtoModel> CountiesSelected { get; set; } = new();

    [JsonProperty("cities")]
    public List<CityDtoModel> CitiesSelected { get; set; } = new();

    [JsonProperty("isAllCountry")]
    public bool IsAllCountry { get; set; } = false;

    [JsonProperty("isAllState")]
    public bool IsAllState { get; set; } = false;

    [JsonProperty("isAllCounty")]
    public bool IsAllCounty { get; set; } = false;

    [JsonProperty("isAllCity")]
    public bool IsAllCity { get; set; } = false;

    [JsonProperty("Km")]
    public double Km { get; set; } = new();

}