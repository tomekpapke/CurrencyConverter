using CommonServiceLocator;
using CurrencyConverter.Service.Connector;
using CurrencyConverter.UI.Providers;
using GalaSoft.MvvmLight.Ioc;

namespace CurrencyConverter.UI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ICurrencyServiceProvider, CurrencyServiceProvider>();
            SimpleIoc.Default.Register<ICurrencyServiceConnector, CurrencyServiceConnector>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}