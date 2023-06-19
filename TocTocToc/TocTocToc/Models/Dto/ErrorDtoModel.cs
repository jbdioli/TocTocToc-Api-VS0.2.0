
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class ErrorDtoModel
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("resolution")]
    public string Resolution { get; set; }

    [JsonProperty("timestamp")]
    public string Timestamp { get; set; }

    [JsonProperty("status")]
    public object Status { get; set; }

    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty("reasonPhrase")]
    public string ReasonPhrase { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("debugMessage")]
    public string DebugMessage { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }
}

//     [JsonProperty("statusCode")]