
using System.Collections.Generic;
namespace Dell.Service.API.Client.Models.Api
{
    public class ServiceUrlViewModel
    {
        public int ServiceUrlId { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public bool EnableVirtualization { get; set; }

        public IEnumerable<ServiceDataViewModel> Data { get; set; }
    }
}
