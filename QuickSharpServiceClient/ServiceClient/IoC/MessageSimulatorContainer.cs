using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Dell.Service.API.Client.Api;
using QuickSharpMessageSimulator.IoC;
using StructureMap;

namespace Dell.Service.API.Client.IoC
{
    public interface IMessageSimulationContainer 
    {
        T GetInstance<T>();
    }
    public class MessageSimulatorContainer : IMessageSimulationContainer
    {
        private readonly IMessageSimulateRegistry _simulateMessageRegistry;

        public MessageSimulatorContainer(IMessageSimulateRegistry simulateMessageRegistry)
        {
            _simulateMessageRegistry = simulateMessageRegistry;
            _simulateMessageRegistry.Container.Register(Component.For<IApiClient>().ImplementedBy<ApiClient>());
        }

        public T GetInstance<T>()
        {
            return _simulateMessageRegistry.Container.Resolve<T>();
        }
    }
}
