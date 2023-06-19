using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class GenderDtoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}