using CurrencyConverter.Common.Constants;
using CurrencyConverter.Common.Literature;
using System;
using System.Linq;
using System.Text;

namespace CurrencyConverter.Service
{
    public class CurrencyWordValueComposer
    {
        private const string DOLLAR_SINGULAR = "dollar";
        private const string DOLLAR_PLURAL = "dollars";
        private const string CENT_SINGULAR = "cent";
        private const string CENT_PLURAL = "cents";
        private const string ZERO = "zero";
        private StringBuilder sb;
        private decimal currency;

        public CurrencyWordValueComposer(decimal currency)
        {
            this.currency = Math.Truncate(currency * 100) / 100;
        }

        public CurrencyWordValueComposer Begin()
        {
            sb = new StringBuilder();
            return this;
        }

        public string BuildExpression(string number, int index)
        {
            StringBuilder sb = new StringBuilder();

            string word = string.Empty;

            number = number.PadLeft(3, '0');

            int length = number.Length;

            for (int i = 0; i < length; i++)
            {
                if (i == 1)
                {
                    if (number[i].ToString() == "1")
                    {
                        word += Numbers.NUMERICAL_RANGES[i][int.Parse(number[i + 1].ToString())];
                        break;
                    }

                    if (number[i].ToString() != "0")
                    {
                        word += Numbers.NUMERICAL_RANGES[i][int.Parse(number[i].ToString()) - 1 + 9];

                        if (number[2].ToString() != "0")
                        {
                            word += "-";
                        }
                        else
                        {
                            word += " ";
                        }
                    }
                }
                else
                {
                    if (number[i].ToString() != "0")
                    {
                        word += Numbers.NUMERICAL_RANGES[i][int.Parse(number[i].ToString()) - 1];
                        word += " ";
                    }
                }
            }

            sb.Append(word.TrimEnd());

            if (index == 1 && word != string.Empty)
            {
                sb.Append(" ").Append(Numbers.THOUSAND).Append(" ");
            }
            else if (index == 2 && word != string.Empty)
            {
                sb.Append(" ").Append(Numbers.MILLION).Append(" ");
            }
            else if (index == 3 && word != string.Empty)
            {
                sb.Append(" ").Append(Numbers.BILLION).Append(" ");
            }
            else if (sb.Length != 0)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public CurrencyWordValueComposer ComposeIntegerExpression()
        {
            if ((int)currency == 0)
            {
                sb.Append(ZERO).Append(" ");
                return this;
            }

            string[] groups = StringComposer.SplitNumber((int)currency).Reverse().ToArray();

            for (int i = 0; i < groups.Length; i++)
            {
                string s = BuildExpression(groups[i], i);
                sb.Insert(0, s);
            }

            return this;
        }

        public CurrencyWordValueComposer AddDollarDeclination()
        {
            sb.Append(Math.Floor(currency) % 10 == 1 ? DOLLAR_SINGULAR : DOLLAR_PLURAL);
            return this;
        }

        public CurrencyWordValueComposer AddCentDeclination()
        {
            sb.Append((int)(((decimal)currency % 1) * 100) % 10 == 1 ? CENT_SINGULAR : CENT_PLURAL);
            return this;
        }

        public CurrencyWordValueComposer AddConjunction()
        {
            sb.Append(" and ");
            return this;
        }

        private bool HasCents()
        {
            return currency.ToString().IndexOf(',') > 0 &&
                currency.ToString().Substring(currency.ToString().LastIndexOf(',') + 1) != "00";
        }

        public CurrencyWordValueComposer ComposeDecimal()
        {
            sb.Append(BuildExpression(((int)(((decimal)currency % 1) * 100))
                .ToString(), -1));
            return this;
        }

        public CurrencyWordValueComposer ComposeDecimalExpression()
        {
            if (HasCents())
            {
                AddConjunction()
                .ComposeDecimal()
                .AddCentDeclination();
            }

            return this;
        }

        public string Output()
        {
            return sb.ToString();
        }
    }
}
