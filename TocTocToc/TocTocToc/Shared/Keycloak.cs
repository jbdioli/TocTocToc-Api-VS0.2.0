using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared
{
    public class Keycloak
    {
        private readonly AuthStorageService _authStorageService = new();
        private readonly int _length;

        public Keycloak()
        {
        }

        public Keycloak(int length) : this()
        {
            _length = length;
        }

        public string GenerateState()
        {

            var returnValue = Utility.AlphanumericValueRandom(_length);
            //var returnValue = "";
            //var alphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            //var random = new Random();

            //for (var i = 0; i < _length; i++)
            //    returnValue = new string(Enumerable.Repeat(alphaNumericCharacters, _length)
            //        .Select(s => s[random.Next(s.Length)]).ToArray());

            return returnValue;
        }

        public string GenerateCodeVerifier()
        {
            var bytes = new byte[32];

            var random = new Random();

            random.NextBytes(bytes);

            var returnValue = Base64Urlencode(bytes);

            return returnValue;
        }


        public string GenerateCodeChallenge(string codeVerifier)
        {
            //var codeChallengeValue = "";

            var textEncoder = Encoding.GetEncoding("us-ascii", new EncoderReplacementFallback("(unknown)"),
                new DecoderReplacementFallback("(error)"));

            var encodedValue = new byte[textEncoder.GetByteCount(codeVerifier)];

            textEncoder.GetBytes(codeVerifier, 0, codeVerifier.Length, encodedValue, 0);

            var sha256Hash = SHA256.Create();

            var ch = sha256Hash.ComputeHash(encodedValue);
            var codeChallengeValue = Base64Urlencode(ch);

            return codeChallengeValue;
        }


        public string GetAuthCode(string state, string codeChallenge, string clientId, string redirectUri,
            string[] scopes)
        {
            var auth = new AuthDtoModel();
            var scope = string.Join(" ", scopes);
            auth.Scope = scope;
            LocalStorageService.SaveAuth(auth);

            var url = "https://jdeo.io:8443/auth/realms/jdeo/protocol/openid-connect/auth";
            url += "?client_id=" + clientId;
            url += "&response_type=code";
            url += "&scope=" + scope;
            url += "&redirect_uri=" + redirectUri;
            url += "&state=" + state;
            url += "&code_challenge=" + codeChallenge;
            url += "&code_challenge_method=S256";

            return url;
        }


        private string Base64Urlencode(byte[] bytes)
        {
            var value = Convert.ToBase64String(bytes);

            var returnValue = value.Replace("+", "-").Replace("/", "_").Replace("=", "");

            return returnValue;
        }


        public async Task<TokenDetailsDtoModel> PostAuthorize(string state, string code)
        {
            TokenDetailsDtoModel tokenDetails = null;

            if (state.Contains(LocalStorageService.GetState()))
                tokenDetails = await RequestToken(code);

            if (tokenDetails == null) return null;

            tokenDetails.TokenDateTime = DateTime.Now;
            //LocalStorageService.SaveTokenDetails(tokenDetails);
            return tokenDetails;

        }

        private async Task<TokenDetailsDtoModel> RequestToken(string code)
        {
            IEnumerable<KeyValuePair<string, string>> postData = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", AuthConstants.ClientId },
                { "client_secret", AuthConstants.ClientSecret },
                { "code", code },
                { "redirect_uri", AuthConstants.RedirectUri },
                { "scope", LocalStorageService.GetScope() },
                { "code_verifier", LocalStorageService.GetCodeVerifier() }
            };

            var tokenDetails = await _authStorageService.RequestToken(postData);

            return tokenDetails;
        }


        public async Task<TokenDetailsDtoModel> RenewToken()
        {
            IEnumerable<KeyValuePair<string, string>> postData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", AuthConstants.ClientId },
                { "client_secret", AuthConstants.ClientSecret },
                { "refresh_token", LocalStorageService.GetRefreshToken() }
            };

            var tokenDetails = await _authStorageService.RequestToken(postData);

            if (tokenDetails.AccessToken == null) return tokenDetails;

            tokenDetails.TokenDateTime = DateTime.Now;
            LocalStorageService.SaveTokenDetails(tokenDetails);

            return tokenDetails;
        }

        public void Logout()
        {
        }
    }


}