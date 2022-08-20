using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TocTocToc.Shared
{
    internal class HttpMethods
    {
        private readonly Auth _auth;

        private HttpClient _httpClient;
        private readonly bool _isFile;

        public HttpMethods(bool isFile)
        {
            _auth = new Auth();

            _isFile = isFile;
            InitHttps();

            // DisableHttps();
        }



        public async Task<TA> HttpPost<TA, TB>(string url, string token, TB data)
        {
            var result = default(TA);
            var bearer = token;
            HttpResponseMessage response = new();

            while (true)
            {
                try
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    HttpContent content;

                    if (!_isFile)
                    {
                        content = JsonContent(data);
                    }
                    else
                    {
                        content = FileContent(data as string);
                    }

                    response = await _httpClient.PostAsync(new Uri(url), content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonDataResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<TA>(jsonDataResponse);
                    }
                    else
                    {
                        var returnValue = ErrorCodeHandling(response);
                        bearer = await returnValue;
                        if (String.IsNullOrEmpty(bearer))
                            break;
                    }
                    
                }
                catch (Exception e)
                {
                    OnError(e.ToString());

                }

                if ((int)response.StatusCode != 401)
                    break;
            }

            return result;
        }



        public async Task<TA> HttpPut<TA, TB>(string url, string token, TB data)
        {
            var result = default(TA);
            var bearer = token;
            HttpResponseMessage response = new();

            while (true)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    var content = JsonContent(data);

                    response = await _httpClient.PutAsync(new Uri(url), content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonDataResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<TA>(jsonDataResponse);
                    }
                    else
                    {
                        var returnValue = ErrorCodeHandling(response);
                        bearer = await returnValue;
                        if (String.IsNullOrEmpty(bearer))
                            break;
                    }

                }
                catch (Exception e)
                {
                    OnError(e.ToString());

                }

                if ((int)response.StatusCode != 401)
                    break;
            }

            return result;
        }



        public async Task<T> HttpGet<T>(string url, string token)
        {
            var result = default(T);
            var bearer = token;
            HttpResponseMessage response = new();

            while (true)
            {

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                    
                    response = await _httpClient.GetAsync(url);

                    var content = response.Content;

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(jsonResponse);
                    }
                    else
                    {
                        var returnValue = ErrorCodeHandling(response);
                        bearer = await returnValue;
                        if (String.IsNullOrEmpty(bearer))
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ HTTPS ERROR ] ", ex.ToString());
                    OnError(ex.ToString());

                }

                if ((int)response.StatusCode != 401)
                    break;
            }
            return result;
        }



        public async Task<T> HttpDelete<T>(string url, string token)
        {
            var result = default(T);
            var bearer = token;
            HttpResponseMessage response = new();

            while (true)
            {

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    response = await _httpClient.DeleteAsync(url);

                    var content = response.Content;

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(jsonResponse);
                    }
                    else
                    {
                        var returnValue = ErrorCodeHandling(response);
                        bearer = await returnValue;
                        if (String.IsNullOrEmpty(bearer)) break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ HTTPS ERROR ] ", ex.ToString());
                    OnError(ex.ToString());

                }

                if ((int)response.StatusCode != 401)
                    break;
            }
            return result;
        }







        private static void OnError(string error)
        {
            Console.WriteLine("[WEBSERVICE ERROR] " + error);
        }

        //private void DisableHttps()
        //{
        //    var httpHandler = new HttpClientHandler()
        //    {
        //        ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
        //    };

        //    _httpClient = new HttpClient(httpHandler);
        //    _httpClient.Timeout = TimeSpan.FromSeconds(15);
        //    _httpClient.MaxResponseContentBufferSize = 256000;
        //    //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //}

        private void InitHttps()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
            _httpClient.MaxResponseContentBufferSize = 256000;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        private static StringContent JsonContent<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            return new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
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


        private async Task<string> ErrorCodeHandling(HttpResponseMessage response)
        {
            string bearer = null;
            var statusCode = (int)response.StatusCode;

            if (statusCode == 401)
            {
                var value = _auth.RefereshToken();
                bearer = await value;
            }
            else
            {
                var reasonPhrase = (string)response.ReasonPhrase;
                throw new Exception((statusCode + " - " + reasonPhrase));
            }

            return bearer;
        }



    }
}
