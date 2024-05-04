namespace WorkChronicle.WebApiClients
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public partial class WebApiClient : IDisposable
    {
        private readonly string baseUrl;

        private HttpClient httpClient;

        public WebApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;

            Initialize(TimeSpan.FromSeconds(600));
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }

        private T Get<T>(string url)
        {
            var result = httpClient.GetAsync(url).Result;
            CheckResponse(result);

            return DeserializeObject<T>(result);
        }

        private async Task<TOut> GetAsync<TOut>(string url)
        {
            var result = await httpClient.GetAsync(url);
            await CheckResponseAsync(result);

            return await DeserializeObjectAsync<TOut>(result);
        }

        private void Get(string url)
        {
            var result = httpClient.GetAsync(url).Result;
            CheckResponse(result);
        }

        private async Task GetAsync(string url)
        {
            var result = await httpClient.GetAsync(url);
            await CheckResponseAsync(result);
        }

        private TOut Post<TIn, TOut>(string url, TIn parameter, bool includeModelState = false)
        {
            var result = httpClient.PostAsync(url, CreateJsonContent(parameter)).Result;
            CheckResponse(result, includeModelState);

            return DeserializeObject<TOut>(result);
        }

        private async Task<TOut> PostAsync<TIn, TOut>(string url, TIn parameter, bool includeModelState = false)
        {
            var result = await httpClient.PostAsync(url, CreateJsonContent(parameter));
            await CheckResponseAsync(result, includeModelState);

            return await DeserializeObjectAsync<TOut>(result);
        }

        private async Task<TOut> PostAsync<TIn, TOut>(string url, TIn parameter, TimeSpan timeout)
        {
            if (httpClient.Timeout != timeout)
            {
                Initialize(timeout);
            }

            var result = await httpClient.PostAsync(url, CreateJsonContent(parameter));
            await CheckResponseAsync(result);

            return await DeserializeObjectAsync<TOut>(result);
        }

        private void Post<T>(string url, T parameter)
        {
            var result = httpClient.PostAsync(url, CreateJsonContent(parameter)).Result;
            CheckResponse(result);
        }

        private async Task PostAsync<T>(string url, T parameter, bool includeModelState = false)
        {
            var result = await httpClient.PostAsync(url, CreateJsonContent(parameter));
            await CheckResponseAsync(result, includeModelState);
        }

        private void Initialize(TimeSpan? timeout = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                var message = "Base address is null or empty.";

                throw new Exception(message);
            }

            httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = timeout ?? TimeSpan.FromSeconds(600);
        }

        private void CheckResponse(HttpResponseMessage response, bool includeModelState = false)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty, ModelState = new System.Collections.Generic.Dictionary<string, string[]>() });

            Exception exception;
            if (includeModelState && error.ModelState != null && error.ModelState.Any())
            {
                exception = new Exception(string.Join("\r\n", error.ModelState.Select(x => string.Join("\r\n", x.Value))));
            }
            else
            {
                exception = new Exception($"Request to '{response.RequestMessage.RequestUri}' returns code: '{response.StatusCode}'." + "\n" + error?.Message);
            }

            exception.Data.Add("ResponseError", error?.Message);
            throw exception;
        }

        private async Task CheckResponseAsync(HttpResponseMessage response, bool includeModelState = false)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            var error = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new { Message = string.Empty, ModelState = new System.Collections.Generic.Dictionary<string, string[]>() });

            Exception exception;
            if (includeModelState && error.ModelState != null && error.ModelState.Any())
            {
                exception = new Exception(string.Join("\r\n", error.ModelState.Select(x => string.Join("\r\n", x.Value))));
            }
            else
            {
                exception = new Exception($"Request to '{response.RequestMessage.RequestUri}' returns code: '{response.StatusCode}'." + "\n" + error?.Message);
            }

            exception.Data.Add("ResponseError", error?.Message);
            throw exception;
        }

        private StringContent CreateJsonContent(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        private T DeserializeObject<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        private async Task<T> DeserializeObjectAsync<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
