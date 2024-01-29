using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared
{
    internal static class HttpMethods
    {

        private static readonly NotificationChannelHandler NOTIFICATION_CHANNEL_HANDLER = new(new DisplayNotification());
        private static readonly TokenHandler AUTH = new(new KeycloakServerChannel());

        private static HttpClient _httpClient;
        // private static bool _isFile;
        private static ErrorDtoModel _error = new();
        private static string _bearer;

        //public HttpMethods(bool isFile)
        static HttpMethods()
        {
            InitHttps();
        }


        /*
         * POST Method
         */
        public static async Task<TB> HttpPostAsync<TA, TB>(string url, string token, TA data, bool isFile)
        {
            var result = default(TB);
            _bearer = token;

            while (true)
            {
                HttpResponseMessage response;
                try
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearer);

                    HttpContent content;

                    if (!isFile)
                    {
                        content = JsonContent(data);
                    }
                    else
                    {
                        content = FileContent(data as string);
                    }
                    response = await _httpClient.PostAsync(new Uri(url), content);
                    if (!response.IsSuccessStatusCode)
                    {
                        _error = JsonConvert.DeserializeObject<ErrorDtoModel>(response.Content.ReadAsStringAsync().Result);
                        _error ??= new ErrorDtoModel();
                        _error.StatusCode = (int)response.StatusCode;
                    }
                    response.EnsureSuccessStatusCode();

                    var jsonDataResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TB>(jsonDataResponse);

                }
                catch (HttpRequestException)
                {
                    var resultValues = await ErrorCodeHandlingAsync(result);
                    if (_error.StatusCode == 401)
                        _bearer = (string)resultValues;
                    break;
                }

                if ((int)response.StatusCode != 401)
                    break;
            }

            //_httpClient.Dispose();
            return result;
        }


        /*
         * PUT Method
         */
        public static async Task<TB> HttpPutAsync<TA, TB>(string url, string token, TA data)
        {
            var result = default(TB);
            _bearer = token;

            while (true)
            {
                HttpResponseMessage response;
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearer);

                    var content = JsonContent(data);

                    response = await _httpClient.PutAsync(new Uri(url), content);
                    if (!response.IsSuccessStatusCode)
                    {
                        _error = JsonConvert.DeserializeObject<ErrorDtoModel>(response.Content.ReadAsStringAsync().Result);
                        _error ??= new ErrorDtoModel();
                        _error.StatusCode = (int)response.StatusCode;
                    }
                    response.EnsureSuccessStatusCode();

                    var jsonDataResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TB>(jsonDataResponse);

                }
                catch (HttpRequestException)
                {
                    var resultValues = await ErrorCodeHandlingAsync(result);
                    if (_error.StatusCode == 401)
                        _bearer = (string)resultValues;
                    break;

                }

                if ((int)response.StatusCode != 401)
                    break;
            }

            //_httpClient.Dispose();
            return result;
        }


        /*
         * GET Method
         */
        public static async Task<T> HttpGetAsync<T>(string url, string token)
        {
            var result = default(T);
            _bearer = token;

            while (true)
            {
                HttpResponseMessage response;
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearer);
                    
                    response = await _httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        _error = JsonConvert.DeserializeObject<ErrorDtoModel>(response.Content.ReadAsStringAsync().Result);
                        _error ??= new ErrorDtoModel();
                        _error.StatusCode = (int)response.StatusCode;
                    }
                    response.EnsureSuccessStatusCode();

                    var content = response.Content;
                    var jsonResponse = await content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonResponse);

                }
                catch (HttpRequestException)
                {
                    var resultValues = await ErrorCodeHandlingAsync(result);
                    if (_error.StatusCode == 401)
                    {
                        _bearer = (string)resultValues;
                        break;
                    }
                    if (resultValues != null)
                    {
                        result = (T)resultValues;
                    }
                    break;
                }

                if ((int)response.StatusCode != 401)
                    break;
            }
            //_httpClient.Dispose();
            return result;
        }


        /*
         * DELETE Method
         */
        public static async Task<T> HttpDeleteAsync<T>(string url, string token)
        {
            var result = default(T);
            _bearer = token;

            while (true)
            {
                HttpResponseMessage response;
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearer);
                    
                    response = await _httpClient.DeleteAsync(url);

                    var content = response.Content;

                    if (!response.IsSuccessStatusCode)
                    {
                        _error = JsonConvert.DeserializeObject<ErrorDtoModel>(response.Content.ReadAsStringAsync().Result);
                        _error ??= new ErrorDtoModel();
                        _error.StatusCode = (int)response.StatusCode;
                    }
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonResponse);


                }
                catch (HttpRequestException)
                {
                    var resultValues = await ErrorCodeHandlingAsync(result);
                    if (_error.StatusCode == 401)
                    {
                        _bearer = (string)resultValues;
                    }
                    break;
                }

                if ((int)response.StatusCode != 401) // leave the loop
                    break;
            }
            return result;
        }




        private static void InitHttps()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = null,
                MaxResponseContentBufferSize = 256000,
                Timeout = TimeSpan.FromSeconds(15)
            };
            //_httpClient.Timeout = TimeSpan.FromSeconds(15);
            //_httpClient.MaxResponseContentBufferSize = 256000;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        private static StringContent JsonContent<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data,
                new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddThh:mm:ss.ssss zzz" });

            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }


        private static MultipartFormDataContent FileContent(string data)
        {
            var multipartFormContent = new MultipartFormDataContent();
            var fileName = Path.GetFileName(data);
            // var fileName = data.Name;
            var content = new StreamContent(File.OpenRead(data));
            //File.OpenRead(data)
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            //Add the file
            multipartFormContent.Add(content, name: "file", fileName: fileName);

            return multipartFormContent;
        }


        private static async Task<object> ErrorCodeHandlingAsync<T>(T httpResponse)
        {

            switch (_error.StatusCode)
            {
                case 401:
                    _error.Message = "401 error message";
                    var bearer = await AUTH.GetBearerAsync();
                    return bearer;
                case 404:
                    _error.Message = "404 error message";

                    if (typeof(T) == typeof(List<DictionaryDtoModel>))
                    {
                        var response = new List<DictionaryDtoModel>
                        {
                            new DictionaryDtoModel
                            {
                                Title = _error.Title,
                                Message = _error.Message,
                                Resolution = _error.Resolution
                            }
                        };

                        return response;
                    }
                    break;
                case 500:
                    NOTIFICATION_CHANNEL_HANDLER.SendNotification(ENotificationType.HttpError500, null);
                    break;
                case 503:
                    NOTIFICATION_CHANNEL_HANDLER.SendNotification(ENotificationType.HttpError503, null);
                    break;
            }

            return Task.CompletedTask;

        }



    }
}
