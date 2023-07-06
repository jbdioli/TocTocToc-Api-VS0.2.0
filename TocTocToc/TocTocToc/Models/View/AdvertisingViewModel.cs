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

        public string Image { get; set; }           // needed

        public string Path { get; set; }            // needed

        public string Name { get; set; }            // needed

        public string Info { get; set; }

        public AreaSelectedViewModel Area { get; set; }

        public int IdGenders { get; set; }

        public string Gender { get; set; }          // needed

        public bool IsAllAge { get; set; }          // needed

        public string AgeMini { get; set; }         // needed

        public string AgeMaxi { get; set; }         // needed

        public string Interests { get; set; }       // needed

        public List<InterestViewModel> InterestsDetails { get; set; }

        public DateTime Date { get; set; }

        public bool IsPause { get; set; }           // needed

        public string Duration { get; set; }

        public string Budget { get; set; }          // needed

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