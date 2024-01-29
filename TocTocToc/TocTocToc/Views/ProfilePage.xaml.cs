using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Models.View;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly ItemRequestChannelHandler _itemRequestGenderHandler = new(new GendersItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestMaritalStatusHandler = new(new MaritalStatusItemRequest());

        private readonly HttpRequestChannelHandler _httpRequestChannelHandler = new(new UserStorageServiceChannel());

        //private readonly UserStorageService _userStorageService;

        private List<ItemDtoModel> _gendersItem;
        private List<ItemDtoModel> _maritalStatusItem;
        private UserDtoModel _userDto;
        private UserModel _userModel;
        private bool _isLastname;
        private bool _isFirstname;
        //private string _userId;

        //private readonly IDisposable _disposed = null;

        public ProfilePage()
        {
            InitializeComponent();

            //_userStorageService = new UserStorageService();
            InitValues();
        }

        protected override async void OnAppearing()
        {
            //if (_disposed != null) return;

            await GetUserProfile();
            SetupPicker();
            InitForm();
            BindingContext = _userModel;
        }

        private async void InitValues()
        {
            //_userId = LocalStorageService.GetUserId();

            _userDto = new UserDtoModel();
            _userModel = new UserModel();

            _gendersItem = await _itemRequestGenderHandler.GetItemsAsync(null);
            _maritalStatusItem = await _itemRequestMaritalStatusHandler.GetItemsAsync(null);
        }

        private async Task GetUserProfile()
        {
            //_userDto = await _userStorageService.GetUserDetailsAsync(_userId);
            _userDto = await _httpRequestChannelHandler.GetHttpAsync<UserDtoModel>();
            CopyModel.UserCopyDtoToModel(_userDto, _userModel);
        }

        private void InitForm()
        {
            _isFirstname = false;
            _isLastname = false;
            XNameSaveButton.IsEnabled = false;

            _userModel.FullPathPhoto = WebConstants.Url + _userModel.Path + _userModel.Photo;

            if (_userModel.IdGenders != 0)
                XNameGenderPicker.SelectedItem = ((List<ItemDtoModel>)XNameGenderPicker.ItemsSource).FirstOrDefault(element => element.Id == _userModel.IdGenders);

            if( _userModel.IdMaritalStatus != 0)
                XNameMaritalStatusPicker.SelectedItem = ((List<ItemDtoModel>)XNameMaritalStatusPicker.ItemsSource).FirstOrDefault(element => element.Id == _userModel.IdMaritalStatus);

            XNameOnDatePicker.Date = string.IsNullOrEmpty(_userModel.Birthday) ? DateTime.Now : ConvertStringDateToDate(_userModel.Birthday);
        }

        private static DateTime ConvertStringDateToDate(string date)
        {
            var dateStrings = date.Split('-');
            var dateDetails = new int[dateStrings.Length];
            for (var i = 0; i < dateStrings.Length; i++)
            {
                dateDetails[i] = int.Parse(dateStrings[i]);
            }
            var returnDateTime = new DateTime(dateDetails[0], dateDetails[1], dateDetails[2]);

            return returnDateTime;
        }

        private void SetupPicker()
        {
            XNameGenderPicker.ItemsSource = _gendersItem;
            XNameMaritalStatusPicker.ItemsSource = _maritalStatusItem;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            var data = new UserDtoModel();
            _userModel.Birthday = GetBirthday();

            CopyModel.UserCopyModelToDto(_userModel, data);

            //var userDto = await _userStorageService.UpdateUserAsync(_userId, data);
            var userDto = await _httpRequestChannelHandler.UpdateHttpAsync<UserDtoModel, UserDtoModel>(data);
            CopyModel.UserCopyDtoToModel(userDto, _userModel);

            InitForm();
            BindingContext = _userModel;

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

            // var fileName = file.FileName;
            var path = file.FullPath;
            // var stream = await file.OpenReadAsync();
            // var photo = ImageSource.FromStream(() => stream);
            _userModel.Photo = path;
            _userModel.FullPathPhoto = path;
            //await _userStorageService.PostPhotoAsync(_userId, _userModel.Photo);
            await _httpRequestChannelHandler.SaveHttpMediaAsync<UserDtoModel>(_userModel.Photo);
            BindingContext = _userModel;
        }


        private void OnGender(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var genderDetails = (ItemDtoModel)picker.SelectedItem;
            _userModel.IdGenders = genderDetails.Id;

        }


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
            var picker = (Picker)sender;
            var maritalStatusDetails = (ItemDtoModel)picker.SelectedItem;
            _userModel.IdMaritalStatus = maritalStatusDetails.Id;
        }


        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_userModel.Lastname))
                _isLastname = true;
            if (!string.IsNullOrEmpty(_userModel.Firstname))
                _isFirstname = true;

            var isAddresses = LocalStorageService.IsAddresses();

            if (_isLastname && _isFirstname && isAddresses)
                XNameSaveButton.IsEnabled = true;

        }

        //protected override void OnDisappearing()
        //{
        //    _disposed.Dispose();
        //}


    }
}