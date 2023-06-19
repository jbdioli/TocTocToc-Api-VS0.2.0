using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class AddressDtoModel
    {
        [JsonProperty("addressId")]
        public string AddressId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("idHousingTypes")]
        public int IdHousingTypes { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonProperty("residenceName")]
        public string ResidenceName { get; set; }

        [JsonProperty("buildingNumber")]
        public string BuildingNumber { get; set; }

        [JsonProperty("buildingName")]
        public string BuildingName { get; set; }

        [JsonProperty("buildingEntrance")]
        public string BuildingEntrance { get; set; }

        [JsonProperty("floor")]
        public int Floor { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("idCountries")]
        public int IdCountries { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("idStates")]
        public int IdStates { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("idCounties")]
        public int IdCounties { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("idCities")]
        public int IdCities { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("distanceWanted")]
        public float DistanceWanted { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

    }
}
