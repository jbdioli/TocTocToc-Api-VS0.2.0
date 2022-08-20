using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TocTocToc.DtoModels
{
    public class AdvertisingDto : EventArgs
    {
        [JsonProperty("advertisingId")]
        public string AdvertisingId { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("genders")]
        public IList<GenderDto> Genders { get; set; }

        [JsonProperty("idGender")]
        public int IdGender { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("ageMini")]
        public int AgeMini { get; set; }

        [JsonProperty("ageMaxi")]
        public int AgeMaxi { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("isPause")]
        public bool IsPause { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("isPayed")]
        public bool IsPayed { get; set; }
    }
}
