using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{
    public partial class UserModel : LanguageViewModel
    {

        public UserModel()
        {
            _addresses = new List<Model.AddressModel>();
        }

        [ObservableProperty]
        private string _userId;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        private string _firstname;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        private string _lastname;

        [ObservableProperty]
        private string _pseudo;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private string _birthday;

        [ObservableProperty]
        private string _company;

        [ObservableProperty]
        private int _idGenders;

        [ObservableProperty]
        private string _gender;

        [ObservableProperty]
        private string _languages;

        [ObservableProperty]
        private int _idMaritalStatus;

        [ObservableProperty]
        private string _maritalStatus;

        [ObservableProperty]
        private string _path;

        [ObservableProperty]
        private string _photo;

        [ObservableProperty]
        private string _job;

        [ObservableProperty]
        private string _interests;

        [ObservableProperty]
        private bool _isFloor;

        [ObservableProperty]
        private bool _isAge;

        [ObservableProperty]
        private bool _isStatus;

        [ObservableProperty]
        private bool _isJob;

        [ObservableProperty]
        private bool _isApartmentNumber;

        [ObservableProperty]
        private List<Model.AddressModel> _addresses;

        [ObservableProperty]
        private string _fullPathPhoto;

        public string Name => $"{_firstname} {_lastname}";
    }
}