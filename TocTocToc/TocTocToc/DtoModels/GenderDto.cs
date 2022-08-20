using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class GenderDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}