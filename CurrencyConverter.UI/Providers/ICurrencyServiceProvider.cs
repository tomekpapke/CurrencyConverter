namespace CurrencyConverter.UI.Providers
{
    public interface ICurrencyServiceProvider
    {
        string GetCurrencyWordValue(decimal value);
    }
}