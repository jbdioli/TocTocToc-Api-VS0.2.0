using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto
{
    public class AdvertisingDtoModel : EventArgs
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

        [JsonProperty("area")]
        public AreaSelectedDtoModel Area { get; set; }

        [JsonProperty("idGenders")]
        public int IdGenders { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("interests")]
        public string Interests { get; set; }

        [JsonProperty("interestsDetails")]
        public List<InterestDtoModel> InterestsDetails { get; set; }

        [JsonProperty("ageMini")]
        public int AgeMini { get; set; }

        [JsonProperty("isAllAge")]
        public bool IsAllAge { get; set; }

        [JsonProperty("ageMaxi")]
        public int AgeMaxi { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("isPause")]
        public bool IsPause { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("isPayed")]
        public bool IsPayed { get; set; }

        public bool IsEditMode { get; set; } = false;

        public bool IsImage { get; set; } = false;
    }
}
