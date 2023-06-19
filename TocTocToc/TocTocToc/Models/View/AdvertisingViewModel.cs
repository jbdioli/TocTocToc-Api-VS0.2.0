using System;
using System.Collections.Generic;
using PropertyChanged;

namespace TocTocToc.Models.View
{
    [AddINotifyPropertyChangedInterface]
    public class AdvertisingViewModel
    {
        public bool IsDownloadingData { get; set; }

        public string AdvertisingId { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public AreaSelectedViewModel Area { get; set; }

        public int IdGenders { get; set; }

        public string Gender { get; set; }

        public bool IsAllAge { get; set; }

        public string AgeMini { get; set; }

        public string AgeMaxi { get; set; }

        public string Interests { get; set; }

        public List<InterestViewModel> InterestsDetails { get; set; }

        public DateTime Date { get; set; }

        public bool IsPause { get; set; }

        public string Duration { get; set; }

        public string Budget { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsPayed { get; set; }

        public string FullPathImage { get; set; }

        public bool IsAgeValid { get; set; } = false;

        public bool IsGender { get; set; } = false;

        public bool IsName { get; set; } = false;

        public bool IsDuration { get; set; } = false;

        public bool IsBudget { get; set; } = false;

        public bool IsArea { get; set; } = false;

        public bool IsInterest { get; set; } = false;

        public bool IsImage { get; set; } = false;

        public bool IsArticleValid { get; set; } = false;

        public bool IsEditMode { get; set; } = false;

        public bool IsSaved { get; set; } = false;
    }
}