
using System;
using System.Linq;
using Castle.DynamicProxy;
using Dell.Service.API.Client.Api;
using Dell.Service.API.Client.IoC;

namespace Dell.Service.API.Client.Insepters
{
    public class MessageSimulatorIntercepter : IInterceptor
    {
        private readonly IMessageSimulator _messageSimulator;

        public MessageSimulatorIntercepter()
        {
            _messageSimulator = ObjectFactory.Container.GetInstance<IMessageSimulator>();
        }


        public void Intercept(IInvocation invocation)
        {
            var response = _messageSimulator.Replay(invocation.Arguments);

            if (response != null)
            {
                invocation.ReturnValue = response;
            }
            else
            {
                invocation.Proceed();
            }
            _messageSimulator.Record(invocation.ReturnValue, invocation.Arguments);
        }
    }
}
