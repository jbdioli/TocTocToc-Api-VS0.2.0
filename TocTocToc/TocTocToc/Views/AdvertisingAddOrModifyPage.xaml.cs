using System;
using System.Collections.Generic;
using System.Globalization;
using TocTocToc.Popup;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyChanged;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using System.Xml.Linq;
using System.Linq;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingAddOrModifyPage : ContentPage
    {
        //private readonly GeolocationHandler _geolocationHandler;
        //private readonly LocationDtoModel _locationDto = new();

        //private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());
        //private readonly HttpRequestChannelHandler _httpRequestAdvertisingChannelHandler = new(new AdvertisingStorageServiceChannel());

        //private readonly ItemRequestChannelHandler _itemRequestGenderHandler = new(new GendersItemRequest());
        //private readonly ItemRequestChannelHandler _itemRequestInterestHandler = new(new InterestsItemRequest());

        //private readonly AutoCompleteEntryHandler _autoCompleteInterestEntryHandler;
        //private readonly AutoCompleteEntryDtoModel _autoCompleteInterestEntryDto = new();

        //private AdvertisingViewModel _advertisingView = new();
        //private readonly AdvertisingDtoModel _advertisingDto = new();
        //private readonly EPayDetailsDtoModel _ePayDetails = new();

        //private ObservableCollection<ItemViewModel> _interestsItem = new();

        //private List<ItemDtoModel> _gendersItem;

        //private IDisposable _disposed;

        //public AdvertisingAddOrModifyPage(AdvertisingViewModel advertisingView)
        public AdvertisingAddOrModifyPage()
        {
            InitializeComponent();
            //_geolocationHandler = new GeolocationHandler(_locationDto);

            //InitViewPage();

            //_autoCompleteInterestEntryHandler = new AutoCompleteEntryHandler(new MultipleAutoCompleteEntry(_autoCompleteInterestEntryDto));

            //InitValues();
            //InitAutoComplete();

        }

        //private void InitViewPage()
        //{
        //    XNameSave.IsEnabled = false;
        //    XNameValidationPay.IsEnabled = false;
        //}

        //private async void InitValues()
        //{
        //    _gendersItem = await _itemRequestGenderHandler.GetItemsAsync(null);

        //    await _itemRequestInterestHandler.GetItemsAsync(null);
        //    _interestsItem = _itemRequestInterestHandler.ConverterToObservableCollection();
        //}

        //private void InitAutoComplete()
        //{
        //    _autoCompleteInterestEntryDto.Suggestions = _interestsItem;
        //    _autoCompleteInterestEntryDto.XNameSuggestionView = XNameInterestSuggestionsView;
        //    _autoCompleteInterestEntryDto.XNameEntries = new List<Entry> { XNameInterestsSearch };

        //}

        ////private void GetAdvertisingData(AdvertisingViewModel advertisingView)
        ////{
        ////    if (advertisingView != null && !string.IsNullOrEmpty(advertisingView.AdvertisingId))
        ////    {
        ////        _advertisingView = advertisingView;
        ////        _advertisingView.IsEditMode = true;
        ////        if (!string.IsNullOrEmpty(advertisingView.Image))
        ////            _advertisingView.IsImage = true;
        ////    }
        ////    else
        ////    {
        ////        _advertisingView = new AdvertisingViewModel();
        ////    }

        ////}


        protected override void OnAppearing()
        {
            //InitValues();
            //InitAutoComplete();

            //if (_disposed == null)
            //{
            //    SubscribeToData();
            //}
            //EntriesHandler(_advertisingView.IsImage);
            //SetupPicker();
            //BindingContext = _advertisingView;
        }


        //    private void SetupPicker()
        //    {
        //        XNameGenderPicker.ItemsSource = _gendersItem;
        //    }


        //    private void SubscribeToData()
        //    {
        //        _disposed = RxNetHandler.AdvertisingSubject.Subscribe(
        //            advertising =>
        //            {
        //                if (advertising == null) return;

        //                _advertisingView = new AdvertisingViewModel();
        //                CopyModel.AdvertisingCopyDtoToViewModel(advertising, _advertisingView);
        //                SetupPicker();
        //                _advertisingView.FullPathImage = WebConstants.Url + _advertisingView.Path + _advertisingView.Image;
        //                if (_advertisingView.IsEditMode)
        //                {
        //                    XNameSaveMediaButton.IsVisible = false;
        //                    XNameUpdateMediaButton.IsVisible = true;
        //                    XNameGenderPicker.SelectedItem = ((List<ItemDtoModel>)XNameGenderPicker.ItemsSource).FirstOrDefault(element => element.Id == _advertisingView.IdGenders);
        //                }

        //                InitValidationEntry();
        //                if (_advertisingView.IsImage)
        //                    EntriesHandler(_advertisingView.IsImage);
        //                IsFullInformationSavedValidation();
        //                IsPaymentButton();

        //                BindingContext = _advertisingView;
        //            },
        //            () =>
        //            {
        //                Console.WriteLine("[ completed ]");
        //            }
        //        );
        //    }


        //    private async void OnAddMedia(object sender, EventArgs e)
        //    {

        //        var file = await FilePicker.PickAsync(new PickOptions()
        //        {
        //            FileTypes = FilePickerFileType.Images,
        //            PickerTitle = "Pick an Image"
        //        });

        //        if (file == null) return;

        //        var path = file.FullPath;
        //        //var fileName = file.FileName;
        //        //var stream = await file.OpenReadAsync();
        //        //var image = ImageSource.FromStream(() => stream);
        //        _advertisingView.Image = path;
        //        _advertisingView.FullPathImage = path;

        //        await _httpRequestAdvertisingChannelHandler.SaveHttpMediaAsync<AdvertisingDtoModel>(_advertisingView.Image);

        //        BindingContext = _advertisingView;

        //        _advertisingView.IsImage = _advertisingView.Image != null;
        //    }


        //    private async void OnUpdateMedia(object sender, EventArgs e)
        //    {
        //        var file = await FilePicker.PickAsync(new PickOptions()
        //        {
        //            FileTypes = FilePickerFileType.Images,
        //            PickerTitle = "Pick an Image"
        //        });

        //        if (file == null) return;

        //        var path = file.FullPath;
        //        _advertisingView.Image = path;
        //        _advertisingView.FullPathImage = path;

        //        var response = await _httpRequestAdvertisingChannelHandler.UpdateHttpMediaAsync<AdvertisingDtoModel>(_advertisingView.Image, _advertisingView.AdvertisingId);
        //        if (response != null && string.IsNullOrWhiteSpace(response.AdvertisingId))
        //        {
        //            _notificationChannelHandler.SendNotification(ENotificationType.IsUpdatedMediaFalse, null);
        //        }

        //        if (response == null) return;

        //        _advertisingView.Image = response.Image;
        //        _advertisingView.Path = response.Path;
        //        _advertisingView.FullPathImage = WebConstants.Url + _advertisingView.Path + _advertisingView.Image;

        //        BindingContext = _advertisingView;
        //    }



        //    private void OnNameUnfocused(object sender, FocusEventArgs e)
        //    {
        //        _advertisingView.IsName = !string.IsNullOrWhiteSpace(_advertisingView.Name);
        //    }



        //    private async void OnAddDescription(object sender, EventArgs e)
        //    {
        //        var valueDetails = new ValueDetailsViewModel
        //        {
        //            Text = _advertisingView.Info
        //        };

        //        var descriptionPopup = new TextAreaPopup(valueDetails);

        //        await Navigation.ShowPopupAsync(descriptionPopup);
        //        _advertisingView.Info = valueDetails.Text;
        //    }


        //    private async void OnAddAreaSelect(object sender, EventArgs e)
        //    {
        //        var areaSelectedDto = new AreaSelectedDtoModel();

        //        if (_advertisingView.IsEditMode)
        //        {
        //            CopyModel.AreaCopyViewModelToDto(_advertisingView.Area, areaSelectedDto);
        //        }
        //        else
        //        {
        //            await _geolocationHandler.GetLocationDetailsAsync();
        //            areaSelectedDto.CountrySelected.Country = _locationDto.Country;
        //        }

        //        var returnValue = await Navigation.ShowPopupAsync(new AreaSelectPopup(areaSelectedDto));

        //        if (returnValue == null) return;

        //        _advertisingView.IsArea = true;
        //        CopyModel.AreaCopyDtoToViewModel(returnValue, _advertisingView.Area);
        //    }



        //    private void OnGender(object sender, EventArgs e)
        //    {
        //        var picker = (Picker)sender;
        //        var genderDetails = (ItemDtoModel)picker.SelectedItem;
        //        _advertisingView.IdGenders = genderDetails.Id;
        //        _advertisingView.Gender = genderDetails.Item;
        //        _advertisingView.IsGender = XNameGenderPicker.SelectedItem != null;
        //    }


        //    private async void OnAddAge(object sender, EventArgs e)
        //    {
        //        var age = new AgeViewModel()
        //        {
        //            IsAllAge = _advertisingView.IsAllAge,
        //            AgeMini = _advertisingView.AgeMini,
        //            AgeMaxi = _advertisingView.AgeMaxi
        //        };

        //        var ageUpdated = await Navigation.ShowPopupAsync(new AgePopup(age));

        //        if (ageUpdated == null)
        //        {
        //            _advertisingView.IsAgeValid = false;
        //            return;
        //        }


        //        _advertisingView.IsAllAge = ageUpdated.IsAllAge;
        //        _advertisingView.AgeMini = ageUpdated.AgeMini;
        //        _advertisingView.AgeMaxi = ageUpdated.AgeMaxi;

        //        _advertisingView.IsAgeValid = ageUpdated.IsAgeValid;
        //    }


        //    private void OnInterestItemTapped(object sender, ItemTappedEventArgs e)
        //    {
        //        _autoCompleteInterestEntryHandler.ItemTapped(sender, e);
        //    }

        //    [SuppressPropertyChangedWarnings]
        //    private void OnInterestTextChanged(object sender, TextChangedEventArgs e)
        //    {
        //        _autoCompleteInterestEntryHandler.TextChanged(e);
        //    }

        //    private async void OnInterestUnfocused(object sender, FocusEventArgs e)
        //    {
        //        await _autoCompleteInterestEntryHandler.Unfocused();

        //        _advertisingView.Interests = _autoCompleteInterestEntryDto.Text;
        //        _advertisingView.InterestsDetails ??= new List<InterestViewModel>();
        //        foreach (var itemView in _autoCompleteInterestEntryDto.ElementsToDisplay)
        //        {
        //            var interestView = new InterestViewModel
        //            {
        //                Id = itemView.Id,
        //                Interest = itemView.Item
        //            };

        //            _advertisingView.InterestsDetails.Add(interestView);
        //        }

        //        if (!string.IsNullOrWhiteSpace(_advertisingView.Interests))
        //            _advertisingView.IsInterest = true;

        //        await _itemRequestInterestHandler.SaveItemsAsync(_autoCompleteInterestEntryDto.NewElementsToAdd);
        //    }


        //    private async void OnBudget(object sender, EventArgs e)
        //    {

        //        var budget = new BudgetDtoModel();

        //        if (_advertisingView.IsEditMode || !string.IsNullOrEmpty(_advertisingView.Budget))
        //        {
        //            budget.Budget = double.Parse(_advertisingView.Budget);
        //            budget.Duration = int.Parse(_advertisingView.Duration);
        //            budget.StartDate = _advertisingView.StartDate;
        //        }

        //        budget = await Navigation.ShowPopupAsync(new BudgetPopup(budget));

        //        if (budget == null)
        //        {
        //            _advertisingView.IsBudget = false;
        //            return;
        //        }

        //        _advertisingView.Budget = budget.Budget.ToString(CultureInfo.InvariantCulture);
        //        _advertisingView.Duration = budget.Duration.ToString(CultureInfo.InvariantCulture);
        //        _advertisingView.StartDate = budget.StartDate;

        //        _advertisingView.IsBudget = true;
        //    }



        //    private async void OnHistory(object sender, EventArgs e)
        //    {
        //        //await Navigation.PushAsync(new AdvertisingHistoryPage());
        //        await Navigation.PushAsync(new NavigationPage());
        //    }


        //    private void OnShowMediaDetails(object sender, EventArgs e)
        //    {
        //        Navigation.ShowPopup(new AdvertisingDisplayPopup(_advertisingView));
        //    }


        //    private async void OnSave(object sender, EventArgs e)
        //    {
        //        await SaveAdvertisement();
        //        IsPaymentButton();
        //    }


        //    private async void OnPay(object sender, EventArgs e)
        //    {
        //        var ePayPage = new EPay(_ePayDetails);
        //        await Navigation.PushAsync(ePayPage);
        //    }


        //    private void InitValidationEntry()
        //    {
        //        if (!string.IsNullOrWhiteSpace(_advertisingView.Name))
        //            _advertisingView.IsName = true;

        //        if (_advertisingView.IdGenders > 0)
        //            _advertisingView.IsGender = true;

        //        if (_advertisingView.IsAllAge || !_advertisingView.AgeMaxi.Equals("0"))
        //            _advertisingView.IsAgeValid = true;

        //        if (!_advertisingView.Budget.Equals("0"))
        //            _advertisingView.IsBudget = true;

        //        if (_advertisingView.Area.StatesSelected.Count > 0 || _advertisingView.Area.CountiesSelected.Count > 0 || _advertisingView.Area.CitiesSelected.Count > 0)
        //            _advertisingView.IsArea = true;

        //        if (!string.IsNullOrWhiteSpace(_advertisingView.Interests))
        //            _advertisingView.IsInterest = true;

        //        if (!string.IsNullOrWhiteSpace(_advertisingView.Image))
        //            _advertisingView.IsImage = true;

        //    }


        //    private void IsFullInformationSavedValidation()
        //    {
        //        var isName = _advertisingView.IsName;
        //        var isGender = _advertisingView.IsGender;
        //        var isAge = _advertisingView.IsAgeValid;
        //        var isBudget = _advertisingView.IsBudget;
        //        var isArea = _advertisingView.IsArea;
        //        var isInterest = _advertisingView.IsInterest;
        //        var isImage = _advertisingView.IsImage;

        //        XNameSave.IsEnabled = isImage;

        //        if (isName && isAge && isGender && isBudget && isArea && isInterest && isImage)
        //        {
        //            _advertisingView.IsSaved = true;
        //        }
        //        else
        //        {
        //            _advertisingView.IsSaved = false;
        //        }
        //    }


        //    private void IsSavedButton()
        //    {
        //        var isImage = _advertisingView.IsImage;

        //        XNameSave.IsEnabled = isImage;
        //    }


        //    private void IsPaymentButton()
        //    {
        //        var isSaved = _advertisingView.IsSaved;
        //        var isPayed = _advertisingView.IsPayed;

        //        if (isSaved && !isPayed)
        //        {
        //            XNameValidationPay.IsEnabled = true;
        //        }
        //        else
        //        {
        //            XNameValidationPay.IsEnabled = false;
        //        }
        //    }

        //    private void EntriesHandler(bool isImage)
        //    {
        //        XNameTitle.IsEnabled = isImage;
        //        XNameDescription.IsEnabled = isImage;
        //        XNameArea.IsEnabled = isImage;
        //        XNameGenderPicker.IsEnabled = isImage;
        //        XNameAge.IsEnabled = isImage;
        //        XNameInterestsSearch.IsEnabled = isImage;
        //        XNameBudget.IsEnabled = isImage;
        //    }

        //    private async Task SaveAdvertisement()
        //    {
        //        CopyModel.AdvertisingCopyViewModelToDto(_advertisingView, _advertisingDto);
        //        await _httpRequestAdvertisingChannelHandler.UpdateHttpAsync<AdvertisingDtoModel, AdvertisingDtoModel>(_advertisingDto);
        //    }


        //    protected override void OnDisappearing()
        //    {
        //        _disposed?.Dispose();
        //        _disposed = null;
        //    }

        private void OnAddMedia(object sender, EventArgs e)
        {
                
        }

        private void OnUpdateMedia(object sender, EventArgs e)
        {
            
        }

        private void OnNameUnfocused(object sender, FocusEventArgs e)
        {
            
        }

        private void OnAddDescription(object sender, EventArgs e)
        {
            
        }

        private void OnAddAreaSelect(object sender, EventArgs e)
        {
            
        }

        private void OnGender(object sender, EventArgs e)
        {
            
        }

        private void OnAddAge(object sender, EventArgs e)
        {
            
        }

        private void OnInterestTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void OnInterestUnfocused(object sender, FocusEventArgs e)
        {
            
        }

        private void OnBudget(object sender, EventArgs e)
        {
            
        }

        private void OnSave(object sender, EventArgs e)
        {
            
        }

        private async void OnHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdvertisingHistoryPage());
        }

        private void OnShowMediaDetails(object sender, EventArgs e)
        {
            
        }

        private void OnInterestItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        private void OnPay(object sender, EventArgs e)
        {
            
        }
    }
}