using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreaSelectPopup : Xamarin.CommunityToolkit.UI.Views.Popup<AreaSelectedDtoModel>
    {
        private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());

        private ObservableCollection<ItemViewModel> CountriesItem { get; set; } = new();
        private ObservableCollection<ItemViewModel> StatesItem { get; set; } = new();
        private ObservableCollection<ItemViewModel> CountiesItem { get; set; } = new();
        private ObservableCollection<ItemViewModel> CitiesItem { get; set; } = new();

        private ObservableCollection<ItemViewModel> TokensItem { get; set; } = new();

        private readonly AutoCompleteEntryDtoModel _autoCompleteStateEntryDto = new();
        private readonly AutoCompleteEntryDtoModel _autoCompleteCountyEntryDto = new();
        private readonly AutoCompleteEntryDtoModel _autoCompleteCityEntryDto = new();

        private readonly ItemRequestChannelHandler _itemRequestCountryHandler = new(new CountriesItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestStateHandler = new(new StatesItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestCountyHandler = new(new CountiesItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestCityHandler = new(new CitiesItemRequest());

        private AutoCompleteEntryHandler _autoCompleteStateEntryHandler;
        private AutoCompleteEntryHandler _autoCompleteCountyEntryHandler;
        private AutoCompleteEntryHandler _autoCompleteCityEntryHandler;

        private readonly AreaSelectedDtoModel _areaSelectedDto;

        private readonly CountryDtoModel _countryDto = new();

        public AreaSelectPopup(AreaSelectedDtoModel areaSelectedDto)
        {
            InitializeComponent();

            _areaSelectedDto = areaSelectedDto;

            InitPage();
        }


        private async void InitPage()
        {
            InitAutoComplete();
            await InitItemsAsync();
            InitViewPage();
            InitObject();
        }

        private void InitObject()
        {
            //_autoCompleteStateEntryHandler =
            //    new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(_autoCompleteStateEntryDto));
            //_autoCompleteCountyEntryHandler =
            //    new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(_autoCompleteCountyEntryDto));
            //_autoCompleteCityEntryHandler =
            //    new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(_autoCompleteCityEntryDto));
        }


        private void InitAutoComplete()
        {
            _autoCompleteStateEntryDto.ElementsToDisplay = TokensItem;
            _autoCompleteStateEntryDto.XNameSuggestionView = XNameStateSuggestionsView;
            _autoCompleteStateEntryDto.XNameEntries = new List<Entry> { XNameStateSearch, XNameCountySearch, XNameCitySearch };

            _autoCompleteCountyEntryDto.ElementsToDisplay = TokensItem;
            _autoCompleteCountyEntryDto.XNameSuggestionView = XNameCountySuggestionsView;
            _autoCompleteCountyEntryDto.XNameEntries = new List<Entry> { XNameStateSearch, XNameCountySearch, XNameCitySearch };

            _autoCompleteCityEntryDto.ElementsToDisplay = TokensItem;
            _autoCompleteCityEntryDto.XNameSuggestionView = XNameCitySuggestionsView;
            _autoCompleteCityEntryDto.XNameEntries = new List<Entry> { XNameStateSearch, XNameCountySearch, XNameCitySearch };
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
            CountriesItem = _itemRequestCountryHandler.ConverterToObservableCollection();


            if (_areaSelectedDto.CountrySelected.Id >= 0 || !string.IsNullOrWhiteSpace(_areaSelectedDto.CountrySelected.Country))
            {
                var country = !string.IsNullOrWhiteSpace(_areaSelectedDto.CountrySelected.Country)
                    ? CountriesItem.FirstOrDefault(el => el.Item.Equals(_areaSelectedDto.CountrySelected.Country))
                    : CountriesItem.FirstOrDefault(el => el.Id.Equals(_areaSelectedDto.CountrySelected.Id));

                if (country != null)
                {
                    XNameCountryLabel.Text = country.Item;
                    _countryDto.Id = country.Id;
                    _countryDto.Country = country.Item;
                }
            }
            else if (!string.IsNullOrWhiteSpace(_areaSelectedDto.CountrySelected.Country))
            {
                var country = CountriesItem.FirstOrDefault(el => el.Item.ToLower().Equals(_areaSelectedDto.CountrySelected.Country.ToLower()));
                if (country != null)
                {
                    XNameCountryLabel.Text = country.Item;
                    _countryDto.Id = country.Id;
                    _countryDto.Country = country.Item;
                }
            }
        }


        private async Task InitStateAsync()
        {
            await _itemRequestStateHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = _countryDto.Id });
            StatesItem = _itemRequestStateHandler.ConverterToObservableCollection();
            _autoCompleteStateEntryDto.Suggestions = StatesItem;

            if (_areaSelectedDto.IsAllCountry) return;
            if (!_areaSelectedDto.IsAllState && _areaSelectedDto.StatesSelected == null) return;
            if (_areaSelectedDto.StatesSelected.Count == 0) return;
            if (!_areaSelectedDto.IsAllState)
            {
                XNameStateSearch.CursorPosition = 1;
                XNameStateSearch.Focus();
                foreach (var stateDto in _areaSelectedDto.StatesSelected)
                {
                    _autoCompleteStateEntryDto.Text = stateDto.State;
                }
            }
            else
            {
                foreach (var stateDto in _areaSelectedDto.StatesSelected)
                {
                    _autoCompleteStateEntryDto.ElementsToDisplay ??= new ObservableCollection<ItemViewModel>();
                    _autoCompleteStateEntryDto.ElementsToDisplay.Add(new ItemViewModel(){ Id = stateDto.Id, IdParents = stateDto.IdCountries, Item = stateDto.State });
                }
            }

            _autoCompleteStateEntryHandler =
                new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(_autoCompleteStateEntryDto));
        }

        private async Task InitCountyAsync()
        {
            if (_areaSelectedDto.CountiesSelected is { Count: > 0 })
            {
                await GetCountyAsync();

                if (!_areaSelectedDto.IsAllCounty)
                {
                    XNameCountySearch.CursorPosition = 1;
                    XNameCountySearch.Focus();
                    foreach (var countyDto in _areaSelectedDto.CountiesSelected)
                    {
                        _autoCompleteCountyEntryDto.Text = countyDto.County;
                    }

                }
                else
                {
                    foreach (var countyDto in _areaSelectedDto.CountiesSelected)
                    {
                        _autoCompleteCountyEntryDto.ElementsToDisplay ??= new ObservableCollection<ItemViewModel>();
                        _autoCompleteCountyEntryDto.ElementsToDisplay.Add(new ItemViewModel(){ Id = countyDto.Id, IdParents = countyDto.IdStates, Item = countyDto.County });
                    }
                }
            }


            _autoCompleteCountyEntryHandler =
                new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(_autoCompleteCountyEntryDto));

        }

        private async Task InitCityAsync()
        {
            if (_areaSelectedDto.IsAllCity && _areaSelectedDto.CitiesSelected is { Count: > 0 })
            {
                await GetCityAsync();

                XNameCitySearch.CursorPosition = 1;
                XNameCitySearch.Focus();
                foreach (var cityDto in _areaSelectedDto.CitiesSelected)
                {
                    _autoCompleteCityEntryDto.ElementsToDisplay ??= new ObservableCollection<ItemViewModel>();
                    _autoCompleteCityEntryDto.ElementsToDisplay.Add(new ItemViewModel() { Id = cityDto.Id, IdParents = cityDto.IdCounties, Item = cityDto.City });
                }

            }

            _autoCompleteCityEntryHandler =
                new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(_autoCompleteCityEntryDto));

        }

        private void InitViewPage()
        {
            XNameTokensView.ItemsSource = TokensItem;
            EntryControlHandler();
        }



        /*
         *  Country
         */
        private void OnCheckedCountry(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                TokensItem.Clear();
                _autoCompleteStateEntryDto.NewElementsToAdd.Clear();
                _autoCompleteStateEntryDto.Text = string.Empty;
                
                _autoCompleteCountyEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCountyEntryDto.Text = string.Empty;
                CountiesItem.Clear();
                _autoCompleteCityEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCityEntryDto.Text = string.Empty;
                CitiesItem.Clear();

                XNameState.IsVisible = false;
                XNameStateCheckBoxIcon.IsVisible = false;
                XNameCounty.IsVisible = false;
                XNameCountyCheckBoxIcon.IsVisible = false;
                XNameCity.IsVisible = false;
            }
            else
            {
                XNameState.IsVisible = true;
                XNameStateCheckBoxIcon.IsVisible = true;
                XNameCounty.IsVisible = true;
                XNameCountyCheckBoxIcon.IsVisible = true;
                XNameCity.IsVisible = true;
            }

        }



        /*
         * State
         */
        private void OnStateTextChanged(object sender, TextChangedEventArgs e)
        {
            XNameStateReturnButton.IsVisible = !string.IsNullOrEmpty(e.NewTextValue);
            _autoCompleteStateEntryHandler?.TextChanged(e);
        }

        private async void OnStateCompleted(object sender, EventArgs e)
        {
            await _autoCompleteStateEntryHandler.TextCompleted(sender);
            XNameStateReturnButton.IsVisible = !string.IsNullOrEmpty(_autoCompleteStateEntryDto.Text);
            
            EntryControlHandler();
        }


        private void OnStateItemTapped(object sender, ItemTappedEventArgs e)
        {
            _autoCompleteStateEntryHandler?.ItemTapped(sender, e);
            EntryControlHandler();
        }

        private async void OnStateUnfocused(object sender, FocusEventArgs e)
        {
            EntryControlHandler();
            await _autoCompleteStateEntryHandler.Unfocused();
            await GetCountyAsync();
        }


        private void OnStateChecked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                _autoCompleteStateEntryHandler =
                    new AutoCompleteEntryHandler(new TokenAutoCompleteEntry(_autoCompleteStateEntryDto));
                XNameStateSearch.Text = string.Empty;

                _autoCompleteStateEntryDto.NewElementsToAdd.Clear();
                _autoCompleteStateEntryDto.Text = string.Empty;
                
                _autoCompleteCountyEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCountyEntryDto.Text = string.Empty;
                CountiesItem.Clear();
                _autoCompleteCityEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCityEntryDto.Text = string.Empty;
                CitiesItem.Clear();

                XNameCountryCheckBoxIcon.IsVisible = false;
                XNameCounty.IsVisible = false;
                XNameCountyCheckBoxIcon.IsVisible = false;
                XNameCity.IsVisible = false;
            }
            else
            {
                _autoCompleteStateEntryHandler =
                    new AutoCompleteEntryHandler(new SingleAutoCompleteEntry(_autoCompleteStateEntryDto));
                TokensItem.Clear();
                _autoCompleteStateEntryDto.NewElementsToAdd.Clear();

                XNameCountryCheckBoxIcon.IsVisible = true;
                XNameCounty.IsVisible = true;
                XNameCountyCheckBoxIcon.IsVisible = true;
                XNameCity.IsVisible = true;
            }
        }


        /*
         * County
         */
        private void OnCountyTextChanged(object sender, TextChangedEventArgs e)
        {
            XNameCountyReturnButton.IsVisible = !string.IsNullOrEmpty(e.NewTextValue);
            _autoCompleteCountyEntryHandler?.TextChanged(e);
        }


        private async void OnCountyCompleted(object sender, EventArgs e)
        {
            await _autoCompleteCountyEntryHandler.TextCompleted(sender);
            EntryControlHandler();
        }


        private void OnCountyItemTapped(object sender, ItemTappedEventArgs e)
        {
            _autoCompleteCountyEntryHandler?.ItemTapped(sender, e);
            EntryControlHandler();
        }

        private async void OnCountyUnfocused(object sender, FocusEventArgs e)
        {
            EntryControlHandler();
            await _autoCompleteCountyEntryHandler.Unfocused();

            await GetCityAsync();

        }


        private void OnCountyChecked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                _autoCompleteCountyEntryHandler = new AutoCompleteEntryHandler(
                    new TokenAutoCompleteEntry(_autoCompleteCountyEntryDto));

                if (!string.IsNullOrWhiteSpace(XNameCountySearch.Text))
                    XNameCountySearch.Text = string.Empty;

                _autoCompleteCountyEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCountyEntryDto.Text = string.Empty;
                CountiesItem.Clear();
                _autoCompleteCityEntryDto.NewElementsToAdd.Clear();
                _autoCompleteCityEntryDto.Text = string.Empty;
                CitiesItem.Clear();

                XNameCountryCheckBoxIcon.IsVisible = false;
                XNameStateCheckBoxIcon.IsVisible = false;
                XNameCity.IsVisible = false;
            }
            else
            {
                _autoCompleteCountyEntryHandler = new AutoCompleteEntryHandler(
                    new SingleAutoCompleteEntry(_autoCompleteCountyEntryDto));
                TokensItem.Clear();
                _autoCompleteCountyEntryDto.NewElementsToAdd.Clear();

                XNameCountryCheckBoxIcon.IsVisible = true;
                XNameStateCheckBoxIcon.IsVisible = true;
                XNameCity.IsVisible = true;
            }
        }


        /*
         * City
         */
        private void OnCityTextChanged(object sender, TextChangedEventArgs e)
        {
            XNameCityReturnButton.IsVisible = !string.IsNullOrEmpty(e.NewTextValue);
            _autoCompleteCityEntryHandler?.TextChanged(e);
        }


        private async void OnCityCompleted(object sender, EventArgs e)
        {
            await _autoCompleteCityEntryHandler.TextCompleted(sender);
        }


        private void OnCityItemTapped(object sender, ItemTappedEventArgs e)
        {
            _autoCompleteCityEntryHandler?.ItemTapped(sender, e);
        }


        private async void OnCityUnfocused(object sender, FocusEventArgs e)
        {
            await _autoCompleteCityEntryHandler.Unfocused();
        }


        private void EntryControlHandler()
        {
            if (string.IsNullOrWhiteSpace(XNameStateSearch.Text))
            {
                XNameCounty.IsEnabled = false;
                XNameCity.IsEnabled = false;
            }
            else if (!string.IsNullOrWhiteSpace(XNameStateSearch.Text))
            {
                XNameCounty.IsEnabled = true;
            }

            if (string.IsNullOrWhiteSpace(XNameCountySearch.Text))
            {
                XNameCity.IsEnabled = false;
            }
            else if (!string.IsNullOrWhiteSpace(XNameCountySearch.Text))
            {
                XNameCity.IsEnabled = true;
            }

        }



        /*
         * Delete token
         */

        public Command<string> TappedDeleteTokenCommand => new(item => {
            var selectedItem = item;

            TokensItem.Remove(TokensItem.Single(el => el.Item.ToLower().Equals(selectedItem.ToLower())));
            if (XNameStateCheckBox.IsChecked)
            {
                _autoCompleteStateEntryDto.NewElementsToAdd.Remove(_autoCompleteStateEntryDto.NewElementsToAdd.Single(el => el.Item.Equals(selectedItem)));
                return;
            }

            if (XNameCountyCheckBox.IsChecked)
            {
                _autoCompleteCountyEntryDto.NewElementsToAdd.Remove(_autoCompleteCountyEntryDto.NewElementsToAdd.Single(el => el.Item.Equals(selectedItem)));
                return;
            }

            _autoCompleteCityEntryDto.NewElementsToAdd.Remove(
                _autoCompleteCityEntryDto.NewElementsToAdd.Single(el => el.Item.Equals(selectedItem)));

        });


        

        private static bool IsAreaSelectedValid(AreaSelectedDtoModel areaSelected)
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

        private async void SaveCitiesItem(List<ItemDtoModel> newCitiesItem)
        {
            await _itemRequestCityHandler.SaveItemsAsync(newCitiesItem);
        }

        private async void SaveCountyItem(List<ItemDtoModel> newCountiesItem)
        {
            await _itemRequestCountyHandler.SaveItemsAsync(newCountiesItem);
        }

        private async void SaveStatesItem(List<ItemDtoModel> newStatesItem)
        {
            await _itemRequestStateHandler.SaveItemsAsync(newStatesItem);
        }


        private async Task GetCountyAsync()
        {
            var stateDto = StatesItem.FirstOrDefault(el => el.Item.ToLower().Equals(_autoCompleteStateEntryDto.Text.ToLower()));
            if (stateDto != null)
            {
                await _itemRequestCountyHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = stateDto.Id, RequestedItem = string.Empty });
                CountiesItem = _itemRequestCountyHandler.ConverterToObservableCollection();
                _autoCompleteCountyEntryDto.Suggestions = CountiesItem;
            }

        }


        private async Task GetCityAsync()
        {
            var countyDto = CountiesItem.FirstOrDefault(el => el.Item.ToLower().Equals(_autoCompleteCountyEntryDto.Text.ToLower()));
            if (countyDto != null)
            {
                await _itemRequestCityHandler.GetItemsAsync(new ItemRequestDtoModel() { RequestedId = countyDto.Id, RequestedItem = string.Empty });
                CitiesItem = _itemRequestCityHandler.ConverterToObservableCollection();
                _autoCompleteCityEntryDto.Suggestions = CitiesItem;
            }
        }


        protected override AreaSelectedDtoModel GetLightDismissResult()
        {
            if (!XNameCountryCheckBox.IsChecked && !XNameStateCheckBox.IsChecked && !XNameCountyCheckBox.IsChecked &&
                string.IsNullOrWhiteSpace(XNameStateSearch.Text) && string.IsNullOrWhiteSpace(XNameCountySearch.Text))
            {
                _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                return null;
            }


            var areaSelected = new AreaSelectedDtoModel
            {
                CountrySelected =
                {
                    Id = _countryDto.Id,
                    Country = _countryDto.Country
                }
            };

            if (XNameStateCheckBox.IsChecked)
            {
                var newStatesItem = new List<ItemDtoModel>();
                var selectedStates = new List<StateDtoModel>();

                foreach (var itemView in _autoCompleteStateEntryDto.ElementsToDisplay)
                {
                    var idState = StatesItem.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedStates.Add(new StateDtoModel() { IdCountries = _countryDto.Id, Id = idState, State = itemView.Item });
                }

                foreach (var itemDto in _autoCompleteStateEntryDto.NewElementsToAdd)
                {
                    newStatesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = _countryDto.Id, Item = itemDto.Item });
                }

                SaveStatesItem(newStatesItem);
                areaSelected.StatesSelected.AddRange(selectedStates);

            }

            if (XNameCountyCheckBox.IsChecked)
            {
                var newCountiesItem = new List<ItemDtoModel>();
                var selectedCounties = new List<CountyDtoModel>();

                var state = StatesItem.FirstOrDefault(el => el.Item.ToLower().Equals(XNameStateSearch.Text.ToLower()));
                if (state == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }

                foreach (var itemView in _autoCompleteCountyEntryDto.ElementsToDisplay)
                {
                    var idCounty = CountiesItem.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedCounties.Add(new CountyDtoModel() { IdStates = state.Id, Id = idCounty, County = itemView.Item });
                }

                foreach (var itemDto in _autoCompleteCountyEntryDto.NewElementsToAdd)
                {
                    newCountiesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = state.Id, Item = itemDto.Item });
                }

                SaveCountyItem(newCountiesItem);
                areaSelected.StatesSelected.Add(new StateDtoModel() { IdCountries = _countryDto.Id, Id = state.Id, State = state.Item });
                areaSelected.CountiesSelected.AddRange(selectedCounties);
            }

            if (!XNameCountryCheckBox.IsChecked && !XNameStateCheckBox.IsChecked && !XNameCountyCheckBox.IsChecked)
            {
                var newCitiesItem = new List<ItemDtoModel>();
                var selectedCities = new List<CityDtoModel>();

                var state = StatesItem.FirstOrDefault(el => el.Item.ToLower().Equals(XNameStateSearch.Text.ToLower()));
                if (state == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }

                var county = CountiesItem.FirstOrDefault(el => el.Item.ToLower().Equals(XNameCountySearch.Text.ToLower()));
                if (county == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }


                foreach (var itemView in _autoCompleteCityEntryDto.ElementsToDisplay)
                {
                    var idCities = CitiesItem.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedCities.Add(new CityDtoModel() { IdCounties = county.Id, Id = idCities, City = itemView.Item });
                }

                foreach (var itemDto in _autoCompleteCityEntryDto.NewElementsToAdd)
                {
                    newCitiesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = county.Id, Item = itemDto.Item });
                }

                SaveCitiesItem(newCitiesItem);
                areaSelected.StatesSelected.Add(new StateDtoModel() { IdCountries = _countryDto.Id, Id = state.Id, State = state.Item });
                areaSelected.CountiesSelected.Add(new CountyDtoModel() { IdStates = state.Id, Id = county.Id, County = county.Item });
                areaSelected.CitiesSelected.AddRange(selectedCities);
            }


            areaSelected.IsAllCountry = XNameCountryCheckBox.IsChecked;
            areaSelected.IsAllState = XNameStateCheckBox.IsChecked;
            areaSelected.IsAllCounty = XNameCountyCheckBox.IsChecked;
            areaSelected.IsAllCity = !XNameCountyCheckBox.IsChecked;


            var isAreaSelectedValid = IsAreaSelectedValid(areaSelected);
            if (!isAreaSelectedValid)
            {
                areaSelected = null;
                _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
            }

            return areaSelected;
        }
    }
}