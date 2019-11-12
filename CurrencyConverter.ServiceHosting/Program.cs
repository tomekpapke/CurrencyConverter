using System;
using System.ServiceModel;

namespace CurrencyConverter.ServiceHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CurrencyConverter.Service.CurrencyConverterService))) 
            {
                host.Open();
                Console.WriteLine(String.Format("Host started at {0}.", DateTime.Now));
                Console.ReadLine();
            }
        }
    }
}

