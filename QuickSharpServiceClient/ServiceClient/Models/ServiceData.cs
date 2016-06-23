using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dell.Service.API.Client.Models
{
    [Table("ServiceDatas")]
    public class ServiceData
    {
        public ServiceData()
        {
        }

        [Key]
        public int ServiceDataId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        //Foreign key
        public int ServiceUrlId { get; set; }
        public ServiceUrl  ServiceUrl { get; set; }
    }
}
