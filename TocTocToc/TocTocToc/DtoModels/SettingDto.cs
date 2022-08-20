using Newtonsoft.Json;

namespace TocTocToc.DtoModels;

public class SettingDto
{
    [JsonProperty("settingId")]
    public string SettingId { get; set; }

    [JsonProperty("idCountries")]
    public int IdCountries { get; set; }

    [JsonProperty("isAge")]
    public bool IsAge { get; set; }

    [JsonProperty("isFloor")]
    public bool IsFloor { get; set; }

    [JsonProperty("isStatus")]
    public bool IsStatus { get; set; }

    [JsonProperty("isJob")]
    public bool IsJob { get; set; }
}