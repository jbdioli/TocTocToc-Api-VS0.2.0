using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{
    public partial class AdvertisingModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isDownloadingData;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdPublic))]
        private string _advertisingId;
        
        [ObservableProperty]
        private string _image;            // needed
        
        [ObservableProperty]
        private string _path;             // needed
        
        [ObservableProperty]
        private string _name;            // needed
        
        [ObservableProperty]
        private string _info;
        
        [ObservableProperty]
        private AreaSelectedModel _area;
        
        [ObservableProperty]
        private int _idGenders;
        
        [ObservableProperty]
        private string _gender;          // needed

        [ObservableProperty]
        private ItemDtoModel _selectedItemGender;
        
        [ObservableProperty]
        private bool _isAllAge;          // needed
        
        [ObservableProperty]
        private string _ageMini;         // needed
        
        [ObservableProperty]
        private string _ageMaxi;         // needed
        
        [ObservableProperty]
        private string _interests;       // needed
        
        [ObservableProperty]
        private List<InterestModel> _interestsDetails;
        
        [ObservableProperty]
        private DateTime _date;
        
        [ObservableProperty]
        private bool _isPause;           // needed
        
        [ObservableProperty]
        private string _duration;
        
        [ObservableProperty]
        private string _budget;         // needed
        
        [ObservableProperty]
        private DateTime _startDate;
        
        [ObservableProperty]
        private bool _isPayed;
        
        [ObservableProperty]
        private string _fullPathImage;

        [ObservableProperty]
        private bool _isEditMode = false;

        public bool IsIdPublic => !string.IsNullOrWhiteSpace(AdvertisingId);
    }
}