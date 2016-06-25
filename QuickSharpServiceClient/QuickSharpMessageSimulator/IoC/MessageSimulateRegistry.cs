using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using QuickSharpMessageSimulator.DbContexts;
using QuickSharpMessageSimulator.Intercepters;
using QuickSharpMessageSimulator.Interceptors;
using QuickSharpMessageSimulator.Repositories;

namespace QuickSharpMessageSimulator.IoC
{
    public interface IMessageSimulateRegistry
    {
        T GetInstance<T>();
        IWindsorContainer Container { get; }
    }

    public class MessageSimulateRegistry : IMessageSimulateRegistry
    {
        private readonly IWindsorContainer _container;

        public IWindsorContainer Container { get { return _container; } }

        public static T Instance<T>()
        {
            var generator = new ProxyGenerator();
            var ms = generator.CreateClassProxy<MessageSimulateRegistry>();
            return ms.GetInstance<T>();
        }

        public MessageSimulateRegistry()
        {
            //this._container = container;
            this._container = new WindsorContainer();
            _container.Install(Configuration.FromXmlFile("Configuration\\services.xml"));

            //this._container = new WindsorContainer();
            //this._container.Register(Component.For<IServiceRepository>().ImplementedBy<ServiceRepository>());
            //this._container.Register(Component.For<IMessageSimulator>().ImplementedBy<MessageSimulator>());

            //this._container.Register(Component.For<ServiceContext>());
            //this._container.Register(Component.For<MessageSimulatorIntercepter>());

            //match interceptor with concrete classes
            this._container.Kernel.ProxyFactory.AddInterceptorSelector(new ServiceManagerPointcut());
        }

        public T GetInstance<T>()
        {
            return this._container.Resolve<T>();
        }
    }

}
