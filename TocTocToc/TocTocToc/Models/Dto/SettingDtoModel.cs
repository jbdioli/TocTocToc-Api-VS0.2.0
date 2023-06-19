using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class SettingDtoModel
{
    [JsonProperty("settingId")]
    public string SettingId { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("isAge")]
    public bool IsAge { get; set; }

    [JsonProperty("isFloor")]
    public bool IsFloor { get; set; }

    [JsonProperty("isStatus")]
    public bool IsStatus { get; set; }

    [JsonProperty("isJob")]
    public bool IsJob { get; set; }
}