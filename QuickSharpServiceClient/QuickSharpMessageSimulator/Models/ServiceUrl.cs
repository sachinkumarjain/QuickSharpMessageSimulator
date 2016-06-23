using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickSharpMessageSimulator.Models
{
    [Table("ServiceUrls")]
    public class ServiceUrl
    {
        public ServiceUrl()
        {
            this.ServiceDatas = new HashSet<ServiceData>();
        }

        [Key]
        public int ServiceUrlId { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public bool EnableVirtualization { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ServiceData> ServiceDatas { get; set; }
    }
}
