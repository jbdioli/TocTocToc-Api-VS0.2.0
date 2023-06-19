using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class AuthDtoModel
    {
        [JsonProperty("codeVerifier")]
        public string CodeVerifier { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("sessionState")]
        public string SessionState { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }


    }
}
