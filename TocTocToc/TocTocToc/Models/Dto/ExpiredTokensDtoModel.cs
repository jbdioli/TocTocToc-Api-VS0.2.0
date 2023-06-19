using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class ExpiredTokensDtoModel
    {
        [JsonProperty("isExpiredToken")]
        public bool IsExpiredToken { get; set; }

        [JsonProperty("isExpiredRefreshToken")]
        public bool IsExpiredRefreshToken { get; set; }
    }
}
