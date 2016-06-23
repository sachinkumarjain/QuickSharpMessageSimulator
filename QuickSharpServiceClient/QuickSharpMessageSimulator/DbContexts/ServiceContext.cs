using System.Data.Entity;

namespace QuickSharpMessageSimulator.DbContexts
{
    using Models;

    public class ServiceContext : DbContext
    {
        public ServiceContext()
            : base("name=ServiceApiDbConnectionString")
        {
        }

        public IDbSet<ServiceUrl> ServiceUrls { get; set; }
        public IDbSet<ServiceData> ServiceDatas { get; set; }
    }
}
