using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickSharpMessageSimulator.Interceptors;
using QuickSharpMessageSimulator.IoC;
using QuickSharpMessageSimulator.Repositories;

namespace QuickSharpMessageSimulator
{
    public interface ISerivceClientDemo
    {
        string GetSvc(string url, string request);
    }

    [EnableMessageSimulation]
    public class SerivceClientDemo : ISerivceClientDemo
    {
        //[EnableMessageSimulation]
        public string GetSvc(string url, string request)
        {
            
            return "Real Service called.";
        }
    }
}
