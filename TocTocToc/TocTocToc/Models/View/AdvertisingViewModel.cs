using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View
{
    public partial class AdvertisingViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isDownloadingData;

        [ObservableProperty]
        private string _advertisingId;
        
        [ObservableProperty]
        private string _image;           // needed

        [ObservableProperty]
        private string _path;            // needed

        [ObservableProperty]
        private string _name;            // needed

        [ObservableProperty]
        private string _info;

        [ObservableProperty]
        private AreaSelectedViewModel _area;

        [ObservableProperty]
        private int _idGenders;

        [ObservableProperty]
        private string _gender;          // needed

        [ObservableProperty]
        private bool _isAllAge;          // needed

        [ObservableProperty]
        private string _ageMini;         // needed

        [ObservableProperty]
        private string _ageMaxi;         // needed

        [ObservableProperty]
        private string _interests;       // needed

        [ObservableProperty]
        private List<InterestViewModel> _interestsDetails;

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
        private bool _isAgeValid = false;

        [ObservableProperty]
        private bool _isGender = false;

        [ObservableProperty]
        private bool _isName = false;

        [ObservableProperty]
        private bool _isDuration = false;

        [ObservableProperty]
        private bool _isBudget = false;

        [ObservableProperty]
        private bool _isArea = false;

        [ObservableProperty]
        private bool _isInterest = false;

        [ObservableProperty]
        private bool _isImage = false;

        [ObservableProperty]
        private bool _isArticleValid = false;

        [ObservableProperty]
        private bool _isEditMode = false;

        [ObservableProperty]
        private bool _isSaved = false;
    }
}