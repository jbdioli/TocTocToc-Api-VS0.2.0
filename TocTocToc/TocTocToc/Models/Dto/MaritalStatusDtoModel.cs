
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class MaritalStatusDtoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("maritalStatus")]
        public string MaritalStatus { get; set; }
    }
}