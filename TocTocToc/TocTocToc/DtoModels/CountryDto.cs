using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class CountryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("isoAlpha2")]
        public string IsoAlpha2 { get; set; }

        [JsonProperty("phoneCode")]
        public int PhoneCode { get; set; }
    }
}