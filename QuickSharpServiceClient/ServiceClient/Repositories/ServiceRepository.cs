using System.Collections.Generic;
using System.Linq;
using Dell.Service.API.Client.DbContexts;
using Dell.Service.API.Client.Models;

namespace Dell.Service.API.Client.Repositories
{
    public interface IServiceRepository
    {
        void Add(ServiceUrl serviceUrl);
        void Update(ServiceUrl serviceUrl);
        void Patch(ServiceData serviceData);
        void Remove(ServiceUrl serviceUrl);
        IEnumerable<ServiceUrl> Find();
        ServiceUrl Find(int id);
        IEnumerable<ServiceUrl> Find(string url, string method);
    }

    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceContext _dbContext;

        public ServiceRepository(ServiceContext context)
        {
            _dbContext = context;
        }

        public void Add(ServiceUrl serviceUrl)
        {
            _dbContext.ServiceUrls.Add(serviceUrl);
            _dbContext.SaveChanges();
        }

        public void Update(ServiceUrl serviceUrl)
        {
            var services = from svc in _dbContext.ServiceUrls
                           where svc.ServiceUrlId == serviceUrl.ServiceUrlId
                           select new ServiceUrl
                           {
                               Url = serviceUrl.Url,
                               Method = serviceUrl.Method,
                               ContentType = serviceUrl.ContentType,
                               EnableVirtualization = serviceUrl.EnableVirtualization,
                               ServiceDatas = serviceUrl.ServiceDatas
                           };

            _dbContext.SaveChanges();
        }

        public void Patch(ServiceData serviceData)
        {
            var services = from svc in _dbContext.ServiceDatas
                           where svc.ServiceUrlId == serviceData.ServiceUrlId && svc.ServiceDataId == serviceData.ServiceDataId
                           select new ServiceData
                           {
                               Request = serviceData.Request,
                               Response = serviceData.Response,
                               Type = serviceData.Type,
                               ModifiedDate = serviceData.ModifiedDate,
                               IsDeleted = serviceData.IsDeleted
                           };

            _dbContext.SaveChanges();
        }

        public void Remove(ServiceUrl serviceUrl)
        {
            _dbContext.ServiceUrls.Remove(serviceUrl);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ServiceUrl> Find()
        {
            return (from svc in _dbContext.ServiceUrls
                    select svc).ToList();
        }

        public ServiceUrl Find(int id)
        {
            return _dbContext.ServiceUrls.Find(id);
        }

        public IEnumerable<ServiceUrl> Find(string url, string method)
        {
            IEnumerable<ServiceUrl> result;
            result = (from svc in _dbContext.ServiceUrls
                      where svc.Url == url && svc.Method == method
                      select svc).ToList();
            return result;
        }
    }
}
