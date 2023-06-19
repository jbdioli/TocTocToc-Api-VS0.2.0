using System;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class EPayAddressDtoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("firstname")]
    public string Firstname { get; set; }

    [JsonProperty("lastname")]
    public string Lastname { get; set; }

    [JsonProperty("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("company")]
    public string Company { get; set; }

    [JsonProperty("address1")]
    public string Address1 { get; set; }

    [JsonProperty("address2")]
    public string Address2 { get; set; }

    [JsonProperty("address3")]
    public string Address3 { get; set; }

    [JsonProperty("idCities")]
    public int IdCities { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("zipCode")]
    public string Zipcode { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("idCountries")]
    public int IdCountries { get; set; }

    public string Name => $"{Firstname} {Lastname}";
}