using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Shared;

namespace TocTocToc.Services
{
    public class AdvertisingStorageService
    {
        public ReplaySubject<List<AdvertisingDto>> AdSubject = new(1);
        public ReplaySubject<List<AdvertisingDto>> AdvertisingListSubject = new(1);
        public ReplaySubject<AdvertisingDto> AdvertisingSubject = new(1);

        private readonly string _url;
        private readonly string _userId;

        public AdvertisingStorageService(string userId)
        {
            _userId = userId;
            _url = WebConstants.Url;
        }


        /**
         * Get user advertising
         */
        public async Task GetAdvertising()
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.GetAdvertising + _userId;
            var token = LocalStorageService.GetAccessToken();

            var advertising = await httpMethods.HttpGet<List<AdvertisingDto>>(url, token) ?? new List<AdvertisingDto>();

            AdvertisingListSubject.OnNext(advertising);
        }




        public async Task PostMedia(string userId, string image)
        {
            throw new System.NotImplementedException();
        }



        /**
        * Get world advertising
        */
        public async Task FindAds()
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.FindAdvertising + _userId;
            var token = LocalStorageService.GetAccessToken();

            var advertising = await httpMethods.HttpGet<List<AdvertisingDto>>(url, token) ?? new List<AdvertisingDto>();

            AdSubject.OnNext(advertising);
        }
    }
}
