using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class ExpiredTokensDto
    {
        [JsonProperty("isExpiredToken")]
        public bool IsExpiredToken { get; set; }

        [JsonProperty("isExpiredRefreshToken")]
        public bool IsExpiredRefreshToken { get; set; }
    }
}
