using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TocTocToc.DtoModels;
using Xamarin.Forms;

namespace TocTocToc.Services
{
    public static class LocalStorageService
    {

        private static readonly string ID_PUBLIC_USER = "id_public_user";
        private static readonly string ID_PUBLIC_BLOG = "id_public_blog";
        private static readonly string ID_PUBLIC_CONTACT_BOOK = "id_public_contact_book";
        private static readonly string ID_PUBLIC_SERVICE_EXCHANGE = "id_public_service_exchange";
        private static readonly string ID_PUBLIC_SETTING = "id_public_setting";
        private static readonly string STATE = "state";
        private static readonly string CODE_VERIFIER = "code_verifier";
        private static readonly string SCOPE = "scope";
        private static readonly string CODE = "code";
        private static readonly string ACCESS_TOKEN = "access_token";
        private static readonly string REFRESH_TOKEN = "refresh_token";
        private static readonly string EXPIRES_IN = "expires_in";
        private static readonly string REFRESH_EXPIERS_IN = "refresh_expires_in";
        private static readonly string ID_TOKEN = "id_token";
        private static readonly string TOKEN_DATE_TIME = "token_date_time";

        // private static readonly string IS_ADDRESS = "is_address";
        private static readonly string IS_ADDRESSES = "is_addresses";


        public static void SaveAuth(AuthDto auth)
        {

            if (!String.IsNullOrEmpty(auth.State))
                Application.Current.Properties[STATE] = auth.State;

            if (!String.IsNullOrEmpty(auth.CodeVerifier))
                Application.Current.Properties[CODE_VERIFIER] = auth.CodeVerifier;

            if (!String.IsNullOrEmpty(auth.Scope))
                Application.Current.Properties[SCOPE] = auth.Scope;

            if (!String.IsNullOrEmpty(auth.Code))
                Application.Current.Properties[CODE] = auth.Code;

            Application.Current.SavePropertiesAsync();
        }

        public static void SaveTokenDetails(TokenDetailsDto tokenDetails)
        {
            if (!String.IsNullOrEmpty(tokenDetails.AccessToken))
                Application.Current.Properties[ACCESS_TOKEN] = tokenDetails.AccessToken;

            if (!String.IsNullOrEmpty(tokenDetails.RefreshToken))
                Application.Current.Properties[REFRESH_TOKEN] = tokenDetails.RefreshToken;

            if (!String.IsNullOrEmpty(tokenDetails.ExpiresIn))
                Application.Current.Properties[EXPIRES_IN] = tokenDetails.ExpiresIn;

            if (!String.IsNullOrEmpty(tokenDetails.RefreshExpiresIn))
                Application.Current.Properties[REFRESH_EXPIERS_IN] = tokenDetails.RefreshExpiresIn;

            if (!String.IsNullOrEmpty(tokenDetails.IdToken))
                Application.Current.Properties[ID_TOKEN] = tokenDetails.IdToken;

            if (tokenDetails.TokenDateTime != new DateTime())
            {
                var dateTime = tokenDetails.TokenDateTime.ToString("dd/MM/yyyy HH:mm:ss");
                Application.Current.Properties[TOKEN_DATE_TIME] = dateTime;
            }

            Application.Current.SavePropertiesAsync();
        }

        public static void SavePublicId(UserInfoDto publicId)
        {
            if (!String.IsNullOrEmpty(publicId.UserId))
                Application.Current.Properties[ID_PUBLIC_USER] = publicId.UserId;

            if (!String.IsNullOrEmpty(publicId.BlogId))
                Application.Current.Properties[ID_PUBLIC_BLOG] = publicId.BlogId;

            if (!String.IsNullOrEmpty(publicId.ContactBookId))
                Application.Current.Properties[ID_PUBLIC_CONTACT_BOOK] = publicId.ContactBookId;

            if (!String.IsNullOrEmpty(publicId.ServiceExchangeId))
                Application.Current.Properties[ID_PUBLIC_SERVICE_EXCHANGE] = publicId.ServiceExchangeId;

            if (!String.IsNullOrEmpty(publicId.SettingId))
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

        public static string GetAccessToken()
        {
            if (Application.Current.Properties.ContainsKey(ACCESS_TOKEN))
                return (string) Application.Current.Properties[ACCESS_TOKEN];
            else
                return String.Empty;
        }

        public static string GetRefreshToken()
        {
            if (Application.Current.Properties.ContainsKey(REFRESH_TOKEN))
                return (string)Application.Current.Properties[REFRESH_TOKEN];
            else
                return String.Empty;
        }

        public static string GetExpiresIn()
        {
            if (Application.Current.Properties.ContainsKey(EXPIRES_IN))
                return (string)Application.Current.Properties[EXPIRES_IN];
            else
                return String.Empty;
        }

        public static string GetRefreshExpiersIn()
        {
            if (Application.Current.Properties.ContainsKey(REFRESH_EXPIERS_IN))
                return (string)Application.Current.Properties[REFRESH_EXPIERS_IN];
            else
                return String.Empty;
        }


        public static DateTime GetTokenDateTime()
        {
            if (Application.Current.Properties.ContainsKey(TOKEN_DATE_TIME))
            {
                var dateTime = (string)Application.Current.Properties[TOKEN_DATE_TIME];
                return DateTime.Parse(dateTime);
            }
            else
                return new DateTime();
        }


        public static bool IsToken()
        {
            if (Application.Current.Properties.ContainsKey(ACCESS_TOKEN))
                return true;
            else
                return false;
        }

        //public static bool IsAddress()
        //{
        //    if (Application.Current.Properties.ContainsKey(IS_ADDRESS))
        //        return (bool)Application.Current.Properties[IS_ADDRESS];
        //    else
        //        return false;
        //}

        public static bool IsAddresses()
        {
            if (Application.Current.Properties.ContainsKey(IS_ADDRESSES))
                return (bool)Application.Current.Properties[IS_ADDRESSES];
            else
                return false;
        }

        public static string GetState()
        {
            if (Application.Current.Properties.ContainsKey(STATE))
                return (string)Application.Current.Properties[STATE];
            else
                return String.Empty;
        }

        public static string GetCodeVerifier()
        {
            if (Application.Current.Properties.ContainsKey(CODE_VERIFIER))
                return (string)Application.Current.Properties[CODE_VERIFIER];
            else
                return String.Empty;
        }

        public static string GetScope()
        {
            if (Application.Current.Properties.ContainsKey(SCOPE))
                return (string)Application.Current.Properties[SCOPE];
            else
                return String.Empty;
        }

        public static string GetCode()
        {
            if (Application.Current.Properties.ContainsKey(CODE))
                return (string)Application.Current.Properties[CODE];
            else
                return String.Empty;
        }

        public static string GetUserId()
        {
            if (Application.Current.Properties.ContainsKey(ID_PUBLIC_USER))
                return (string)Application.Current.Properties[ID_PUBLIC_USER];
            else
                return String.Empty;
        }


        public static string GetSettingId()
        {
            if (Application.Current.Properties.ContainsKey(ID_PUBLIC_SETTING))
                return (string)Application.Current.Properties[ID_PUBLIC_SETTING];
            else
                return String.Empty;
        }




        public static void CleanAuthStorage()
        {
            Application.Current.Properties.Remove(ACCESS_TOKEN);
            Application.Current.Properties.Remove(REFRESH_TOKEN);
            Application.Current.Properties.Remove(EXPIRES_IN);
            Application.Current.Properties.Remove(REFRESH_EXPIERS_IN);
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
