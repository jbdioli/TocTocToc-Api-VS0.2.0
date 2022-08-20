using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Shared;

namespace TocTocToc.Services
{
    public class AddressStorageService
    {
        public ReplaySubject<List<AddressDto>> AddressesSubject = new(1);

        private readonly string _url;
        private readonly string _userId;

        public AddressStorageService(string userId)
        {
            _userId = userId;
            _url = WebConstants.Url;
        }

        public async Task GetAddress()
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.GetAddress + _userId;
            var token = LocalStorageService.GetAccessToken();

            var addresses = await httpMethods.HttpGet<List<AddressDto>>(url, token) ?? new List<AddressDto>();

            AddressesSubject.OnNext(addresses);
        }

        public async Task UpdateAddress(string addressId, AddressDto addressDto)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.UpdateAddress + addressId;
            var token = LocalStorageService.GetAccessToken();

            var addresses = await httpMethods.HttpPut<List<AddressDto>, AddressDto>(url, token, addressDto);
            if (addresses == null) return;
            AddressesSubject.OnNext(addresses);
        }

        public async Task SaveAddress(AddressDto addressDto)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.PostAddress + _userId;
            var token = LocalStorageService.GetAccessToken();

            var addresses = await httpMethods.HttpPost<List<AddressDto>, AddressDto>(url, token, addressDto);
            if (addresses == null) return;
            AddressesSubject.OnNext(addresses);
        }
    }
}