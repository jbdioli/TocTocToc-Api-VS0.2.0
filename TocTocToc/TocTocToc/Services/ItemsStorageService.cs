using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services
{
    public class ItemsStorageService
    {
        //private const bool IsFile = false;
        //private readonly HttpMethods _httpMethods = new(IsFile);

        private readonly string _url = WebConstants.Url;


        /*
         * GET
         */

        public async Task<List<GenderDtoModel>> GetGendersAsync()
        {
            var settingId = LocalStorageService.GetSettingId(); // to take item in function of the language
            var token = LocalStorageService.GetAccessToken();

            var url = _url + WebConstants.GetGenders + settingId;

            var genders = await HttpMethods.HttpGetAsync<List<GenderDtoModel>>(url, token);

            genders ??= [];

            return genders;
        }


        public async Task<List<MaritalStatusDtoModel>> GetMaritalStatusAsync()
        {
            var token = LocalStorageService.GetAccessToken();
            var settingId = LocalStorageService.GetSettingId(); // to take item in function of the language

            var url = _url + WebConstants.GetMaritalStatus + settingId;

            var maritalStatus = await HttpMethods.HttpGetAsync<List<MaritalStatusDtoModel>>(url, token);

            maritalStatus ??= [];

            return maritalStatus;
        }


        public async Task<List<HousingTypeDtoModel>> GetHousingTypesAsync()
        {
            var token = LocalStorageService.GetAccessToken();
            var settingId = LocalStorageService.GetSettingId(); // to take item in function of the language

            var url = _url + WebConstants.GetHousingTypes + settingId;

            var housingTypes = await HttpMethods.HttpGetAsync<List<HousingTypeDtoModel>>(url, token);

            housingTypes ??= [];

            return housingTypes;
        }



        public async Task<List<InterestDtoModel>> GetInterestsAsync()
        {
            var settingId = LocalStorageService.GetSettingId();
            var url = _url + WebConstants.GetInterests + settingId;
            var token = LocalStorageService.GetAccessToken();

            var interests = await HttpMethods.HttpGetAsync<List<InterestDtoModel>>(url, token);

            interests ??= [];

            return interests;
        }



        public async Task<List<CountryDtoModel>> GetCountriesAsync()
        {
            var url = _url + WebConstants.GetCountries;
            var token = LocalStorageService.GetAccessToken();

            var countries = await HttpMethods.HttpGetAsync<List<CountryDtoModel>>(url, token);

            countries ??= [];

            return countries;
        }


        public async Task<List<StateDtoModel>> GetStatesAsync(CountryDtoModel country)
        {
            var url = _url + WebConstants.GetStates + country.Id.ToString();
            var token = LocalStorageService.GetAccessToken();

            var states = await HttpMethods.HttpGetAsync<List<StateDtoModel>>(url, token);

            states ??= [];

            return states;
        }


        public async Task<List<CountyDtoModel>> GetCountiesAsync(StateDtoModel states)
        {
            var url = _url + WebConstants.GetCounties + states.Id.ToString();
            var token = LocalStorageService.GetAccessToken();

            var counties = await HttpMethods.HttpGetAsync<List<CountyDtoModel>>(url, token);

            counties ??= [];

            return counties;
        }


        public async Task<List<CityDtoModel>> GetCitiesAsync(CountyDtoModel counties)
        {
            var url = _url + WebConstants.GetCities + counties.Id.ToString();
            var token = LocalStorageService.GetAccessToken();

            var cities= await HttpMethods.HttpGetAsync<List<CityDtoModel>>(url, token);

            cities ??= [];

            return cities;
        }


        /*
         * SAVE
         */


        public async Task<List<StateDtoModel>> SaveStatesAsync(List<StateDtoModel> states)
        {
            var url = _url + WebConstants.PostStates;
            var token = LocalStorageService.GetAccessToken();

            var statesUpdated = await HttpMethods.HttpPostAsync<List<StateDtoModel>, List<StateDtoModel>>(url, token, states, false);

            statesUpdated ??= [];

            return statesUpdated;
        }


        public async Task<List<CountyDtoModel>> SaveCountiesAsync(List<CountyDtoModel> counties)
        {
            var url = _url + WebConstants.PostCounties;
            var token = LocalStorageService.GetAccessToken();

            var countiesUpdated = await HttpMethods.HttpPostAsync<List<CountyDtoModel>, List<CountyDtoModel>>(url, token, counties, false);

            countiesUpdated ??= [];

            return countiesUpdated;
        }


        public async Task<List<CityDtoModel>> SaveCitiesAsync(List<CityDtoModel> cities)
        {
            var url = _url + WebConstants.PostCities;
            var token = LocalStorageService.GetAccessToken();

            var citiesUpdated = await HttpMethods.HttpPostAsync<List<CityDtoModel>, List<CityDtoModel>>(url, token, cities, false);

            citiesUpdated ??= [];

            return citiesUpdated;
        }


        public async Task<List<InterestDtoModel>> SaveInterestsAsync(List<InterestDtoModel> interests)
        {
            var settingId = LocalStorageService.GetSettingId();
            var url = _url + WebConstants.GetInterests + settingId;
            var token = LocalStorageService.GetAccessToken();

            var interestsUpdated = await HttpMethods.HttpPostAsync<List<InterestDtoModel>, List<InterestDtoModel>>(url, token, interests, false);

            interestsUpdated ??= [];

            return interestsUpdated;
        }
    }
}