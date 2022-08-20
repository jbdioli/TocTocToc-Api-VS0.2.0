using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Services;

namespace TocTocToc.Shared
{
    public class Keycloak
    {
        private AuthStorageService _authStorageService = new();
        private readonly int _lenght;

        public Keycloak()
        {
        }

        public Keycloak(int length) : this()
        {
            _lenght = length;
        }

        public string GenerateState()
        {
            var returnValue = "";
            var alphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var random = new Random();

            for (var i = 0; i < _lenght; i++)
                returnValue = new string(Enumerable.Repeat(alphaNumericCharacters, _lenght)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

            return returnValue;
        }

        public string GenerateCodeVerifier()
        {
            var returnValue = "";
            var bytes = new byte[32];

            var random = new Random();

            random.NextBytes(bytes);

            returnValue = Base64Urlencode(bytes);

            return returnValue;
        }


        public string GenerateCodeChallenge(string codeVerifier)
        {
            var codeChallengeValue = "";

            var textEncoder = Encoding.GetEncoding("us-ascii", new EncoderReplacementFallback("(unknown)"),
                new DecoderReplacementFallback("(error)"));

            var encodedValue = new byte[textEncoder.GetByteCount(codeVerifier)];

            textEncoder.GetBytes(codeVerifier, 0, codeVerifier.Length, encodedValue, 0);

            var sha256Hash = SHA256.Create();

            var ch = sha256Hash.ComputeHash(encodedValue);
            codeChallengeValue = Base64Urlencode(ch);

            return codeChallengeValue;
        }


        public string GetAuthCode(string state, string codeChallenge, string clientId, string redirectUri,
            string[] scopes)
        {
            var auth = new AuthDto();
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


        public async Task<TokenDetailsDto> PostAuthorize(string state, string code)
        {
            TokenDetailsDto tokenDetails = null;

            if (state.Contains(LocalStorageService.GetState()))
                tokenDetails = await RequestToken(code);

            if (tokenDetails != null)
            {
                var tokenDateTime = DateTime.Now;
                tokenDetails.TokenDateTime = tokenDateTime;
                LocalStorageService.SaveTokenDetails(tokenDetails);
            }
            return tokenDetails;

        }

        private async Task<TokenDetailsDto> RequestToken(string code)
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


        public async Task<TokenDetailsDto> RenewToken()
        {
            IEnumerable<KeyValuePair<string, string>> postData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", AuthConstants.ClientId },
                { "client_secret", AuthConstants.ClientSecret },
                { "refresh_token", LocalStorageService.GetRefreshToken() }
            };

            var tokenDetails = await _authStorageService.RequestToken(postData);

            if (tokenDetails.AccessToken != null)
            {
                tokenDetails.TokenDateTime = DateTime.Now;
                LocalStorageService.SaveTokenDetails(tokenDetails);
            }


            return tokenDetails;
        }

        public void Logout()
        {
        }
    }


}