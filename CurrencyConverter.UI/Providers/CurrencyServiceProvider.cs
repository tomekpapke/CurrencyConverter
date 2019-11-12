using CurrencyConverter.Service.Connector;

namespace CurrencyConverter.UI.Providers
{
    public class CurrencyServiceProvider : ICurrencyServiceProvider
    {
        private readonly ICurrencyServiceConnector connector;
        public CurrencyServiceProvider(ICurrencyServiceConnector connector) 
        {
            this.connector = connector;
        }
        public string GetCurrencyWordValue(decimal value)
        {
            return connector.GetCurrencyWordValue(value);
        }
    }
}
