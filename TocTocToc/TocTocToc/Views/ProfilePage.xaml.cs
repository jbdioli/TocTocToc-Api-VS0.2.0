using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TocTocToc.DtoModels;
using TocTocToc.Services;
using TocTocToc.Shared;
using TocTocToc.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly UserStorageService _userStorageService;
        private readonly ItemsStorageService _itemsStorageService;
        private UserDto _userDto;
        private UserViewModel _userViewModel;
        private bool _isLastname;
        private bool _isFirstname;
        private readonly string _userId;

        public ProfilePage()
        {
            InitializeComponent();

            _userStorageService = new UserStorageService();
            _itemsStorageService = new ItemsStorageService();
            _userId = LocalStorageService.GetUserId();

            GetUserProfile();
        }

        private async void GetUserProfile()
        {
            _userDto = new UserDto();
            _userViewModel = new UserViewModel();

            var settingId = LocalStorageService.GetSettingId();

            _userDto = await _userStorageService.GetUserDetails(_userId);

            CopyToViewModel(_userViewModel, _userDto);

            // DuckCopyShallow(_userViewModel, data);
            _userViewModel = await _itemsStorageService.SetGenders(_userViewModel);
            _userViewModel = await _itemsStorageService.SetMaritalStatuses(_userViewModel);

            SetupPicker();
            InitForm();
            BindingContext = _userViewModel;
        }

        private void InitForm()
        {
            _isFirstname = false;
            _isLastname = false;
            XNameSaveButton.IsEnabled = false;

            _userViewModel.FullPathPhoto = WebConstants.Url + _userViewModel.Path + _userViewModel.Photo;

            if (_userViewModel.IdGenders != 0)
                XNameGenderPicker.SelectedItem = ((List<GenderDto>)XNameGenderPicker.ItemsSource).FirstOrDefault(element => element.Id == _userViewModel.IdGenders);

            if( _userViewModel.IdMaritalStatus != 0)
                XNameMaritalStatusPicker.SelectedItem = ((List<MaritalStatusDto>)XNameMaritalStatusPicker.ItemsSource).FirstOrDefault(element => element.Id == _userViewModel.IdMaritalStatus);

            XNameOnDatePicker.Date = String.IsNullOrEmpty(_userViewModel.Birthday) ? DateTime.Now : ConvertStringDateToDate(_userViewModel.Birthday);
        }

        private static DateTime ConvertStringDateToDate(string date)
        {
            var dateStrings = date.Split('-');
            var dateInts = new int[dateStrings.Length];
            for (var i = 0; i < dateStrings.Length; i++)
            {
                dateInts[i] = int.Parse(dateStrings[i]);
            }
            var returnDateTime = new DateTime(dateInts[0], dateInts[1], dateInts[2]);

            return returnDateTime;
        }

        private void SetupPicker()
        {
            XNameGenderPicker.ItemsSource = (System.Collections.IList)_userViewModel.Genders;
            XNameMaritalStatusPicker.ItemsSource = (System.Collections.IList)_userViewModel.MaritalStatuses;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            var data = new UserDto();
            _userViewModel.Birthday = GetBirthday();

            CopyToDto(_userViewModel, data);

            var userDto = await _userStorageService.UpdateUser<UserDto>(_userId, data);
            CopyToViewModel(_userViewModel, userDto);

            InitForm();
            BindingContext = _userViewModel;

        }


        private async void OnAddPhoto(object sender, EventArgs e)
        {
            // var user = new UserDto();
            // BindingContext = user;

            var file = await FilePicker.PickAsync(new PickOptions()
            {
               FileTypes = FilePickerFileType.Images,
               PickerTitle = "Pick an Image"
            });

            if (file == null) return;

            var fileName = file.FileName;
            var path = file.FullPath;
            var stream = await file.OpenReadAsync();
            var photo = ImageSource.FromStream(() => stream);
            _userViewModel.Photo = path;
            _userViewModel.FullPathPhoto = path;
            await _userStorageService.PostPhoto(_userId, _userViewModel.Photo);
            BindingContext = _userViewModel;
        }

        private void OnGender(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            var genderDetails = (GenderDto)picker.SelectedItem;
            _userViewModel.IdGenders = genderDetails.Id;

        }

        //private void OnLanguages(object sender, EventArgs e)
        //{

        //}

        private async void OnAddresses(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressPage());
        }

        private string GetBirthday()
        {
            var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            var birthday = XNameOnDatePicker.Date.ToString(isoDateTimeFormat.UniversalSortableDateTimePattern);
            birthday = birthday.Remove(10);
            return birthday;
        }

        private void OnMaritalStatus(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            var maritalStatusDetails = (MaritalStatusDto)picker.SelectedItem;
            _userViewModel.IdMaritalStatus = maritalStatusDetails.Id;
        }


        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(_userViewModel.Lastname))
                _isLastname = true;
            if (!String.IsNullOrEmpty(_userViewModel.Firstname))
                _isFirstname = true;

            var isAddresses = LocalStorageService.IsAddresses();

            if (_isLastname && _isFirstname && isAddresses)
                XNameSaveButton.IsEnabled = true;

        }


        public static void DuckCopyShallow(object dst, object src)
        {
            var srcT = src.GetType();
            var dstT = dst.GetType();
            foreach (var f in srcT.GetFields())
            {
                var dstF = dstT.GetField(f.Name);
                if (dstF == null || dstF.IsLiteral)
                    continue;
                dstF.SetValue(dst, f.GetValue(src));
            }

            foreach (var f in srcT.GetProperties())
            {
                var dstF = dstT.GetProperty(f.Name);
                if (dstF == null)
                    continue;

                dstF.SetValue(dst, f.GetValue(src, null), null);
            }
        }

        private static void CopyToDto(UserViewModel userViewModel, UserDto data)
        {
            data.UserId = userViewModel.UserId;
            data.Firstname = userViewModel.Firstname;
            data.Lastname = userViewModel.Lastname;
            data.Pseudo = userViewModel.Pseudo;
            data.Email = userViewModel.Email;
            data.Birthday = userViewModel.Birthday;
            data.Languages = userViewModel.Languages;
            data.Company = userViewModel.Company;
            data.Path = userViewModel.Path;
            data.Photo = userViewModel.Photo;
            data.IdGenders = userViewModel.IdGenders;
            data.IdMaritalStatus = userViewModel.IdMaritalStatus;
            data.Job = userViewModel.Job;
            data.Interests = userViewModel.Interests;
            data.IsAge = userViewModel.IsAge;
            data.IsFloor = userViewModel.IsFloor;
            data.IsStatus = userViewModel.IsStatus;
            data.IsJob = userViewModel.IsJob;
            data.IsApartmentNumber = userViewModel.IsApartmentNumber;
        }

        private void CopyToViewModel(UserViewModel userViewModel, UserDto userDto)
        {
            _userViewModel.UserId = userDto.UserId;
            _userViewModel.Firstname = userDto.Firstname;
            _userViewModel.Lastname = userDto.Lastname;
            _userViewModel.Birthday = userDto.Birthday;
            _userViewModel.Company = userDto.Company;
            _userViewModel.Email = userDto.Email;
            _userViewModel.IdGenders = userDto.IdGenders;
            _userViewModel.Gender = userDto.Gender;
            _userViewModel.IdMaritalStatus = userDto.IdMaritalStatus;
            _userViewModel.MaritalStatus = userDto.MaritalStatus;
            _userViewModel.Interests = userDto.Interests;
            _userViewModel.IsAge = userDto.IsAge;
            _userViewModel.IsApartmentNumber = userDto.IsApartmentNumber;
            _userViewModel.IsFloor = userDto.IsFloor;
            _userViewModel.IsJob = userDto.IsJob;
            _userViewModel.IsStatus = userDto.IsStatus;
            _userViewModel.Photo = userDto.Photo;
            _userViewModel.Path = userDto.Path;
            _userViewModel.Pseudo = userDto.Pseudo;
        }

    }
}







            //var gender = new GenderViewModel();
            //var result = await Navigation.ShowPopupAsync(new GenderPopup(gender));