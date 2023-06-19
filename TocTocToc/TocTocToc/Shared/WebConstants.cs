namespace TocTocToc.Shared
{
    public static class WebConstants
    {
        public static readonly string Url = "https://eureka.jdeo.io:9090/toctoctoc-app-ws";

        // User
        public static readonly string CheckUserRegistrationContext = "/users/connect";
        public static readonly string UpdateUserContext = "/users/update/";
        public static readonly string UserInfoContext = "/users/info/";
        public static readonly string PostPhotoProfile = "/users/upload/";
        public static readonly string DeleteUserContext = "/users/";
        public static readonly string GetUserDetails = "/users/";
        
        // Items
        public static readonly string GetGenders = "/items/genders/";
        public static readonly string GetMaritalStatus = "/items/maritalstatus/";
        public static readonly string GetHousingTypes = "/items/housingtypes/";
        public static readonly string GetInterests = "/items/interests/";
        public static readonly string PostInterests = "/items/interests/";
        public static readonly string GetCountries = "/items/countries/";
        public static readonly string GetStates = "/items/states/";
        public static readonly string PostStates = "/items/states/";
        public static readonly string GetCounties = "/items/counties/";
        public static readonly string PostCounties = "/items/counties/";
        public static readonly string GetCities = "/items/cities/";
        public static readonly string PostCities = "/items/cities/";
        
        // Address
        public static readonly string GetAddress = "/address/";
        public static readonly string PostAddress = "/address/";
        public static readonly string UpdateAddress = "/address/";
        public static readonly string DeleteAddress = "/address/";
        public static readonly string ActivateAddress = "/address/activate";
        
        // Advertisement
        public static readonly string GetAdvertising = "/advertisement/advertising/";
        public static readonly string FindAdvertising = "/advertisement/ads/";
        public static readonly string DeleteAdvertising = "/advertisement/";
        public static readonly string UpdateAdvertising = "/advertisement/update/";
        public static readonly string UploadMediaAdvertising = "/advertisement/upload/";
        public static readonly string UploadUpdateMediaAdvertising = "/advertisement/upload/update/";
        
        // Setting
        public const string GetSetting = "/setting/";
        public const string UpdateSetting = "/setting/update/";
        public const string UpdateSettingLanguage = "/setting/update/language/";
        public const string UpdateSettingIsFloor = "/setting/update/floor/";
        public const string UpdateSettingIsAge = "/setting/update/age/";
        public const string UpdateSettingIsMaritalStatus = "/setting/update/maritalstatus/";
        public const string UpdateSettingIsJob = "/setting/update/job/";
    }
}
