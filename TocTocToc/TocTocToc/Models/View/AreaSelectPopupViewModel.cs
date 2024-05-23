using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Shared;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TocTocToc.Models.View;

public partial class AreaSelectPopupViewModel : AreaSelectedModel
{
    public ObservableCollection<ItemModel> CountriesSuggestion { get; set; } = [];
    public ObservableCollection<ItemModel> StatesSuggestion { get; set; } = [];
    public ObservableCollection<ItemModel> CountiesSuggestion { get; set; } = [];
    public ObservableCollection<ItemModel> CitiesSuggestion { get; set; } = [];
    public ObservableCollection<ItemModel> TokensCollection { get; set; } = [];

    public readonly AutoCompleteEntryModel AutoCompleteStateEntry = new();
    public readonly AutoCompleteEntryModel AutoCompleteCountyEntry = new();
    public readonly AutoCompleteEntryModel AutoCompleteCityEntry = new();

    private readonly ItemRequestChannelHandler _itemRequestCountryHandler = new(new CountriesItemRequest());
    private readonly ItemRequestChannelHandler _itemRequestStateHandler = new(new StatesItemRequest());
    private readonly ItemRequestChannelHandler _itemRequestCountyHandler = new(new CountiesItemRequest());
    private readonly ItemRequestChannelHandler _itemRequestCityHandler = new(new CitiesItemRequest());

    private AutoCompleteEntryHandler _autoCompleteStateEntryHandler;
    private AutoCompleteEntryHandler _autoCompleteCountyEntryHandler;
    private AutoCompleteEntryHandler _autoCompleteCityEntryHandler;

    private readonly CountryDtoModel _countryDto = new();

    public ICommand StateFocusedCommand { get; set; }
    public ICommand CountyFocusedCommand { get; set; }


    public AsyncCommand<object> StateTextChangedAsyncCommand { get; }
    public AsyncCommand<object> CountyTextChangedAsyncCommand { get; }
    public AsyncCommand<object> CityTextChangedAsyncCommand { get; }

    public AsyncCommand StateUnfocusedAsyncCommand { get; }
    public AsyncCommand CountyUnfocusedAsyncCommand { get; }
    public AsyncCommand CityUnfocusedAsyncCommand { get; }

    public AsyncCommand<object> StateItemTappedAsyncCommand { get; }

    public AsyncCommand<object> CountyItemTappedAsyncCommand { get; }
    public AsyncCommand<object> CityItemTappedAsyncCommand { get; }

    public AsyncCommand StateCompletedAsyncCommand { get; }
    public AsyncCommand CountyCompletedAsyncCommand { get; }
    public AsyncCommand CityCompletedAsyncCommand { get; }

    public AsyncCommand<object> CountryCheckBoxAsyncCommand { get; }
    public AsyncCommand<object> StateCheckBoxAsyncCommand { get; }
    public AsyncCommand<object> CountyCheckBoxAsyncCommand { get; }


    [ObservableProperty]
    private string _countryText;
    [ObservableProperty]
    private string _stateText;
    [ObservableProperty]
    private string _countyText;
    [ObservableProperty]
    private string _cityText;

    [ObservableProperty]
    private int _stateCursorPosition;
    [ObservableProperty]
    private int _countyCursorPosition;
    [ObservableProperty]
    private int _cityCursorPosition;

    [ObservableProperty]
    private bool _isStateSuggestion;
    [ObservableProperty]
    private bool _isCountySuggestion;
    [ObservableProperty]
    private bool _isCitySuggestion;

    [ObservableProperty]
    private bool _isStateReturnButton;
    [ObservableProperty]
    private bool _isCountyReturnButton;
    [ObservableProperty]
    private bool _isCityReturnButton;
    
    [ObservableProperty]
    private bool _isCountyEntryEnable;
    [ObservableProperty]
    private bool _isCityEntryEnable;

    [ObservableProperty]
    private bool _isStateEntryFocused;
    [ObservableProperty]
    private bool _isCountyEntryFocused;
    [ObservableProperty]
    private bool _isCityEntryFocused;

    [ObservableProperty]
    private bool _isStateVisible;
    [ObservableProperty]
    private bool _isCountyVisible;
    [ObservableProperty]
    private bool _isCityVisible;

    [ObservableProperty]
    private bool _isCountryCheckBoxVisible = true;
    [ObservableProperty]
    private bool _isStateCheckBoxVisible;
    [ObservableProperty]
    private bool _isCountyCheckBoxVisible;

    [ObservableProperty]
    private ItemModel _stateSelected;
    [ObservableProperty]
    private ItemModel _countySelected;
    [ObservableProperty]
    private ItemModel _citySelected;

    private bool _isSingleStateModeHandler;
    private bool _isTokenStateModeHandler;
    private bool _isSingleCountyModeHandler;
    private bool _isTokenCountyModeHandler;
    private bool _isSingleCityModeHandler = false;
    private bool _isTokenCityModeHandler;

    private bool _isStateTapped;
    private bool _isCountyTapped;
    private bool _isCityTapped;
    private bool _isInitMode = false;

    [ObservableProperty]
    private List<ItemDtoModel> _stateDtoModels = [];
    
    [ObservableProperty]
    private List<ItemDtoModel> _countyDtoModels = [];
    
    [ObservableProperty]
    private List<ItemDtoModel> _cityDtoModels = [];


    public AreaSelectPopupViewModel()
    {
        CountryCheckBoxAsyncCommand = new AsyncCommand<object>(CountryCheckedTask);

        StateCheckBoxAsyncCommand = new AsyncCommand<object>(StateCheckedTask);
        StateCompletedAsyncCommand = new AsyncCommand(StateCompletedTask);
        StateItemTappedAsyncCommand = new AsyncCommand<object>(StateItemTappedTask);
        StateTextChangedAsyncCommand = new AsyncCommand<object>(StateTextChangedTask);
        StateUnfocusedAsyncCommand = new AsyncCommand(StateUnfocusedTask);
        StateFocusedCommand = new AsyncCommand<object>(StateFocusedTask);


        CountyCheckBoxAsyncCommand = new AsyncCommand<object>(CountyCheckedTask);
        CountyCompletedAsyncCommand = new AsyncCommand(CountyCompletedTask);
        CountyItemTappedAsyncCommand = new AsyncCommand<object>(CountyItemTappedTask);
        CountyTextChangedAsyncCommand = new AsyncCommand<object>(CountyTextChangedTask);
        CountyUnfocusedAsyncCommand = new AsyncCommand(CountyUnfocusedTask);
        CountyFocusedCommand = new AsyncCommand<object>(CountyFocusedTask);

        CityCompletedAsyncCommand = new AsyncCommand(CityCompletedTask);
        CityItemTappedAsyncCommand = new AsyncCommand<object>(CityItemTappedTask);
        CityTextChangedAsyncCommand = new AsyncCommand<object>(CityTextChangedTask);
        CityUnfocusedAsyncCommand = new AsyncCommand(CityUnfocusedTask);

        InitPage();
    }


    private async void InitPage()
    {
        _isInitMode = true;
        IsBusy = true;
        InitObject();
        await InitItemsAsync();
        InitViewPage();
        _isInitMode = false;
        IsBusy = false;
    }

    private void InitObject()
    {
        _autoCompleteStateEntryHandler =
            new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(AutoCompleteStateEntry));
        _autoCompleteCountyEntryHandler =
            new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(AutoCompleteCountyEntry));
        _autoCompleteCityEntryHandler =
            new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(AutoCompleteCityEntry));
    }



    private async Task InitItemsAsync()
    {
        await InitCountryAsync();
        await InitStateAsync();
        await InitCountyAsync();
        await InitCityAsync();
    }

    private async Task InitCountryAsync()
    {

        await _itemRequestCountryHandler.GetItemsAsync(null);
        CountriesSuggestion = _itemRequestCountryHandler.ConverterToObservableCollection();

        var country = new ItemModel
        {
            IsBusy = false,
            Id = 0,
            IdParents = 0,
            Item = ""
        };

        if (CountryDetails.Id >= 0 || !string.IsNullOrWhiteSpace(CountryDetails.Country))
        {
            country = !string.IsNullOrWhiteSpace(CountryDetails.Country)
                ? CountriesSuggestion.FirstOrDefault(el => el.Item.Equals(CountryDetails.Country))
                : CountriesSuggestion.FirstOrDefault(el => el.Id.Equals(CountryDetails.Id));

            if (country != null)
            {
                
                CountryText = country.Item;
                _countryDto.Id = country.Id;
                _countryDto.Country = country.Item;
            }
        }
        else if (!string.IsNullOrWhiteSpace(CountryDetails.Country))
        {
            country = CountriesSuggestion.FirstOrDefault(el => el.Item.ToLower().Equals(CountryDetails.Country.ToLower()));
            if (country != null)
            {
                CountryText = country.Item;
                _countryDto.Id = country.Id;
                _countryDto.Country = country.Item;
            }
        }

        if (country != null)
        {
            CountryDetails.Country = country.Item;
            CountryDetails.Id = country.Id;
        }
    }


    private async Task InitStateAsync()
    {
        await GetStatesTask();

        if (IsAllCountry) return;

        if (StatesDetails.Count == 0) return;

        foreach (var stateModel in StatesDetails)
        {
            AutoCompleteStateEntry.EntryItems ??= [];
            AutoCompleteStateEntry.EntryItems.Add(new ItemModel() { Id = stateModel.Id, IdParents = stateModel.IdCountries, Item = stateModel.State });
        }

        if (!IsAllState)
        {
            _autoCompleteStateEntryHandler =
                new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(AutoCompleteStateEntry));
            _isSingleStateModeHandler = true;
            _isTokenStateModeHandler = false;

            foreach (var stateModel in StatesDetails)
            {
                StateText = stateModel.State;
                AutoCompleteStateEntry.Text = stateModel.State;
                AutoCompleteStateEntry.ItemSuggestions ??= [];
                AutoCompleteStateEntry.ItemSuggestions.Add(new ItemModel() { Id = stateModel.Id, IdParents = stateModel.IdCountries, Item = stateModel.State });
            }
        }
        else
        {
            _autoCompleteStateEntryHandler =
                new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(AutoCompleteStateEntry));

            _isSingleStateModeHandler = false;
            _isTokenStateModeHandler = true;

            foreach (var stateModel in StatesDetails)
            {
                TokensCollection ??= [];
                TokensCollection.Add(new ItemModel() { Id = stateModel.Id, IdParents = stateModel.IdCountries, Item = stateModel.State });
            }
        }

    }


    private async Task InitCountyAsync()
    {
        if (IsAllState) return;

        if (StatesDetails.Count > 0) 
            await GetCountiesTask();

        if (CountiesDetails.Count == 0) return;

        foreach (var countyModel in CountiesDetails)
        {
            AutoCompleteCountyEntry.EntryItems ??= [];
            AutoCompleteCountyEntry.EntryItems.Add(new ItemModel() { Id = countyModel.Id, IdParents = countyModel.IdStates, Item = countyModel.County });
        }

        if (!IsAllCounty)
        {

            _autoCompleteCountyEntryHandler =
                new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(AutoCompleteCountyEntry));
            _isSingleCountyModeHandler = true;
            _isTokenCountyModeHandler = false;

            foreach (var countyModel in CountiesDetails)
            {
                CountyText = countyModel.County;
                AutoCompleteCountyEntry.Text = countyModel.County;
                AutoCompleteCountyEntry.ItemSuggestions ??= [];
                AutoCompleteCountyEntry.ItemSuggestions.Add(new ItemModel() { Id = countyModel.Id, IdParents = countyModel.IdStates, Item = countyModel.County });
            }

        } else
        {
            _autoCompleteCountyEntryHandler =
                new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(AutoCompleteCountyEntry));
            _isSingleCountyModeHandler = false;
            _isTokenCountyModeHandler = true;

            if (CountiesDetails.Count == 0) return;

            foreach (var countyModel in CountiesDetails)
            {
                TokensCollection ??= [];
                TokensCollection.Add(new ItemModel() { Id = countyModel.Id, IdParents = countyModel.IdStates, Item = countyModel.County });
            }
        }

    }

    private async Task InitCityAsync()
    {

        if (CountiesDetails.Count > 0)
            await GetCitiesTask();

        _autoCompleteCityEntryHandler =
            new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(AutoCompleteCityEntry));
        _isTokenCityModeHandler = false;
        _isTokenCityModeHandler = true;

        if (CitiesDetails.Count == 0) return;

        foreach (var cityDto in CitiesDetails)
        {
            AutoCompleteCityEntry.EntryItems ??= [];
            AutoCompleteCityEntry.EntryItems.Add(new ItemModel() { Id = cityDto.Id, IdParents = cityDto.IdCounties, Item = cityDto.City });
            TokensCollection ??= [];
            TokensCollection.Add(new ItemModel() { Id = cityDto.Id, IdParents = cityDto.IdCounties, Item = cityDto.City });
        }


    }

    private void InitViewPage()
    {
        InitCheckBox();
        EntryHandler();
    }



    /*
     *  Country
     */
    private Task CountryCheckedTask(object arg)
    {
        if (arg is not CheckedChangedEventArgs e) return Task.CompletedTask;

        TokensCollection.Clear();

        AutoCompleteStateEntry.EntryItems.Clear();
        //AutoCompleteStateEntry.DataBaseItems.Clear();
        StateDtoModels.Clear();
        AutoCompleteStateEntry.Text = string.Empty;

        AutoCompleteCountyEntry.EntryItems.Clear();
        //AutoCompleteCountyEntry.DataBaseItems.Clear();
        CountyDtoModels.Clear();
        AutoCompleteCountyEntry.Text = string.Empty;
        CountiesSuggestion.Clear();

        AutoCompleteCityEntry.EntryItems.Clear();
        //AutoCompleteCityEntry.DataBaseItems.Clear();
        CityDtoModels.Clear();
        AutoCompleteCityEntry.Text = string.Empty;
        CitiesSuggestion.Clear();

        if (e.Value)
        {
            IsStateVisible = false;
            IsStateCheckBoxVisible = false;
            IsCountyVisible = false;
            IsCountyCheckBoxVisible = false;
            IsCityVisible = false;

            StateText = string.Empty;
            CountyText = string.Empty;
            CityText = string.Empty;

        }
        else
        {
            IsStateVisible = true;
            IsStateCheckBoxVisible = true;
            IsCountyVisible = true;
            IsCountyCheckBoxVisible = true;
            IsCityVisible = true;
        }

        return Task.CompletedTask;
    }



    /*
     * State
     */
    private async Task StateTextChangedTask(object arg)
    {

        if (_isInitMode == true) return;

        if (arg is not TextChangedEventArgs e) return;


        if (_isStateTapped)
        {
            return;
        }

        if (e.OldTextValue != null && e.NewTextValue.Length < e.OldTextValue.Length)
        {
            CountyText = string.Empty;
            CityText = string.Empty;

            AutoCompleteCountyEntry.EntryItems.Clear();
            AutoCompleteCountyEntry.Text = string.Empty;

            AutoCompleteCityEntry.EntryItems.Clear();
            AutoCompleteCityEntry.Text = string.Empty;
            TokensCollection.Clear();

            IsCountyEntryEnable = false;
            IsCityEntryEnable = false;
        }

        IsStateReturnButton = !string.IsNullOrEmpty(e.NewTextValue);
        await _autoCompleteStateEntryHandler.TextChanged(e, 0); // Attention CursorPosition a faire

        IsStateSuggestion = AutoCompleteStateEntry.IsSuggestionView;
        
        StatesSuggestion.Clear();
        foreach (var state in AutoCompleteStateEntry.ItemSuggestions)
        {
            StatesSuggestion.Add(state);
        }

    }



    private async Task StateCompletedTask()
    {
        await _autoCompleteStateEntryHandler.TextCompleted();
        IsStateReturnButton = !string.IsNullOrEmpty(AutoCompleteStateEntry.Text);

        EnabledEntryHandler();
        if (_isTokenStateModeHandler)
            TokenHandler();
    }


    private async Task StateItemTappedTask(object arg)
    {
        if (arg is not ItemModel item) return;

        _isStateTapped = true;
        

        await _autoCompleteStateEntryHandler.ItemTapped(item);

        EntryHandler();

        if (_isTokenStateModeHandler)
        {
            TokenHandler();
        }
        else
        {
            StateText = AutoCompleteStateEntry.Text;
            IsStateSuggestion = AutoCompleteStateEntry.IsSuggestionView;
        }

    }

    private async Task StateUnfocusedTask()
    {
        if (_isInitMode == true) return;

        if (_isStateTapped)
        {
            _isStateTapped = false;
            await GetCountiesTask();
            
            EntryHandler();
            return;
        }

        if (_autoCompleteStateEntryHandler == null ) return; // Why ????

        if (AutoCompleteStateEntry != null && AutoCompleteStateEntry.EntryItems.Count == 0 ) return;

        await _autoCompleteStateEntryHandler.Unfocused();
        await GetCountiesTask();

        EntryHandler();
        if (_isTokenStateModeHandler)
            TokenHandler();

    }


    private Task StateFocusedTask(object args)
    {
        return Task.CompletedTask;
    }


    private Task StateCheckedTask(object arg)
    {
        if (arg is not CheckedChangedEventArgs e) return Task.CompletedTask;


        if (e.Value)
        {
            _autoCompleteStateEntryHandler =
                new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(AutoCompleteStateEntry));
            _isTokenStateModeHandler = true;
            _isSingleStateModeHandler = false;

            IsCountryCheckBoxVisible= false;
            IsCountyVisible = false;
            IsCountyCheckBoxVisible = false;
            IsCityVisible = false;

            StateText = string.Empty;
            CountyText = string.Empty;
            CityText = string.Empty;
        }
        else
        {
            _autoCompleteStateEntryHandler =
                new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(AutoCompleteStateEntry));
            _isTokenStateModeHandler = false;
            _isSingleStateModeHandler = true;

            IsCountyCheckBoxVisible = true;
            IsCountyVisible = true;
            IsCountyCheckBoxVisible = true;
            IsCityVisible = true;
        }

        TokensCollection.Clear();

        AutoCompleteStateEntry.EntryItems.Clear();
        //AutoCompleteStateEntry.DataBaseItems.Clear();
        StateDtoModels.Clear();
        AutoCompleteStateEntry.Text = string.Empty;

        AutoCompleteCountyEntry.EntryItems.Clear();
        //AutoCompleteCountyEntry.DataBaseItems.Clear();
        CountyDtoModels.Clear();
        AutoCompleteCountyEntry.Text = string.Empty;
        CountiesSuggestion.Clear();

        AutoCompleteCityEntry.EntryItems.Clear();
        //AutoCompleteCityEntry.DataBaseItems.Clear();
        CityDtoModels.Clear();
        AutoCompleteCityEntry.Text = string.Empty;
        CitiesSuggestion.Clear();


        return Task.CompletedTask;
    }


    /*
     * County
     */
    private async Task CountyTextChangedTask(object arg)
    {
        if (_isInitMode == true) return;

        if (arg is not TextChangedEventArgs e) return;

        if (_isCountyTapped) return;

        if (e.OldTextValue != null && e.NewTextValue.Length < e.OldTextValue.Length)
        {
            CityText = string.Empty;

            AutoCompleteCityEntry.EntryItems.Clear();
            AutoCompleteCityEntry.Text = string.Empty;
            TokensCollection.Clear();

            IsCityEntryEnable = false;
        }


        IsCountyReturnButton = !string.IsNullOrEmpty(e.NewTextValue);
        await _autoCompleteCountyEntryHandler.TextChanged(e, 0); // cursor position a faire

        IsCountySuggestion = AutoCompleteCountyEntry.IsSuggestionView;
        CountiesSuggestion.Clear();
        foreach (var county in AutoCompleteCountyEntry.ItemSuggestions)
        {
            CountiesSuggestion.Add(county);
        }
    }


    private async Task CountyCompletedTask()
    {
        await _autoCompleteCountyEntryHandler.TextCompleted();
        
        EntryHandler();
        
        if (_isTokenCountyModeHandler)
            TokenHandler();
    }


    private async Task CountyItemTappedTask(object arg)
    {
        if (arg is not ItemModel item) return;

        _isCountyTapped = true;

        await _autoCompleteCountyEntryHandler.ItemTapped(item);
        
        EntryHandler();

        if (_isTokenCountyModeHandler)
        {
            TokenHandler();
        }
        else
        {
            CountyText = AutoCompleteCountyEntry.Text;
            IsCountySuggestion = AutoCompleteCountyEntry.IsSuggestionView;
        }
    }

    private async Task CountyUnfocusedTask()
    {
        if (_isInitMode == true) return;

        if (_isCountyTapped)
        {
            _isCountyTapped = false;
            await GetCitiesTask();
            EntryHandler();
            return;
        }

        if (AutoCompleteCountyEntry != null && AutoCompleteCountyEntry.EntryItems.Count == 0) return;

        await _autoCompleteCountyEntryHandler.Unfocused();
        await GetCitiesTask();

        EntryHandler();

        if (_isTokenCountyModeHandler)
            TokenHandler();

    }

    private Task CountyFocusedTask(object args)
    {
        return Task.CompletedTask;
    }


    private Task CountyCheckedTask(object arg)
    {
        if (arg is not CheckedChangedEventArgs e) return Task.CompletedTask;

        if (e.Value)
        {
            _autoCompleteCountyEntryHandler = new AutoCompleteEntryHandler(
                new TokenAutoCompleteEntry(AutoCompleteCountyEntry));
            _isTokenCountyModeHandler = true;
            _isSingleCountyModeHandler = false;

            IsCountryCheckBoxVisible = false;
            IsStateCheckBoxVisible = false;
            IsCityVisible = false;

            CountyText = string.Empty;
            CityText = string.Empty;
        }
        else
        {
            _autoCompleteCountyEntryHandler = new AutoCompleteEntryHandler(
                new SingleAutoCompleteEntry(AutoCompleteCountyEntry));
            _isTokenStateModeHandler = false;
            _isSingleStateModeHandler = true;

            IsCountryCheckBoxVisible = true;
            IsStateCheckBoxVisible = true;
            IsCityVisible = true;
        }


        TokensCollection.Clear();

        AutoCompleteCountyEntry.EntryItems.Clear();
        //AutoCompleteCountyEntry.DataBaseItems.Clear();
        CountyDtoModels.Clear();
        AutoCompleteCountyEntry.Text = string.Empty;
        CountiesSuggestion.Clear();

        AutoCompleteCityEntry.EntryItems.Clear();
        //AutoCompleteCityEntry.DataBaseItems.Clear();
        CityDtoModels.Clear();
        AutoCompleteCityEntry.Text = string.Empty;
        CitiesSuggestion.Clear();

        return Task.CompletedTask;
    }


    /*
     * City
     */
    private async Task CityTextChangedTask(object arg)
    {
        if (_isInitMode == true) return;

        if (arg is not TextChangedEventArgs e) return;

        if (_isCityTapped) return;

        IsCityReturnButton = !string.IsNullOrEmpty(e.NewTextValue);
        await _autoCompleteCityEntryHandler.TextChanged(e, 0); // Attention cursor 

        IsCitySuggestion = AutoCompleteCityEntry.IsSuggestionView;
        CitiesSuggestion.Clear();
        foreach (var city in AutoCompleteCityEntry.ItemSuggestions)
        {
            CitiesSuggestion.Add(city);
        }

    }


    private async Task CityCompletedTask()
    {
        await _autoCompleteCityEntryHandler.TextCompleted();

        IsCitySuggestion = AutoCompleteCityEntry.IsSuggestionView;
    }


    private async Task CityItemTappedTask(object arg)
    {
        if (arg is not ItemModel item) return;

        _isCityTapped = true;

        await _autoCompleteCityEntryHandler.ItemTapped(item);

        EntryHandler();
        TokenHandler();
        CityText = string.Empty;
        IsCitySuggestion = AutoCompleteCityEntry.IsSuggestionView;
    }


    private async Task CityUnfocusedTask()
    {
        if (_isInitMode == true) return;

        await _autoCompleteCityEntryHandler.Unfocused();
    }


    /*
     * shared code
     */


    /*
     * CheckBox
     */

    private void InitCheckBox()
    {
        if (!IsAllCountry)
        {
            IsStateVisible = true;
            IsCountyVisible = true;
            IsCityVisible = true;

            IsStateCheckBoxVisible = true;
            IsCountyCheckBoxVisible = true;
        }
        else
        {
            IsStateVisible = false;
            IsCountyVisible = false;
            IsCityVisible = false;

            IsStateCheckBoxVisible = false;
            IsCountyCheckBoxVisible = false;
        }

    }

    /*
     * Entry
     */

    private void EntryHandler()
    {
        EnabledEntryHandler();
        FocusedEntryHandler();
    }


    /*
     * Entry focused
     */



    private void FocusedEntryHandler()
    {
        switch (IsCountyEntryEnable)
        {
            case false when !IsCityEntryEnable:
                IsStateEntryFocused = true;
                IsCountyEntryFocused = false;
                IsCityEntryFocused = false;
                break;
            case true when !IsCityEntryEnable:
                IsStateEntryFocused = false;
                IsCountyEntryFocused = true;
                IsCityEntryFocused = false;
                break;
            case true when IsCityEntryEnable:
                IsStateEntryFocused = false;
                IsCountyEntryFocused = false;
                IsCityEntryFocused = true;
                break;
        }
    }


    /*
     * Entry enabled
     */


    private void EnabledEntryHandler()
    {
        if (string.IsNullOrWhiteSpace(StateText))
        {
            IsCountyEntryEnable= false;
            IsCityEntryEnable = false;
        }
        else if (!string.IsNullOrWhiteSpace(StateText))
        {
            IsCountyEntryEnable = true;
        }

        if (string.IsNullOrWhiteSpace(CountyText))
        {
            IsCityEntryEnable = false;
        }
        else if (!string.IsNullOrWhiteSpace(CountyText))
        {
            IsCityEntryEnable = true;
        }

    }


    /*
     * Token
     */


    private void TokenHandler()
    {
        var toDisplay = new ObservableCollection<ItemModel>();
        if (_isTokenStateModeHandler)
        {
            toDisplay = AutoCompleteStateEntry.EntryItems;
        }
        if (_isTokenCountyModeHandler) toDisplay = AutoCompleteCountyEntry.EntryItems;
        if (!_isTokenStateModeHandler  && !_isTokenCountyModeHandler) toDisplay = AutoCompleteCityEntry.EntryItems;

        TokensCollection.Clear();

        IsStateSuggestion = false;
        IsCountySuggestion = false;
        IsCitySuggestion = false;

        if (toDisplay.Count <= 0) return;
        
        foreach (var item in toDisplay)
        {
            TokensCollection.Add(item);
        }

    }



    /*
     * Delete token
     */

    public Command<string> TappedDeleteTokenCommand => new(item => {
        var selectedItem = item;

        TokensCollection.Remove(TokensCollection.Single(el => el.Item.ToLower().Equals(selectedItem.ToLower())));
        //if (XNameStateCheckBox.IsChecked)
        if (IsAllState)
        {
            //AutoCompleteStateEntry.DataBaseItems.Remove(AutoCompleteStateEntry.DataBaseItems.Single(el => el.Item.Equals(selectedItem)));
            StateDtoModels.Remove(StateDtoModels.Single(el => el.Item.Equals(selectedItem)));
            return;
        }

        if (IsAllCounty)
        {
            //AutoCompleteCountyEntry.DataBaseItems.Remove(AutoCompleteCountyEntry.DataBaseItems.Single(el => el.Item.Equals(selectedItem)));
            CountyDtoModels.Remove( CountyDtoModels.Single(el => el.Item.Equals(selectedItem)));
            return;
        }

        //AutoCompleteCityEntry.DataBaseItems.Remove(AutoCompleteCityEntry.DataBaseItems.Single(el => el.Item.Equals(selectedItem)));
        CityDtoModels.Remove(CityDtoModels.Single(el => el.Item.Equals(selectedItem)));

    });



    


    public bool IsAreaSelectedValid(AreaSelectedDtoModel areaSelected)
    {
        if (areaSelected == null) return false;

        if (areaSelected.IsAllCountry)
            return true;
        if (areaSelected.CitiesSelected.Count > 0 && !areaSelected.IsAllCounty)
            return true;
        if (areaSelected.CountiesSelected.Count > 0 && areaSelected.IsAllCounty)
            return true;
        if (areaSelected.StatesSelected.Count > 0 && areaSelected.IsAllState)
            return true;

        return false;
    }

    /*
     * State
     */


    private async Task GetStatesTask()
    {
        await _itemRequestStateHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = _countryDto.Id });
        var states = _itemRequestStateHandler.ConverterToObservableCollection();
        AutoCompleteStateEntry.ItemProposals = states;
    }


    /*
     * County
     */

    private async Task GetCountiesTask()
    {

        var stateDetails = AutoCompleteStateEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(AutoCompleteStateEntry.Text.ToLower()));

        if (stateDetails != null)
        {
            await _itemRequestCountyHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = stateDetails.Id, RequestedItem = string.Empty });
            var counties = _itemRequestCountyHandler.ConverterToObservableCollection();
            AutoCompleteCountyEntry.ItemProposals = counties;
        }

    }


    /*
     * City
     */


    private async Task GetCitiesTask()
    {
        var countyDetails = AutoCompleteCountyEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(AutoCompleteCountyEntry.Text.ToLower()));

        if (countyDetails != null)
        {
            await _itemRequestCityHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = countyDetails.Id, RequestedItem = string.Empty });
            var cities = _itemRequestCityHandler.ConverterToObservableCollection();
            AutoCompleteCityEntry.ItemProposals = cities;
        }
    }

}