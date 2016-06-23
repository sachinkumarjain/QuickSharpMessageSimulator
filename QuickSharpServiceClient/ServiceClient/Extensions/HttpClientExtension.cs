namespace Dell.Service.API.Client.Extensions
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for HttpClient
    /// </summary>
    public static class HttpClientExtension
    {
        /// <summary>
        /// Patches the asynchronous request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="iContent">HttpContent to pass</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            var response = await client.SendAsync(request);

            return response;
        }

    }
}
