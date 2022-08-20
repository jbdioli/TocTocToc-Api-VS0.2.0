
using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class MaritalStatusDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("maritalStatus")]
        public string MaritalStatus { get; set; }
    }
}