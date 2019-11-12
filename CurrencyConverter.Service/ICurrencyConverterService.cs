using System.ServiceModel;

namespace CurrencyConverter.Service
{
    [ServiceContract]
    public interface ICurrencyConverterService
    {
        [OperationContract]
        string GetCurrencyWordValue(decimal currency);
    }
}
