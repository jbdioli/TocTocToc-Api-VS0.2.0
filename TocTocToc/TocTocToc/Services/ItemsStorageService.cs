using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Shared;

namespace TocTocToc.Services
{
    public class ItemsStorageService
    {
        private readonly string _url = WebConstants.Url;
        private string _userId = "";

        private async Task<IList<GenderDto>> GetGenders(string settingId)
        {
            //IList<GenderDto> genders = new List<GenderDto>();

            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.GetGenders + settingId;
            var token = LocalStorageService.GetAccessToken();

            IList<GenderDto> genders = await httpMethods.HttpGet<IList<GenderDto>>(url, token) ?? new List<GenderDto>();

            return genders;
        }

        private async Task<IList<MaritalStatusDto>> GetMaritalStatus(string settingId)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.GetMaritalStatus + settingId;
            var token = LocalStorageService.GetAccessToken();

            IList<MaritalStatusDto> maritalStatus = await httpMethods.HttpGet<IList<MaritalStatusDto>>(url, token) ?? new List<MaritalStatusDto>();
            
            return maritalStatus;
        }

        private async Task<IList<HousingTypeDto>> GetHousingTypes(string settingId)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.GetHousingTypes + settingId;
            var token = LocalStorageService.GetAccessToken();

            IList<HousingTypeDto> housingTypes = await httpMethods.HttpGet<IList<HousingTypeDto>>(url, token) ?? new List<HousingTypeDto>();
            
            return housingTypes;
        }



        public async Task<T> SetGenders<T>(T objectDetails)
        {
            var genderProperty = objectDetails.GetType().GetProperty("Genders");

            var settingId = LocalStorageService.GetSettingId();
            var buffer = GetGenders(settingId);
            var genderList = await buffer;
            if (genderProperty != null)
                genderProperty.SetValue(objectDetails, new List<GenderDto>(genderList));

            return objectDetails;
        }


        public async Task<T> SetMaritalStatuses<T>(T objectDetails)
        {
            var genderProperty = objectDetails.GetType().GetProperty("MaritalStatuses");

            var settingId = LocalStorageService.GetSettingId();
            var buffer = GetMaritalStatus(settingId);
            var maritalStatusList = await buffer;
            if (genderProperty != null)
                genderProperty.SetValue(objectDetails, new List<MaritalStatusDto>(maritalStatusList));

            return objectDetails;
        }


        public async Task<T> SetHousingTypes<T>(T objectDetails)
        {
            var genderProperty = objectDetails.GetType().GetProperty("HousingTypes");

            var settingId = LocalStorageService.GetSettingId();
            var buffer = GetHousingTypes(settingId);
            var housingTypeList = await buffer;
            if (genderProperty != null)
                genderProperty.SetValue(objectDetails, new List<HousingTypeDto>(housingTypeList));

            return objectDetails;
        }

    }
}