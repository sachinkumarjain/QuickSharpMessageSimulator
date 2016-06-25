
using Dell.Service.API.Client.IoC;
using QuickSharpMessageSimulator.IoC;
namespace Dell.Service.API.Client.Api
{
    public interface IApiContainer
    {
        IApiClient Create();
    }

    public class ApiContainer : IApiContainer
    {
        private readonly IApiClient _apiClient;

        public ApiContainer(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IApiClient Create()
        {
            return _apiClient;
        }
    }
}
