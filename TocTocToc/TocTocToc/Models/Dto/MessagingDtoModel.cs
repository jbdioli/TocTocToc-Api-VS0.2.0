using System.Collections.Generic;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class MessagingDtoModel
{
    [JsonProperty("messagingId")]
    public string MessagingId { get; set; }

    [JsonProperty("userOwnerId")]
    public string UserOwnerId { get; set; }

    [JsonProperty("isGroup")]
    public bool IsGroup { get; set; }
    
    [JsonProperty("messages")]
    public List<MessageDtoModel> Messages { get; set; }

}