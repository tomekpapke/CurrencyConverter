using CurrencyConverter.Service.Connector.CurrencyService;
using System.ServiceModel;

namespace CurrencyConverter.Service.Connector
{
    public class CurrencyServiceConnector : BaseProxyService<ICurrencyService>, ICurrencyServiceConnector
    {
        public CurrencyServiceConnector(string serviceEndpoint) : base(serviceEndpoint)
        {
        }

        public string GetCurrencyWordValue(decimal value)
        {
            return Channel.GetCurrencyWordValue(value);
        }
    }
}
