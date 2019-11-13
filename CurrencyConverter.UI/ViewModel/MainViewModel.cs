using CurrencyConverter.Common.Messages;
using CurrencyConverter.UI.Async;
using CurrencyConverter.UI.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CurrencyConverter.UI.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly ICurrencyServiceProvider _provider;
        private string currencyWordValue;
        private bool canExecute;
        private string currencyNumericValue;
        public MainViewModel(ICurrencyServiceProvider provider)
        {
            _provider = provider;
            CanExecute = true;

            GetCurrencyWordValueCommand = AsyncCommand.Create(
                () =>
                {
                    return Task.Run(() =>
                    {
                        CanExecute = false;

                        try
                        {
                            CurrencyWordValue = provider.GetCurrencyWordValue(Convert.ToDecimal(CurrencyNumericValue));
                        }
                        catch (Exception ex)
                        {
                            AddErrors(ex, nameof(CurrencyNumericValue));
                        }
                        finally
                        {
                            CanExecute = true;
                        }
                    });
                });
        }

        #region Properties

        public IAsyncCommand GetCurrencyWordValueCommand { get; private set; }

        
        public string CurrencyWordValue 
        {
            get => currencyWordValue; 
            set 
            { 
                currencyWordValue = value;
                OnPropertyChanged(nameof(CurrencyWordValue));
            }
        }

        public bool IsValid
        {
            get
            {
                return (Errors[nameof(CurrencyNumericValue)].Count == 0);
            }
        }
 
        public bool CanExecute
        {
            get => canExecute;
            set
            {
                canExecute = value;
                OnPropertyChanged();
            }
        }

        public string CurrencyNumericValue
        {
            get => currencyNumericValue;
            set
            {
                currencyNumericValue = value;
                ValidateCurrencyValue();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValid));
            }
        }

        #endregion

        #region INotifyPropertyChanged

        protected Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Methods

        private void AddErrors(Exception error, string key)
        {
            if (!Errors.ContainsKey(key))
            {
                Errors.Add(key, new List<string>());
            }

            else
            {
                if (!Errors[key].Contains(error.Message))
                {
                    Errors[key].Add(error.Message);

                }
            }

            OnPropertyErrorsChanged(key);
        }

        #endregion

        #region Validators
        private void ValidateCurrencyValue()
        {
            List<string> errors;
            if (Errors.TryGetValue(nameof(CurrencyNumericValue), out errors) == false)
            {
                errors = new List<string>();
            }
            else
            {
                errors.Clear();
            }

            decimal value;

            if (!decimal.TryParse(CurrencyNumericValue.Replace(".", ","), out value) && !string.IsNullOrEmpty(CurrencyNumericValue))
            {
                errors.Add(Validation.ILLEGAL_CHARACTER_MESSAGE);
            }

            if (value > 999999999m)
            {
                errors.Add(Validation.OUT_OF_RANGE_MESSAGE);
            }

            if (string.IsNullOrEmpty(CurrencyNumericValue))
            {
                errors.Add(Validation.EMPTY_MESSAGE);
            }

            Errors[nameof(CurrencyNumericValue)] = errors;

            if (errors.Count > 0)
            {
                OnPropertyErrorsChanged(nameof(CurrencyNumericValue));
            }
        }

        #endregion

        #region INotifyDataErrorInfo

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnPropertyErrorsChanged(string p)
        {
            if (ErrorsChanged != null)
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(p));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();
            if (propertyName != null)
            {
                Errors.TryGetValue(propertyName, out errors);
                return errors;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}