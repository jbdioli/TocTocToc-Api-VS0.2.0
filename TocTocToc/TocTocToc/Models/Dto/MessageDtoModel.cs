using System;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class MessageDtoModel
{
    [JsonProperty("messageId")]
    public string MessageId { get; set; }

    [JsonProperty("user")]
    public UserDtoModel User { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}