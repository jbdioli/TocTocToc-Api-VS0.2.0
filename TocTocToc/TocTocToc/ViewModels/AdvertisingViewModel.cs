using System;
using System.Collections.Generic;
using PropertyChanged;
using TocTocToc.DtoModels;

namespace TocTocToc.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AdvertisingViewModel
    {
        public string AdvertisingId { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public IList<GenderDto> Genders { get; set; }

        public int IdGenders { get; set; }

        public string Gender { get; set; }

        public int AgeMini { get; set; }

        public int AgeMaxi { get; set; }

        public DateTime Date { get; set; }

        public bool IsPause { get; set; }

        public int Duration { get; set; }

        public int Budget { get; set; }

        public bool IsPayed { get; set; }

        public string FullPathImage { get; set; }
    }
}