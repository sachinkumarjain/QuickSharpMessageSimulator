using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dell.Service.API.Client.Extensions;
using Dell.Service.API.Client.IoC;
using QuickSharpMessageSimulator;
using QuickSharpMessageSimulator.Interceptors;

namespace Dell.Service.API.Client.Api
{
    public interface IApiClient : IDisposable
    {
        TimeSpan Timeout { get; set; }

        bool EnableVirtualizationToAll { get; set; }
        bool DisableVirtualizationToAll { get; set; }

        //IApiClient Create();

        void AddHeader(string key, string value);

        TResponse GetSync<TResponse>(Uri identifier) where TResponse : class;

        Task<TResponse> GetAsync<TResponse>(Uri identifier) where TResponse : class;

        Task<HttpResponseMessage> PostAsync<TRequest>(Uri identifier, TRequest httpContentRequest);
        Task<TResponse> PostAsync<TRequest, TResponse>(Uri identifier, TRequest postData);

        Task<HttpResponseMessage> PatchAsync<TRequest>(Uri identifier, TRequest httpContentRequest);

        Task<TResponse> PatchAsync<TRequest, TResponse>(Uri identifier, TRequest postData);

        Task<HttpResponseMessage> PutAsync<TRequest>(Uri identifier, TRequest httpContentRequest);

        Task<TResponse> PutAsync<TRequest, TResponse>(Uri identifier, TRequest postData);
    }

    [ExcludeFromCodeCoverage]
    [EnableMessageSimulation]
    public class ApiClient : IApiClient
    {
        private HttpClient _httpClient;
        private bool _disposed;

        public TimeSpan Timeout
        {
            get { return _httpClient.Timeout; }
            set { _httpClient.Timeout = value; }
        }

        public bool EnableVirtualizationToAll { get; set; }
        public bool DisableVirtualizationToAll { get; set; }

        public ApiClient()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(120),
                MaxResponseContentBufferSize = 256000
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiClientHelper.JsonContentType));
        }

        public void AddHeader(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(key, value);
        }

        [EnableMessageSimulation]
        public TResponse GetSync<TResponse>(Uri identifier) where TResponse : class
        {
            var result = _httpClient.GetAsync(identifier).Result;

            var response = result.Content.ReadAsStringAsync().Result.FromJson<TResponse>();

            return response;
        }

        [EnableMessageSimulation]
        public async Task<TResponse> GetAsync<TResponse>(Uri identifier) where TResponse : class
        {
            var response = await DecodeResponseMessageAsync<TResponse>((await _httpClient.GetAsync(identifier).ConfigureAwait(false)).Content).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<TRequest>(Uri identifier, TRequest httpContentRequest)
        {

            return await _httpClient.PostAsync(identifier, httpContentRequest.ToHttpContent());
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(Uri identifier, TRequest postData)
        {
            return await DecodeResponseMessageAsync<TResponse>((await _httpClient.PostAsJsonAsync(identifier.ToString(), postData).ConfigureAwait(false)).Content).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchAsync<TRequest>(Uri identifier, TRequest httpContentRequest)
        {
            return await _httpClient.PatchAsync(identifier, httpContentRequest.ToHttpContent());
        }

        public async Task<TResponse> PatchAsync<TRequest, TResponse>(Uri identifier, TRequest postData)
        {
            return await DecodeResponseMessageAsync<TResponse>((await _httpClient.PatchAsync(identifier, postData.ToHttpContent()).ConfigureAwait(false)).Content).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutAsync<TRequest>(Uri identifier, TRequest httpContentRequest)
        {
            return await _httpClient.PutAsync(identifier, httpContentRequest.ToHttpContent());
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(Uri identifier, TRequest postData)
        {
            return await DecodeResponseMessageAsync<TResponse>((await _httpClient.PutAsJsonAsync(identifier.ToString(), postData).ConfigureAwait(false)).Content).ConfigureAwait(false);
        }

        private static async Task<TResponse> DecodeResponseMessageAsync<TResponse>(HttpContent content)
        {
            return await content.ReadAsAsync<TResponse>().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            if (_httpClient != null)
            {
                var hc = _httpClient;
                _httpClient = null;
                hc.Dispose();
            }

            _disposed = true;
        }

    }


    public class EnableServiceVirtualizationAttribute : System.Attribute
    {

    }
}
