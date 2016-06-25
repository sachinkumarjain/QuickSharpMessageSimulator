using System;
using System.Threading;
using System.Web.Http;
using Castle.DynamicProxy;
using Castle.Windsor;
using Dell.Service.API.Client.Api;
using QuickSharpMessageSimulator;
using QuickSharpMessageSimulator.Intercepters;
using QuickSharpMessageSimulator.IoC;
using StructureMap;
using StructureMap.Graph;

namespace Dell.Service.API.Client.IoC
{
    public class ObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
        new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            //var generator = new ProxyGenerator();
            
            return new Container(i =>
            {
                i.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                    s.AddAllTypesOf<ApiController>();
                });


                //Message Simulator registry
                i.For<IMessageSimulateRegistry>().Add<MessageSimulateRegistry>();

                //if used WithDefaultConventions setting and .Use() is required here.
                i.For<IApiClient>().Use(MessageSimulateRegistry.Instance<IApiClient>());
            });
        }
    }
}
