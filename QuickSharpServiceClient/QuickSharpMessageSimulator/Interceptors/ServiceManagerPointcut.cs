using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.MicroKernel.Proxy;
using QuickSharpMessageSimulator.Intercepters;

namespace QuickSharpMessageSimulator.Interceptors
{
    public class ServiceManagerPointcut : IModelInterceptorsSelector
    {
        public bool HasInterceptors(Castle.Core.ComponentModel model)
        {
            return model.Implementation.IsDefined(typeof(EnableMessageSimulationAttribute), true);
        }

        public Castle.Core.InterceptorReference[] SelectInterceptors(Castle.Core.ComponentModel model,
            Castle.Core.InterceptorReference[] interceptors)
        {
            return new[] {
                InterceptorReference.ForType<MessageSimulatorIntercepter>()
            }.Concat(interceptors).ToArray();
        }
    }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class EnableMessageSimulationAttribute : Attribute
    {
    }
}
