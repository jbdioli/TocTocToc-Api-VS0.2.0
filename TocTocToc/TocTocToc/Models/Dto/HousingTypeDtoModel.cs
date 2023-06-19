using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class HousingTypeDtoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("type")]
        public string Type { get; set; }
    }
}