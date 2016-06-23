using System.Web.Http;
using Dell.Service.API.Client.Mappers;
using Dell.Service.API.Client.Models.Api;
using QuickSharpMessageSimulator.Repositories;

namespace Dell.Service.API.Client.Controllers
{
    public class RecordsController : ApiController
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceUrlMapper _serviceUrlMapper;
        private readonly IServiceDataMapper _serviceDataMapper;

        public RecordsController(IServiceRepository serviceRepository, 
            IServiceUrlMapper serviceUrlMapper,
            IServiceDataMapper serviceDataMapper)
        {
            _serviceRepository = serviceRepository;
            _serviceUrlMapper = serviceUrlMapper;
            _serviceDataMapper = serviceDataMapper;
        }

        [HttpPost]
        [Route("api/records")]
        public void Record(ServiceUrlViewModel request)
        {
            var source = _serviceUrlMapper.Map(request);
            _serviceRepository.Add(source);
        }

        [HttpPut]
        [Route("api/records")]
        public void Update(ServiceUrlViewModel request)
        {
            var source = _serviceUrlMapper.Map(request);
            _serviceRepository.Update(source);
        }

        [HttpPatch]
        [Route("api/records")]
        public void Patch(ServiceDataViewModel request)
        {
            var source = _serviceDataMapper.Map(request);
            _serviceRepository.Patch(source);
        }
    }
}
