
using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using QuickSharpMessageSimulator.Interceptors;
using QuickSharpMessageSimulator.IoC;

namespace QuickSharpMessageSimulator.Intercepters
{
    public class MessageSimulatorIntercepter : IInterceptor
    {
        private readonly IMessageSimulator _messageSimulator;

        public MessageSimulatorIntercepter(IMessageSimulator messageSimulator)
        {
            _messageSimulator = messageSimulator;
        }

        public void Intercept(IInvocation invocation)
        {
            var isEnabled = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(EnableMessageSimulationAttribute), true).Length > 0;

            if (!isEnabled)
            {
                invocation.Proceed();
                return;
            }
            
            var response = _messageSimulator.Replay(invocation.InvocationTarget + "." + invocation.Method.Name, invocation.Arguments);

            if (response != null)
            {
                if (invocation.Method.ReturnType.BaseType.Name.Contains(typeof(Task).Name))
                {
                    if (invocation.Method.ReturnType.IsGenericType)
                    {
                        var returnType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
                        invocation.ReturnValue = Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject(response, returnType));
                    }
                }
                else
                { invocation.ReturnValue = Newtonsoft.Json.JsonConvert.DeserializeObject(response, typeof(object)); }
                System.Diagnostics.Debug.Print(invocation.Method.Name + "[" + invocation.Arguments[0] + "] has been replayed and record response is " + invocation.ReturnValue);
            }
            else
            {
                invocation.Proceed();

                var taskResponse = invocation.ReturnValue as Task<dynamic>;
                object data;

                if (taskResponse != null)
                {
                    taskResponse.Wait();
                    data = taskResponse.Result;
                }
                else
                { data = invocation.ReturnValue; }

                //TODO:
                //Record the Return type as type name and IsAsync = T|F

                if (_messageSimulator.Record(data, invocation.InvocationTarget + "." + invocation.Method.Name, invocation.Arguments))
                { System.Diagnostics.Debug.Print(invocation.Method.Name + "[" + invocation.Arguments[0] + "] has been recorded and recorded response is " + data); }
            }
        }
    }
}
