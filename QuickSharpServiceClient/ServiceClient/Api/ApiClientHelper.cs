
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Dell.Service.API.Client.Extensions;

namespace Dell.Service.API.Client.Api
{
    public static class ApiClientHelper
    {
        public const string JsonContentType = "application/json";
        public static StringContent ToHttpContent<TRequest>(this TRequest request)
        {
            return new StringContent(request.AsJson(), Encoding.UTF8, JsonContentType);
        }

        public static Uri GetUri(params string[] inputs)
        {
            return new Uri(Path.Combine(inputs));
        }

        public static bool Created(this HttpResponseMessage response)
        {
            return response.StatusCode.Equals(HttpStatusCode.Created);
        }

        public static bool NoContent(this HttpResponseMessage response)
        {
            return response.StatusCode.Equals(HttpStatusCode.NoContent);
        }

        public static bool Ok(this HttpResponseMessage response)
        {
            return response.StatusCode.Equals(HttpStatusCode.OK);
        }
    }
}
