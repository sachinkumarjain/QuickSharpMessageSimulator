
using Castle.DynamicProxy;
using Castle.Windsor;
using Dell.Service.API.Client.Api;
using QuickSharpMessageSimulator.IoC;
using QuickSharpMessageSimulator.Repositories;
using StructureMap;
using StructureMap.Graph;

namespace Dell.Service.API.Client.IoC
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            //var generator = new ProxyGenerator();

            Scan(i =>
            {
                i.TheCallingAssembly();
                i.WithDefaultConventions();
                i.LookForRegistries();
            });
            //IoC Interface Registration for Classes - 
            //For<IApiClient>().Add<ApiClient>();
                
                //.DecorateWith(x => generator.CreateInterfaceProxyWithTarget<IApiClient>(x, new ServiceResponseIntercepter()));
            //For<IServiceRepository>().Add<ServiceRepository>();

            //Castle IoC container
            //For<IApiClient>().Use<ApiClient>().DecorateWith(x => ObjectFactory.Container.GetInstance<ICastleRegistry>().GetInstance<IApiClient>());
            
        }
    }
}