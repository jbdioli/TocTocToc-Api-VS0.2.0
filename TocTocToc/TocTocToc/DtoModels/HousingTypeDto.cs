using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class HousingTypeDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("type")]
        public string Type { get; set; }
    }
}