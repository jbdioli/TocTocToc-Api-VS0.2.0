using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class UserInfoDtoModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("contactBookId")]
        public string ContactBookId { get; set; }

        [JsonProperty("blogId")]
        public string BlogId { get; set; }

        [JsonProperty("serviceExchangeId")]
        public string ServiceExchangeId { get; set; }

        [JsonProperty("settingId")]
        public string SettingId { get; set; }
    }
}