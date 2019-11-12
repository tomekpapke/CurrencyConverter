namespace CurrencyConverter.Service.Connector
{
    public interface ICurrencyServiceConnector
    {
        string GetCurrencyWordValue(decimal value);
    }
}
