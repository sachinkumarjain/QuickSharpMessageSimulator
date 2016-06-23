using System.Linq;
using Dell.Service.API.Client.Models.Api;
using QuickSharpMessageSimulator.Models;

namespace Dell.Service.API.Client.Mappers
{
    public interface IServiceUrlMapper
    {
        ServiceUrlViewModel Map(ServiceUrl svc);
        ServiceUrl Map(ServiceUrlViewModel svc);
    }

    public class ServiceUrlMapper : IServiceUrlMapper
    {
        private readonly IServiceDataMapper _serviceDataMapper;

        public ServiceUrlMapper(IServiceDataMapper serviceDataMapper)
        {
            _serviceDataMapper = serviceDataMapper;
        }

        public ServiceUrlViewModel Map(ServiceUrl svc)
        {
            var result = new ServiceUrlViewModel
            {
                ServiceUrlId = svc.ServiceUrlId,
                Url = svc.Url,
                Method = svc.Method,
                ContentType = svc.ContentType,
                EnableVirtualization = svc.EnableVirtualization
            };

            result.Data = svc.ServiceDatas.Select(x => _serviceDataMapper.Map(x));
            return result;
        }

        public ServiceUrl Map(ServiceUrlViewModel svc)
        {
            var result = new ServiceUrl
            {
                ServiceUrlId = svc.ServiceUrlId,
                Url = svc.Url,
                Method = svc.Method,
                ContentType = svc.ContentType,
                EnableVirtualization = svc.EnableVirtualization
            };

            if (svc.Data == null) return result;

            result.ServiceDatas = svc.Data.Select(x => _serviceDataMapper.Map(x)).ToList();
            return result;
        }
    }
}
