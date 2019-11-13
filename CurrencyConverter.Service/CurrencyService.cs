namespace CurrencyConverter.Service
{
    public class CurrencyService : ICurrencyService
    {
        public string GetCurrencyWordValue(decimal currency)
        {
            return new CurrencyWordValueComposer(currency)
                .Begin()
                .ComposeIntegerExpression()
                .AddDollarDeclination()
                .ComposeDecimalExpression()
                .Output();
        }
    }
}
