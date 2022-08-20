
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Shared;

namespace TocTocToc.Services
{
    public class UserStorageService
    {
        private readonly string _url = WebConstants.Url;

        public async Task<UserDto> GetUserRegistrationDetails()
        {
            var publicIds = new UserInfoDto();

            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);
            
            var url = _url + WebConstants.CheckUserRegistrationContext;
            var token = LocalStorageService.GetAccessToken();
            var userDetails = await httpMethods.HttpGet<UserDto>(url, token);

            if (userDetails != null)
            {
                publicIds.UserId = userDetails.UserId;
                LocalStorageService.SavePublicId(publicIds);
            }

            return userDetails;
        }


        public async Task GetUserIdsDetails(string userId)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);

            var url = _url + WebConstants.UserInfoContext + userId;
            var token = LocalStorageService.GetAccessToken();

            var publicIds = await httpMethods.HttpGet<UserInfoDto>(url, token);

            if (publicIds == null) return;
            LocalStorageService.SavePublicId(publicIds);

        }



        public async Task<UserDto> GetUserDetails(string userId)
        {
            const bool isFile = false;
            var httpMethods = new HttpMethods(isFile);
            
            var url = _url + WebConstants.GetUserDetails + userId;
            var token = LocalStorageService.GetAccessToken();

            var userDetails = await httpMethods.HttpGet<UserDto>(url, token);

            return userDetails;
        }



        public async Task<UserDto> PostPhoto(string userId, string data)
        {
            var error = new ErrorDto();
            var httpMethods = new HttpMethods(true);


            var url = _url + WebConstants.PostPhotoProfile + userId;
            var token = LocalStorageService.GetAccessToken();

            var returnData = await httpMethods.HttpPost<UserDto, string>(url, token, data);

            return returnData;
        }

        public async Task<UserDto> UpdateUser<T>(string userId, T data)
        {
            var error = new ErrorDto();
            var httpMethods = new HttpMethods(false);


            var url = _url + WebConstants.UpdateUserContext + userId;
            var token = LocalStorageService.GetAccessToken();

            var returnData = await httpMethods.HttpPut<UserDto, T>(url, token, data);

            return returnData;
        }
    }
}
