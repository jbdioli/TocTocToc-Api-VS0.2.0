using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TocTocToc.DtoModels;

namespace TocTocToc.Services
{
    class AuthStorageService
    {
        private HttpClient _httpClient = new HttpClient();
        private string _url = "https://jdeo.io:8443/auth/realms/jdeo/protocol/openid-connect/token";

        public AuthStorageService()
        {

        }

        public async Task<TokenDetailsDto> RequestToken(IEnumerable<KeyValuePair<string, string>> data)
        {
            var tokensDetails = await HttpPost(_url, data);

            return tokensDetails;
        }


        public async Task<TokenDetailsDto> HttpPost(string url, IEnumerable<KeyValuePair<string, string>> postData)
        {

            var returnObject = new TokenDetailsDto();

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(postData))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                        var response = await client.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonDataResponse = await response.Content.ReadAsStringAsync();
                            returnObject = JsonConvert.DeserializeObject<TokenDetailsDto>(jsonDataResponse);
                        }
                        else
                        {
                            throw new Exception(((int)response.StatusCode).ToString() + " - " + response.ReasonPhrase);
                        }

                        return returnObject;
                    }
                }

            }
            catch (Exception e)
            {
                OnError(e.ToString());
                return returnObject;
            }
        }


        private void OnError(string error)
        {
            Console.WriteLine("[WEBSERVICE ERROR] " + error);
        }

    }


}
