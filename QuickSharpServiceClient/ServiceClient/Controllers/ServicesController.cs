using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dell.Service.API.Client.Mappers;
using Dell.Service.API.Client.Models.Api;
using QuickSharpMessageSimulator.Repositories;

namespace Dell.Service.API.Client.Controllers
{
    public class ServicesController : ApiController
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceUrlMapper _serviceUrlMapper;

        public ServicesController(IServiceRepository serviceRepository, IServiceUrlMapper serviceUrlMapper)
        {
            _serviceRepository = serviceRepository;
            _serviceUrlMapper = serviceUrlMapper;
        }

        [HttpGet]
        [Route("api/services")]
        public HttpResponseMessage GetAll()
        {
            var result = _serviceRepository.Find().Select(x => _serviceUrlMapper.Map(x));

            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpGet]
        [Route("api/services/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var svc = _serviceRepository.Find(id);

            var result = _serviceUrlMapper.Map(svc);

            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("api/services")]
        public HttpResponseMessage GetByUrl(ServiceUrlRequest request)
        {
            var result = _serviceRepository.Find(request.Url, request.Method).Select(x => _serviceUrlMapper.Map(x));
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        
    }
}
