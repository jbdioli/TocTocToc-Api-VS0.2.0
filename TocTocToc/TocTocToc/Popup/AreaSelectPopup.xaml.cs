using System.Collections.Generic;
using System.Linq;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms.Xaml;

//public partial class AreaSelectPopup : Xamarin.CommunityToolkit.UI.Views.Popup<AreaSelectedDtoModel>


namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreaSelectPopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {
        private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());

        private readonly ItemRequestChannelHandler _itemRequestStateHandler = new(new StatesItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestCountyHandler = new(new CountiesItemRequest());
        private readonly ItemRequestChannelHandler _itemRequestCityHandler = new(new CitiesItemRequest());

        private readonly AreaSelectPopupViewModel _context;

        public AreaSelectPopup(AreaSelectedModel areaSelectedModel)
        {
            InitializeComponent();

            var bindingContext = BindingContext;

            _context = bindingContext as AreaSelectPopupViewModel;

            if (_context == null) return;
            
            _context.CountryDetails = areaSelectedModel.CountryDetails;
            _context.StatesDetails = areaSelectedModel.StatesDetails;
            _context.CountiesDetails = areaSelectedModel.CountiesDetails;
            _context.CitiesDetails = areaSelectedModel.CitiesDetails;
            _context.IsAllCountry = areaSelectedModel.IsAllCountry;
            _context.IsAllState = areaSelectedModel.IsAllState;
            _context.IsAllCounty = areaSelectedModel.IsAllCounty;
            _context.IsAllCity = areaSelectedModel.IsAllCity;

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


        protected override object GetLightDismissResult()
        {
            var context = _context;

            if (  !context.IsAllCountry && !context.IsAllState && !context.IsAllCounty &&
                string.IsNullOrWhiteSpace(context.StateText) && string.IsNullOrWhiteSpace(context.CountyText))
            {
                _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                return null;
            }


            var areaSelected = new AreaSelectedDtoModel
            {
                CountrySelected =
                    {
                        Id = context.CountryDetails.Id,
                        Country = context.CountryDetails.Country
                    }
            };


            if (context.IsAllState)
            {
                var newStatesItem = new List<ItemDtoModel>();
                var selectedStates = new List<StateDtoModel>();

                foreach (var itemView in context.AutoCompleteStateEntry.EntryItems)
                {
                    var idState = context.AutoCompleteStateEntry.ItemProposals.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedStates.Add(new StateDtoModel() { IdCountries = context.CountryDetails.Id, Id = idState, State = itemView.Item });
                }

                //foreach (var itemDto in context.AutoCompleteStateEntry.DataBaseItems)
                foreach (var itemDto in context.StateDtoModels)
                {
                    newStatesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = context.CountryDetails.Id, Item = itemDto.Item });
                }

                SaveStatesItem(newStatesItem);
                areaSelected.StatesSelected.AddRange(selectedStates);

            }

            if (context.IsAllCounty)
            {
                var newCountiesItem = new List<ItemDtoModel>();
                var selectedCounties = new List<CountyDtoModel>();

                var state = context.AutoCompleteStateEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(context.StateText.ToLower()));
                if (state == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }

                foreach (var itemView in context.AutoCompleteCountyEntry.EntryItems)
                {
                    var idCounty = context.AutoCompleteCountyEntry.ItemProposals.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedCounties.Add(new CountyDtoModel() { IdStates = state.Id, Id = idCounty, County = itemView.Item });
                }

                //foreach (var itemDto in context.AutoCompleteCountyEntry.DataBaseItems)
                foreach (var itemDto in context.CountyDtoModels)
                {
                    newCountiesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = state.Id, Item = itemDto.Item });
                }

                SaveCountyItem(newCountiesItem);
                areaSelected.StatesSelected.Add(new StateDtoModel() { IdCountries = context.CountryDetails.Id, Id = state.Id, State = state.Item });
                areaSelected.CountiesSelected.AddRange(selectedCounties);
            }

            if (!context.IsAllCountry && !context.IsAllState && !context.IsAllCounty)
            {
                var newCitiesItem = new List<ItemDtoModel>();
                var selectedCities = new List<CityDtoModel>();

                var state = context.AutoCompleteStateEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(context.StateText.ToLower()));
                if (state == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }

                var county = context.AutoCompleteCountyEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(context.CountyText.ToLower()));
                if (county == null)
                {
                    _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
                    return null;
                }


                foreach (var itemView in context.AutoCompleteCityEntry.EntryItems)
                {
                    var idCities = context.AutoCompleteCityEntry.ItemProposals.Where(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).Select(itemEl => itemEl.Id).FirstOrDefault();
                    selectedCities.Add(new CityDtoModel() { IdCounties = county.Id, Id = idCities, City = itemView.Item });
                }

                //foreach (var itemDto in context.AutoCompleteCityEntry.DataBaseItems)
                foreach (var itemDto in context.CityDtoModels)
                {
                    newCitiesItem.Add(new ItemDtoModel() { Id = itemDto.Id, IdParents = county.Id, Item = itemDto.Item });
                }

                SaveCitiesItem(newCitiesItem);
                areaSelected.StatesSelected.Add(new StateDtoModel() { IdCountries = context.CountryDetails.Id, Id = state.Id, State = state.Item });
                areaSelected.CountiesSelected.Add(new CountyDtoModel() { IdStates = state.Id, Id = county.Id, County = county.Item });
                areaSelected.CitiesSelected.AddRange(selectedCities);
            }


            areaSelected.IsAllCountry = context.IsAllCountry;
            areaSelected.IsAllState = context.IsAllState;
            areaSelected.IsAllCounty = context.IsAllCounty;
            areaSelected.IsAllCity = !context.IsAllCity;


            var isAreaSelectedValid = context.IsAreaSelectedValid(areaSelected);
            if (!isAreaSelectedValid)
            {
                areaSelected = null;
                _notificationChannelHandler.SendNotification(ENotificationType.IsAreaSelectedIncomplete, null);
            }

            return areaSelected;
        }
    }
}