using CurrencyConverter.Service.Connector.CurrencyConverterService;
using System.ServiceModel;

namespace CurrencyConverter.Service.Connector
{
    public class CurrencyServiceConnector : ICurrencyServiceConnector
    {
        public string GetCurrencyWordValue(decimal value)
        {
            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8090/CurrencyConverterService");

            CurrencyConverterServiceClient service = new CurrencyConverterServiceClient(binding, address);
            return service.GetCurrencyWordValue(value);           
        }
    }
}
