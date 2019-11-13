using System.ServiceModel;

namespace CurrencyConverter.Service
{
    [ServiceContract]
    public interface ICurrencyService
    {
        [OperationContract]
        string GetCurrencyWordValue(decimal currency);
    }
}
