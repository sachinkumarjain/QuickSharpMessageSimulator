using System;

namespace Dell.Service.API.Client.Models.Api
{
    public class ServiceDataViewModel
    {
        public int ServiceDataId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int ServiceUrlId { get; set; }
    }
}
