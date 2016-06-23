using System;
using System.Linq;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Proxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace OrderService
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Service Begin");

            var svc = SystemRegistry.Instance.CreateOrderService();

            svc.Proxy();

            var r = svc.Client(20);

            Console.WriteLine("Override the return value");
            Console.WriteLine(r);

            Console.ReadLine();
        }
    }

    public class SystemRegistry
    {
        private readonly IWindsorContainer container;

        public static SystemRegistry Instance
        {
            get
            {
                var generator = new ProxyGenerator();
                return generator.CreateClassProxy<SystemRegistry>();
            }
        } 

        public SystemRegistry()
        {
            this.container = new WindsorContainer();
            this.container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>());
            this.container.Register(Component.For<ILogService>().ImplementedBy<LogService>());

            this.container.Register(Component.For<LogInterceptor>());


            //match interceptor with concrete classes
            this.container.Kernel.ProxyFactory.AddInterceptorSelector(new ServiceManagerPointcut());
        }

        public IOrderService CreateOrderService()
        {
            return this.container.Resolve<IOrderService>();
        }

        public ILogService CreateLogService()
        {
            return this.container.Resolve<ILogService>();
        }
    }

    public class ServiceManagerPointcut : IModelInterceptorsSelector
    {
        public bool HasInterceptors(Castle.Core.ComponentModel model)
        {
            return model.Implementation.IsDefined(typeof(LogAttribute), true);
        }

        public Castle.Core.InterceptorReference[] SelectInterceptors(Castle.Core.ComponentModel model,
            Castle.Core.InterceptorReference[] interceptors)
        {
            return new[] {
                InterceptorReference.ForType<LogInterceptor>()
            }.Concat(interceptors).ToArray();
        }
    }

    public class LogInterceptor : IInterceptor
    {
        private readonly ILogService _logService;
        public LogInterceptor(ILogService logService)
        {
            _logService = logService;
        }

        public void Intercept(IInvocation invocation)
        {
            var parameter = invocation.Arguments.Any() ? invocation.Arguments.FirstOrDefault() : "NA";
            var r = _logService.OnEntry(invocation.Method.Name + " #"+ parameter);
            invocation.Proceed();
            invocation.ReturnValue = r;
            _logService.OnExit(invocation.Method.Name + " #"+ parameter);
        }
    }

    public interface IOrderService
    {
        void Proxy();
        string Client(int i);
    }

    [Log]
    public class OrderService : IOrderService
    {
        public void Proxy()
        {
            Console.WriteLine("Order service proxy invoked..");
        }

        public string Client(int i)
        {
            Console.WriteLine("Order service client ({0}) invoked..", i);
            return string.Format("Order service client ({0}) invoked..", i);
        }
    }

    public interface ILogService
    {
        string OnEntry(string msg);
        void OnExit(string msg);
    }

    public class LogService : ILogService
    {
        public string OnEntry(string msg)
        {
            Console.WriteLine("Entry: Log the method - {0}", msg);
            return string.Concat("return value ", msg);
        }

        public void OnExit(string msg)
        {
            Console.WriteLine("Exit: Log the method - {0}", msg);
        }
    }

    public class LogAttribute : Attribute
    {
    }
}
