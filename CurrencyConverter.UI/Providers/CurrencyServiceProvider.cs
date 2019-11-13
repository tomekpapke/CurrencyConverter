using CurrencyConverter.Service.Connector;

namespace CurrencyConverter.UI.Providers
{
    public class CurrencyServiceProvider : ICurrencyServiceProvider
    {
        private readonly string SERVICE_ENDPOINT = "net.tcp://localhost:8090/CurrencyService";
        public string GetCurrencyWordValue(decimal value) 
        {
            using (var proxy = new CurrencyServiceConnector(SERVICE_ENDPOINT)) 
            {
                return proxy.GetCurrencyWordValue(value);
            }
        }
    }
}
