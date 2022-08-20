namespace TocTocToc.Shared
{
    public static class WebConstants
    {
        public static readonly string Url = "https://eureka.jdeo.io:9090/toctoctoc-app-ws/";
        public static readonly string CheckUserRegistrationContext = "users/connect";
        public static readonly string UpdateUserContext = "users/update/";
        public static readonly string UserInfoContext = "users/info/";
        public static readonly string PostPhotoProfile = "users/upload/";
        public static readonly string DeleteUserContext = "users/";
        public static readonly string GetUserDetails = "users/";
        public static readonly string GetGenders = "items/gender/";
        public static readonly string GetMaritalStatus = "items/maritalstatus/";
        public static readonly string GetHousingTypes = "items/housingtype/";
        public static readonly string GetAddress = "address/";
        public static readonly string PostAddress = "address/";
        public static readonly string UpdateAddress = "address/";
        public static readonly string ActivateAddress = "address/activate/";
        public static readonly string GetAdvertising = "advertisement/advertising/";
        public static readonly string FindAdvertising = "advertisement/ads/";
        public static readonly string DeleteAdvertising = "advertisement/";
        public static readonly string UpdateAdvertising = "advertisement/update/";
        public static readonly string UploadAdvertising = "advertisement/upload/";
    }
}
