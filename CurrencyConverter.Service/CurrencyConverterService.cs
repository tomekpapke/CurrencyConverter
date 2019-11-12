namespace CurrencyConverter.Service
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public string GetCurrencyWordValue(decimal currency)
        {
            return new CurrencyWordValueBuilder(currency)
                .BeginComposingText()
                .ConvertInteger()
                .AddDollarDeclination()
                .ConvertDecimal()
                .Output();
        }
    }
}
