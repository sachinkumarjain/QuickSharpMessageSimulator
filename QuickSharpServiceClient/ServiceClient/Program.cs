
using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using Dell.Service.API.Client.Api;
using Dell.Service.API.Client.IoC;
using Newtonsoft.Json.Serialization;
using QuickSharpMessageSimulator;
using QuickSharpMessageSimulator.IoC;
using QuickSharpMessageSimulator.Repositories;

namespace Dell.Service.API.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
           //var svc = ObjectFactory.Container.GetInstance<ISimulateMessageRegistry>();
           //var resp2 = svc.GetInstance<ISerivceClientDemo>().GetSvc("aa", "bb");

            var url = "http://g1vmdcpqod01.olqa.preol.dell.com:1006/v1/quotes?number=3000000458226&version=1";

            //var ioc = ObjectFactory.Container.GetInstance<ICastleRegistry>();
            //var apiClient = ioc.GetInstance<IApiClient>();

            //var apiClient = ObjectFactory.Container.GetInstance<IApiClient>();
            //var apiClient = ObjectFactory.Container.GetInstance<ICastleRegistry>().GetInstance<IApiClient>();
            //apiClient = apiClient.Create();

            using (var client = ObjectFactory.Container.GetInstance<IApiContainer>().Create())
            {
                var resp = client.GetAsync<object>(new Uri(url)).Result;
            }
            return;
            //var demo = QuickSharpMessageSimulator.IoC.ObjectFactory.Container.GetInstance<ISerivceClientDemo>();
            //var svc = QuickSharpMessageSimulator.IoC.ObjectFactory.Container.GetInstance<IMessageSimulator>();
            //svc.EnabledToAll = true;
            //svc.DisabledToAll = false;
            //var resp2 = demo.GetSvc("aa", "bb");
            //var config = new HttpSelfHostConfiguration("http://localhost:12595");

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //    );

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));
            ////config.Services.Replace(typeof(IExceptionLogger), ObjectFactory.Container.GetInstance<WebApiUnhandledExceptionLogger>());
            //DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));

            //using (var server = new HttpSelfHostServer(config))
            //{
            //    server.OpenAsync().Wait();
            //    Console.WriteLine("Self Hosting begin ..");
            //    Console.ReadLine();
            //}

            //return;

            //var url = "http://g1vmdcpqod01.olqa.preol.dell.com:1004/v1/quotes?number=3000000001990&version=1"; // "http://g1vmdcpqod01.olqa.preol.dell.com:1006/v1/quotes?number=3000000458226&version=1";

            //var apiClient = Dell.Service.API.Client.IoC.ObjectFactory.Container.GetInstance<IApiClient>();
            //apiClient.GetSync<object>(new Uri(url));
            //var serviceRepository = ObjectFactory.Container.GetInstance<IServiceRepository>();

            //var s2 = serviceRepository.Find(2);

            //var services = serviceRepository.Find(url, "GET");

            //foreach (var svc in services)
            //{
            //    if (svc.EnableVirtualization && !svc.ServiceDatas.Any())
            //    {
                    //object resp;
                    //using (var client = new ApiContainer(apiClient).Create())
                    //{
                    //    resp = client.GetSync<object>(new Uri(url));
                    //}

                    //var su = rep.Find(1);

                    //var s1 = new ServiceUrl
                    //{
                    //    Method = "GET",
                    //    ContentType = "json",
                    //    Url = "http://g1vmdcpqod01.olqa.preol.dell.com:1006/v1/quotes?number=3000000458226&version=1",
                    //    EnableVirtualization = true,

                    //};
                    //s1.ServiceDatas.Add(new ServiceData { Request = "*", Response = resp.AsJson(), Type = "default", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });

                    //serviceRepository.Add(s1);
                //}
           // }
        }
    }


}
