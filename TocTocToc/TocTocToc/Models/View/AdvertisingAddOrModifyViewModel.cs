using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Popup;
using TocTocToc.Services;
using TocTocToc.Shared;
using TocTocToc.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TocTocToc.Models.View;

public partial class AdvertisingAddOrModifyViewModel : AdvertisingModel
{

    private readonly ItemRequestChannelHandler _itemRequestGenderHandler = new(new GendersItemRequest());
    private readonly HttpRequestChannelHandler _httpRequestAdvertisingChannelHandler = new(new AdvertisingStorageServiceChannel());
    private readonly ItemRequestChannelHandler _itemRequestInterestHandler = new(new InterestsItemRequest());

    private readonly AutoCompleteEntryHandler _autoCompleteInterestEntryHandler;
    private readonly AutoCompleteEntryModel _autoCompleteInterestEntry = new();

    private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());

    private readonly GeolocationHandler _geolocationHandler;


    private ICommand Init { get; }
    private ICommand ClearRxNet { get; }
    public AsyncCommand AddMediaAsyncCommand { get; }
    public AsyncCommand UpdateMediaAsyncCommand { get; }
    public AsyncCommand AddDescriptionAsyncCommand { get; }
    public AsyncCommand AddAreaSelectAsyncCommand { get; }
    public AsyncCommand GenderAsyncCommand { get; }
    public AsyncCommand AddAgeAsyncCommand { get; }
    public AsyncCommand<object> InterestTextChangedAsyncCommand { get; }
    public AsyncCommand InterestUnfocusedAsyncCommand { get; }
    public AsyncCommand<object> InterestItemTappedAsyncCommand { get; }
    public AsyncCommand InterestCompletedAsyncCommand { get; }
    public AsyncCommand BudgetAsyncCommand { get; }
    public AsyncCommand SaveAdvertisementAsyncCommand { get; }
    public AsyncCommand HistoryAsyncCommand { get; }
    public AsyncCommand PayAsyncCommand { get; }
    public AsyncCommand<AdvertisingModel> ShowMediaDetailsAsyncCommand { get; }
    public AsyncCommand ValidationEntriesAsyncCommand { get; }


    private IDisposable _disposed = null;

    private readonly LocationDtoModel _locationDto = new();
    private readonly EPayDetailsDtoModel _ePayDetails = new();

    public ObservableCollection<ItemModel> InterestSuggestions { get; set; } = [];

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
    private bool _isSaved = false;
    [ObservableProperty]
    private bool _isPayingButton = false;
    [ObservableProperty]
    private List<ItemDtoModel> _gendersItem = new();
    [ObservableProperty]
    private bool _isInterestSuggestions = false;
    [ObservableProperty]
    private bool _isEnabledComponent = true;
    [ObservableProperty]
    private ItemModel _selectedInterest;
    [ObservableProperty]
    private bool _isInterestBusy = false;
    private bool _isInterestTapedItemBusy = false;


    public AdvertisingAddOrModifyViewModel()
    {
        Init = new AsyncCommand(InitAsync);
        Init.Execute(null);
        ClearRxNet = new AsyncCommand(ClearRxNetAsync);
        AddMediaAsyncCommand = new AsyncCommand(AddMediaAsync);
        UpdateMediaAsyncCommand = new AsyncCommand(UpdateMediaAsync);
        AddDescriptionAsyncCommand = new AsyncCommand(AddDescriptionAsync);
        AddAreaSelectAsyncCommand = new AsyncCommand(AddAreaSelectAsync);
        GenderAsyncCommand = new AsyncCommand(GenderAsync);
        AddAgeAsyncCommand = new AsyncCommand(AddAgeAsync);
        InterestTextChangedAsyncCommand = new AsyncCommand<object>(InterestTextChangedTask);
        InterestUnfocusedAsyncCommand = new AsyncCommand(InterestUnfocusedTask);
        InterestItemTappedAsyncCommand = new AsyncCommand<object>(InterestItemTappedTask);
        InterestCompletedAsyncCommand = new AsyncCommand(InterestCompletedTask);
        BudgetAsyncCommand = new AsyncCommand(BudgetAsync);
        SaveAdvertisementAsyncCommand = new AsyncCommand(SaveAdvertisementAsync);
        HistoryAsyncCommand = new AsyncCommand(HistoryAsync);
        PayAsyncCommand = new AsyncCommand(PayAsync);
        ShowMediaDetailsAsyncCommand = new AsyncCommand<AdvertisingModel>(ShowMediaDetailsAsync);
        ValidationEntriesAsyncCommand = new AsyncCommand(ValidationEntryAsync);

        _geolocationHandler = new GeolocationHandler(_locationDto);
        _autoCompleteInterestEntryHandler = new AutoCompleteEntryHandler(new MultipleAutoCompleteEntry(_autoCompleteInterestEntry));
    }


    private async Task InitAsync()
    {
        await InitValues();
        await InitAutoComplete();

        if (_disposed == null)
        {
            SubscribeToData();
        }
        //EntriesHandler(_advertisingModel.IsImage);
        //SetupPicker();
    }

    private Task ClearRxNetAsync()
    {
        _disposed?.Dispose();
        _disposed = null;
        return Task.CompletedTask;
    }


    private async Task AddMediaAsync()
    {
        //var advertisingModel = new AdvertisingModel();

        var file = await FilePicker.PickAsync(new PickOptions()
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Pick an Image"
        });

        if (file == null) return;

        var path = file.FullPath;
        //var fileName = file.FileName;
        //var stream = await file.OpenReadAsync();
        //var image = ImageSource.FromStream(() => stream);
        Image = path;
        FullPathImage = path;

        await _httpRequestAdvertisingChannelHandler.SaveHttpMediaAsync<AdvertisingDtoModel>(Image);

        IsImage = Image != null;
    }



    private async Task UpdateMediaAsync()
    {
        //var advertisingModel = new AdvertisingModel();

        var file = await FilePicker.PickAsync(new PickOptions()
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Pick an Image"
        });

        if (file == null) return;

        var path = file.FullPath;

        Image = path;
        FullPathImage = path;

        var response = await _httpRequestAdvertisingChannelHandler.UpdateHttpMediaAsync<AdvertisingDtoModel>(Image, AdvertisingId);
        if (response != null && string.IsNullOrWhiteSpace(response.AdvertisingId))
        {
            _notificationChannelHandler.SendNotification(ENotificationType.IsUpdatedMediaFalse, null);
        }

        if (response == null) return;

        Image = response.Image;
        Path = response.Path;
        FullPathImage = WebConstants.Url + Path + Image;
    }


    private async Task AddDescriptionAsync()
    {
        var valueDetails = new ValueDetailsModel
        {
            Text = Info
        };

        var descriptionPopup = new TextAreaPopup(valueDetails);

        await Application.Current.MainPage.Navigation.ShowPopupAsync(descriptionPopup);
        Info = valueDetails.Text;
    }


    private async Task AddAreaSelectAsync()
    {
        var areaSelectedModel = new AreaSelectedModel();

        if (IsEditMode)
        {
            areaSelectedModel = Area;
        }
        if (string.IsNullOrWhiteSpace(areaSelectedModel.CountryDetails.Country))
        {
            await _geolocationHandler.GetLocationDetailsAsync();
            areaSelectedModel.CountryDetails.Country = _locationDto.Country;
        }

        if (await Application.Current.MainPage.Navigation.ShowPopupAsync(new AreaSelectPopup(areaSelectedModel)) is not AreaSelectedDtoModel returnValue) return;

        IsArea = true;
        CopyModel.AreaCopyDtoToModel(returnValue, Area);

        await ValidationEntryAsync();
        ValidationButtons();
    }


    private async Task GenderAsync()
    {
        await ValidationEntryAsync();
        ValidationButtons();
    }

    private async Task AddAgeAsync()
    {
        var age = new AgeModel()
        {
            IsAllAge = IsAllAge,
            AgeMini = AgeMini,
            AgeMaxi = AgeMaxi
        };

        var ageUpdated = await Application.Current.MainPage.Navigation.ShowPopupAsync(new AgePopup(age));

        if (ageUpdated == null)
        {
            IsAgeValid = false;
            return;
        }


        IsAllAge = ageUpdated.IsAllAge;
        AgeMini = ageUpdated.AgeMini;
        AgeMaxi = ageUpdated.AgeMaxi;

        IsAgeValid = ageUpdated.IsAgeValid;

        await ValidationEntryAsync();
        ValidationButtons();
    }


    /*
     * Interests
     */

    private async Task InterestTextChangedTask(object arg)
    {
        //if (_isInterestTapedItemBusy)
        //{
        //    _isInterestTapedItemBusy = false;
        //    return;
        //}

        if (arg is not TextChangedEventArgs e) return;

        IsInterestBusy = true;

        await _autoCompleteInterestEntryHandler.TextChanged(e);
        IsInterestSuggestions = _autoCompleteInterestEntry.IsSuggestionView;
        IsEnabledComponent = !_autoCompleteInterestEntry.IsSuggestionView;
        InterestSuggestions.Clear();
        if (IsInterestSuggestions)
        {
            foreach (var interest in _autoCompleteInterestEntry.ItemSuggestions)
            {
                InterestSuggestions.Add(interest);
            }
        }
        
        IsInterestBusy = false;
    }


    private async Task InterestItemTappedTask(object arg)
    {
        if (arg is not ItemModel item) return;
        _isInterestTapedItemBusy = true;

        await _autoCompleteInterestEntryHandler.ItemTapped(item);

        SelectedInterest = null!;

        IsInterestSuggestions = _autoCompleteInterestEntry.IsSuggestionView;
        IsEnabledComponent = !_autoCompleteInterestEntry.IsSuggestionView;
        Interests = _autoCompleteInterestEntry.Text;
    }


    private async Task InterestCompletedTask()
    {
        await _autoCompleteInterestEntryHandler.TextCompleted();
        IsInterestSuggestions = _autoCompleteInterestEntry.IsSuggestionView;
    }


    private async Task InterestUnfocusedTask()
    {
        await _autoCompleteInterestEntryHandler.Unfocused();
        IsInterestSuggestions = _autoCompleteInterestEntry.IsSuggestionView;

        SetInterestDetails();

        await SaveNewInterestItems();

        ValidationButtons();
    }


    private async Task SaveNewInterestItems()
    {
        var itemsDto = _autoCompleteInterestEntry.EntryItems.Select(itemModel => new ItemDtoModel() { Id = itemModel.Id, IdParents = itemModel.IdParents, Item = itemModel.Item }).ToList();

        await _itemRequestInterestHandler.SaveItemsAsync(itemsDto);
    }


    /*
     * Budget
     */


    private async Task BudgetAsync()
    {
        var budget = new BudgetDtoModel();

        if (IsEditMode || !string.IsNullOrEmpty(Budget))
        {
            budget.Budget = double.Parse(Budget);
            budget.Duration = int.Parse(Duration);
            budget.StartDate = StartDate;
        }

        budget = await Application.Current.MainPage.Navigation.ShowPopupAsync(new BudgetPopup(budget)) as BudgetDtoModel;

        if (budget == null)
        {
            IsBudget = false;
            return;
        }

        Budget = budget.Budget.ToString(CultureInfo.InvariantCulture);
        Duration = budget.Duration.ToString(CultureInfo.InvariantCulture);
        StartDate = budget.StartDate;

        IsBudget = true;
        await ValidationEntryAsync();
        ValidationButtons();
    }


    /*
     * Save
     */


    private async Task SaveAdvertisementAsync()
    {
        var advertisingDto = new AdvertisingDtoModel();
        GetPickerGender();
        CopyModel.AdvertisingCopyModelToDto(this, advertisingDto);
        await _httpRequestAdvertisingChannelHandler.UpdateHttpAsync<AdvertisingDtoModel, AdvertisingDtoModel>(advertisingDto);
        ValidationButtons();
    }


    private static async Task HistoryAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AdvertisingHistoryPage());
    }


    private async Task PayAsync()
    {
        var ePayPage = new EPay(_ePayDetails);
        await Application.Current.MainPage.Navigation.PushAsync(ePayPage);
    }

    private static async Task ShowMediaDetailsAsync(AdvertisingModel advertisingModel)
    {
        if (advertisingModel == null) return;

        await Application.Current.MainPage.Navigation.ShowPopupAsync(new AdvertisingDisplayPopup(advertisingModel));
        
    }


    private async Task InitValues()
    { 
        GendersItem = await _itemRequestGenderHandler.GetItemsAsync(null);
    }


    private async Task InitAutoComplete()
    {
        await _itemRequestInterestHandler.GetItemsAsync(null);
        var interests = _itemRequestInterestHandler.ConverterToObservableCollection();
        _autoCompleteInterestEntry.ItemProposals = interests;
    }


    private void SubscribeToData()
    {
        _disposed = RxNetHandler.AdvertisingSubject.Subscribe(
            advertising =>
            {
                if (advertising == null) return;

                CopyModel.AdvertisingCopyDtoToModel(advertising, this);
                //SetupPicker();
                FullPathImage = WebConstants.Url + Path + Image;


                if (string.IsNullOrWhiteSpace(Image))
                {
                    IsImage = true;
                }
                if (IsEditMode)
                {
                    SelectedItemGender = GendersItem.FirstOrDefault(element => element.Id == IdGenders) ?? new ItemDtoModel();
                }

                ValidationEntryAsync();
                ValidationButtons();

            },
            () =>
            {
                Console.WriteLine("[ completed ]");
            }
        );
    }

    private void GetPickerGender()
    {
        IdGenders = SelectedItemGender.Id;
        Gender = SelectedItemGender.Item;
    }


    private void ValidationButtons()
    {
        if (IsName && IsAgeValid && IsGender && IsBudget && IsArea && IsInterest && IsImage)
        {
            IsPayingButton  = true;
        }
        else
        {
            IsPayingButton = false;
        }
    }


    private Task ValidationEntryAsync()
    {
        if (!string.IsNullOrWhiteSpace(Name))
            IsName = true;

        if (IdGenders > 0)
            IsGender = true;

        if (IsAllAge || !AgeMaxi.Equals("0"))
            IsAgeValid = true;

        if (!Budget.Equals("0"))
            IsBudget = true;

        if (Area.StatesDetails.Count > 0 || Area.CountiesDetails.Count > 0 || Area.CitiesDetails.Count > 0)
            IsArea = true;

        if (!string.IsNullOrWhiteSpace(Interests))
            IsInterest = true;

        if (!string.IsNullOrWhiteSpace(Image))
            IsImage = true;
        
        return Task.CompletedTask;
    }


    private void SetInterestDetails()
    {
        InterestsDetails ??= [];
        foreach (var itemView in _autoCompleteInterestEntry.EntryItems)
        {
            var interestView = new InterestModel
            {
                Id = itemView.Id,
                Interest = itemView.Item
            };

            InterestsDetails.Add(interestView);
        }

        if (!string.IsNullOrWhiteSpace(Interests))
            IsInterest = true;
    }


}