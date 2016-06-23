
using Dell.Service.API.Client.IoC;
namespace Dell.Service.API.Client.Api
{
    public interface IApiContainer
    {
        IApiClient Create();
    }

    public class ApiContainer : IApiContainer
    {
        private readonly IMessageSimulationContainer _container;

        public ApiContainer(IMessageSimulationContainer container)
        {
            _container = container;
        }

        public IApiClient Create()
        {
            var client = _container.GetInstance<IApiClient>();
            return client;
        }
    }
}
