
using Dell.Service.API.Client.Models;
using Dell.Service.API.Client.Models.Api;
using QuickSharpMessageSimulator.Models;
namespace Dell.Service.API.Client.Mappers
{
    public interface IServiceDataMapper
    {
        ServiceDataViewModel Map(ServiceData input);
        ServiceData Map(ServiceDataViewModel input);
    }

    public class ServiceDataMapper : IServiceDataMapper
    {
        public ServiceDataViewModel Map(ServiceData input)
        {
            return new ServiceDataViewModel { 
                ServiceDataId = input.ServiceDataId,
                Request = input.Request,
                Response = input.Response,
                Type = input.Type,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate,
                IsDeleted = input.IsDeleted,
                ServiceUrlId = input.ServiceUrlId
            };
        }

        public ServiceData Map(ServiceDataViewModel input)
        {
            return new ServiceData
            {
                ServiceDataId = input.ServiceDataId,
                Request = input.Request,
                Response = input.Response,
                Type = input.Type,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate,
                IsDeleted = input.IsDeleted,
                ServiceUrlId = input.ServiceUrlId
            };
        }
    }
}
