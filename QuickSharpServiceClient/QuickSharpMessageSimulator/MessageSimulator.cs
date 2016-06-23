using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickSharpMessageSimulator.Extensions;
using QuickSharpMessageSimulator.Models;
using QuickSharpMessageSimulator.Repositories;

namespace QuickSharpMessageSimulator
{
    public interface IMessageSimulator
    {
        /// <summary>
        /// Record the response
        /// </summary>
        /// <param name="data">Response</param>
        /// <param name="args">Key {Url}, Method, Request </param>
        bool Record(object data, string method, object[] args);

        /// <summary>
        /// Replay the recorded response
        /// </summary>
        /// <param name="args">Key {Key | Url}, Method, Request </param>
        /// <returns></returns>
        string Replay(string method, object[] arguments);
    }

    public class MessageSimulator : IMessageSimulator
    {
        private readonly IServiceRepository _serviceRepository;
        
        public MessageSimulator(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        #region Virtualization

        public bool Record(object data, string method, object[] args)
        {
            //if (arguments.Length == 0) return false;

            //var args = arguments.Select(x => Convert.ToString(x)).ToArray();

            var request = string.Empty;
            var key = string.Empty;
            if (args.Length > 0) { key = Convert.ToString(args[0]); }
            else if (args.Length > 1) { request = Convert.ToString(args[1]); }

            if (EnableVirtualization(key, method))
            {
                //Record
                _serviceRepository.Patch(new ServiceData
                {
                    Request = request,
                    Response = data.AsJson(),
                    Type = "default",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                });
            }
            else
            {
                _serviceRepository.Add(new ServiceUrl
                {
                    Url = key,
                    Method = method,
                    ContentType = "default",
                    EnableVirtualization = true,
                    ServiceDatas = new[] { new ServiceData { Request = request, Response = data.AsJson(), Type = "default", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow } }
                });
            }

            return true;
        }

        private bool EnableVirtualization(string url, string method)
        {
            try
            {
                var svc = _serviceRepository.Find(url, method);
                return svc.Any(x => x.EnableVirtualization);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string Replay(string method, object[] args)
        {
            //if (args.Length == 0) return default(string);

            //var args = arguments.Select(x => Convert.ToString(x)).ToArray();

            var request = string.Empty;
            var key = string.Empty;
            if (args.Length > 0) { key = Convert.ToString(args[0]); }
            else if (args.Length > 1) { request = Convert.ToString(args[1]); }

            try
            {
                var svc = _serviceRepository.Find(key, method);
                if (svc.Any(x => x.EnableVirtualization))
                {
                    if (svc.Any(x => x.ServiceDatas.Any(d => d != null && !d.IsDeleted && !string.IsNullOrWhiteSpace(d.Response))))
                    {
                        //already recorded
                        var data = svc.Select(x => x.ServiceDatas.First(y => !y.IsDeleted && !string.IsNullOrWhiteSpace(y.Response) 
                            && y.Request.Equals(request, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
                        return data != null ? data.Response : default(string);
                    }
                }
                return default(string);
            }
            catch (Exception)
            {
                return default(string);
            }
        }

        #endregion
    }
}
