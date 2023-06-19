using System;
using System.Globalization;
using TocTocToc.Models.Dto;
using Xamarin.Forms;

namespace TocTocToc.Services
{
    public static class LocalStorageService
    {
        // Public ID
        private static readonly string ID_PUBLIC_USER = "id_public_user";
        private static readonly string ID_PUBLIC_BLOG = "id_public_blog";
        private static readonly string ID_PUBLIC_CONTACT_BOOK = "id_public_contact_book";
        private static readonly string ID_PUBLIC_SERVICE_EXCHANGE = "id_public_service_exchange";
        private static readonly string ID_PUBLIC_SETTING = "id_public_setting";
        
        // Auth
        private static readonly string STATE = "state";
        private static readonly string CODE_VERIFIER = "code_verifier";
        private static readonly string SCOPE = "scope";
        private static readonly string CODE = "code";
        private static readonly string ACCESS_TOKEN = "access_token";
        private static readonly string REFRESH_TOKEN = "refresh_token";
        private static readonly string EXPIRES_IN = "expires_in";
        private static readonly string REFRESH_EXPIRES_IN = "refresh_expires_in";
        private static readonly string ID_TOKEN = "id_token";
        private static readonly string TOKEN_DATE_TIME = "token_date_time";

        // Address
        private static readonly string IS_ADDRESSES = "is_addresses";

        // Setting
        private static readonly string APP_LANGUAGE = "application_language";
        private static readonly string IS_AGE = "is_age";
        private static readonly string IS_FLOOR = "is_floor";
        private static readonly string IS_JOB = "is_job";
        private static readonly string IS_MARITAL_STATUS = "is_marital_status";

        public static void SaveAuth(AuthDtoModel auth)
        {

            if (!string.IsNullOrEmpty(auth.State))
                Application.Current.Properties[STATE] = auth.State;

            if (!string.IsNullOrEmpty(auth.CodeVerifier))
                Application.Current.Properties[CODE_VERIFIER] = auth.CodeVerifier;

            if (!string.IsNullOrEmpty(auth.Scope))
                Application.Current.Properties[SCOPE] = auth.Scope;

            if (!string.IsNullOrEmpty(auth.Code))
                Application.Current.Properties[CODE] = auth.Code;

            Application.Current.SavePropertiesAsync();
        }

        public static void SaveTokenDetails(TokenDetailsDtoModel tokenDetails)
        {
            if (!string.IsNullOrEmpty(tokenDetails.AccessToken))
                Application.Current.Properties[ACCESS_TOKEN] = tokenDetails.AccessToken;

            if (!string.IsNullOrEmpty(tokenDetails.RefreshToken))
                Application.Current.Properties[REFRESH_TOKEN] = tokenDetails.RefreshToken;

            if (!string.IsNullOrEmpty(tokenDetails.ExpiresIn))
                Application.Current.Properties[EXPIRES_IN] = tokenDetails.ExpiresIn;

            if (!string.IsNullOrEmpty(tokenDetails.RefreshExpiresIn))
                Application.Current.Properties[REFRESH_EXPIRES_IN] = tokenDetails.RefreshExpiresIn;

            if (!string.IsNullOrEmpty(tokenDetails.IdToken))
                Application.Current.Properties[ID_TOKEN] = tokenDetails.IdToken;

            if (tokenDetails.TokenDateTime != new DateTime())
            {
                var utcDateTime = tokenDetails.TokenDateTime.ToUniversalTime();
                var day = utcDateTime.Day;
                var month = utcDateTime.Month;
                var year = utcDateTime.Year;
                var formattedUtcDateTime = DateTime.Parse($"{month}-{day}-{year} {utcDateTime.TimeOfDay}");
                formattedUtcDateTime = DateTime.SpecifyKind(formattedUtcDateTime, DateTimeKind.Utc);
                var dateTime = formattedUtcDateTime.ToString(CultureInfo.InvariantCulture);

                Application.Current.Properties[TOKEN_DATE_TIME] = dateTime;
            }

            Application.Current.SavePropertiesAsync();
        }

        public static void SavePublicId(UserInfoDtoModel publicId)
        {
            if (!string.IsNullOrEmpty(publicId.UserId))
                Application.Current.Properties[ID_PUBLIC_USER] = publicId.UserId;

            if (!string.IsNullOrEmpty(publicId.BlogId))
                Application.Current.Properties[ID_PUBLIC_BLOG] = publicId.BlogId;

            if (!string.IsNullOrEmpty(publicId.ContactBookId))
                Application.Current.Properties[ID_PUBLIC_CONTACT_BOOK] = publicId.ContactBookId;

            if (!string.IsNullOrEmpty(publicId.ServiceExchangeId))
                Application.Current.Properties[ID_PUBLIC_SERVICE_EXCHANGE] = publicId.ServiceExchangeId;

            if (!string.IsNullOrEmpty(publicId.SettingId))
                Application.Current.Properties[ID_PUBLIC_SETTING] = publicId.SettingId;

            Application.Current.SavePropertiesAsync();
        }


        //public static void SaveIsAddress(bool isAddress)
        //{
        //    Application.Current.Properties[IS_ADDRESS] = isAddress;

        //    Application.Current.SavePropertiesAsync();
        //}

        public static void SaveIsAddresses(bool isAddresses)
        {
            Application.Current.Properties[IS_ADDRESSES] = isAddresses;

            Application.Current.SavePropertiesAsync();
        }


        public static void SaveSetting(SettingDtoModel setting)
        {

            if (!string.IsNullOrEmpty(setting.Language))
                Application.Current.Properties[APP_LANGUAGE] = setting.Language;

            Application.Current.Properties[IS_AGE] = setting.IsAge;

            Application.Current.Properties[IS_FLOOR] = setting.IsFloor;

            Application.Current.Properties[IS_JOB] = setting.IsJob;

            Application.Current.Properties[IS_MARITAL_STATUS] = setting.IsStatus;


            Application.Current.SavePropertiesAsync();
        }


        public static void SaveSettingLanguage(SettingDtoModel setting)
        {
            if (!string.IsNullOrEmpty(setting.Language))
                Application.Current.Properties[APP_LANGUAGE] = setting.Language;
        }


        public static void SaveSettingIsAge(SettingDtoModel setting)
        {
            Application.Current.Properties[IS_AGE] = setting.IsAge;
        }

        public static void SaveSettingIsFloor(SettingDtoModel setting)
        {
            Application.Current.Properties[IS_FLOOR] = setting.IsFloor;
        }


        public static void SaveSettingIsJob(SettingDtoModel setting)
        {
            Application.Current.Properties[IS_JOB] = setting.IsJob;
        }


        public static void SaveSettingIsMaritalStatus(SettingDtoModel setting)
        {
            Application.Current.Properties[IS_MARITAL_STATUS] = setting.IsStatus;
        }

        
        //public static string GetAccessToken()
        //{
        //    if (Application.Current.Properties.ContainsKey(ACCESS_TOKEN))
        //        return (string)Application.Current.Properties[ACCESS_TOKEN];
        //    else
        //        return string.Empty;
        //}

        public static string GetAccessToken()
        {
            if (Application.Current.Properties.TryGetValue(ACCESS_TOKEN, out var property))
                return (string) property;
            else
                return string.Empty;
        }

        public static string GetRefreshToken()
        {
            if (Application.Current.Properties.TryGetValue(REFRESH_TOKEN, out var property))
                return (string)property;
            else
                return string.Empty;
        }


        public static string GetExpiresIn()
        {
            if (Application.Current.Properties.TryGetValue(EXPIRES_IN, out var property))
                return (string)property;
            else
                return string.Empty;
        }


        public static string GetRefreshExpiresIn()
        {
            if (Application.Current.Properties.TryGetValue(REFRESH_EXPIRES_IN, out var property))
                return (string)property;
            else
                return string.Empty;
        }


        public static DateTime GetTokenDateTime()
        {
            if (!Application.Current.Properties.TryGetValue(TOKEN_DATE_TIME, out var property)) return new DateTime();

            //var dateFormats = new[] { "yyyy/MM/dd", "MM/dd/yyyy", "MM/dd/yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss" };
            var dateFormats = new[] { "MM/dd/yyyy HH:mm:ss"};

            var provider = CultureInfo.InvariantCulture;

            var stringDateTime = (string)property;

            var utcDateTime = DateTime.ParseExact(stringDateTime, dateFormats, provider, DateTimeStyles.AdjustToUniversal);
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            var dateTime = utcDateTime.ToLocalTime();

            return dateTime;

        }

        public static bool IsToken()
        {
            return Application.Current.Properties.ContainsKey(ACCESS_TOKEN);
        }

        public static bool IsAddresses()
        {
            if (Application.Current.Properties.TryGetValue(IS_ADDRESSES, out var property))
                return (bool)property;
            else
                return false;
        }

        public static string GetState()
        {
            if (Application.Current.Properties.TryGetValue(STATE, out var property))
                return (string)property;
            else
                return string.Empty;
        }

        public static string GetCodeVerifier()
        {
            if (Application.Current.Properties.TryGetValue(CODE_VERIFIER, out var property))
                return (string)property;
            else
                return string.Empty;
        }

        public static string GetScope()
        {
            if (Application.Current.Properties.TryGetValue(SCOPE, out var property))
                return (string)property;
            else
                return string.Empty;
        }

        public static string GetCode()
        {
            if (Application.Current.Properties.TryGetValue(CODE, out var property))
                return (string)property;
            else
                return string.Empty;
        }

        public static string GetUserId()
        {
            if (Application.Current.Properties.TryGetValue(ID_PUBLIC_USER, out var property))
                return (string)property;
            else
                return string.Empty;
        }


        public static string GetSettingId()
        {
            if (Application.Current.Properties.TryGetValue(ID_PUBLIC_SETTING, out var property))
                return (string)property;
            else
                return string.Empty;
        }


        public static SettingDtoModel GetSetting()
        {
            var setting = new SettingDtoModel();

            // Language
            if (Application.Current.Properties.TryGetValue(APP_LANGUAGE, out var language))
                setting.Language = (string)language;
            else
                setting.Language = string.Empty;

            // IsAge
            if (Application.Current.Properties.TryGetValue(IS_AGE, out var isAge))
                setting.IsAge = (bool)isAge;
            else
                setting.IsAge = false;

            // IsFloor
            if (Application.Current.Properties.TryGetValue(IS_FLOOR, out var isFloor))
                setting.IsFloor = (bool)isFloor;
            else
                setting.IsFloor = false;

            // IsJob
            if (Application.Current.Properties.TryGetValue(IS_JOB, out var isJob))
                setting.IsJob = (bool)isJob;
            else
                setting.IsJob = false;

            // IsMaritalStatus
            if (Application.Current.Properties.TryGetValue(IS_MARITAL_STATUS, out var isMaritalStatus))
                setting.IsStatus = (bool)isMaritalStatus;
            else
                setting.IsStatus = false;


            return setting;
        }



        public static void CleanAuthStorage()
        {
            Application.Current.Properties.Remove(ACCESS_TOKEN);
            Application.Current.Properties.Remove(REFRESH_TOKEN);
            Application.Current.Properties.Remove(EXPIRES_IN);
            Application.Current.Properties.Remove(REFRESH_EXPIRES_IN);
            Application.Current.Properties.Remove(TOKEN_DATE_TIME);
            Application.Current.Properties.Remove(SCOPE);
            Application.Current.Properties.Remove(STATE);
            Application.Current.Properties.Remove(CODE_VERIFIER);
            Application.Current.Properties.Remove(CODE);
            Application.Current.Properties.Remove(ID_TOKEN);
            
        Application.Current.SavePropertiesAsync();
        }


    }
}
