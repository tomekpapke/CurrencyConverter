using CurrencyConverter.Common.Literature;
using System;
using System.Linq;
using System.Text;

namespace CurrencyConverter.Service
{
    public class CurrencyWordValueBuilder
    {
        private static readonly string[] ones =
    {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
    };

        private static readonly string[] tens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen",
            "twenty",
            "thirty",
            "fourty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        private static readonly string[] hundreds =
        {
            "one hundred",
            "two hundred",
            "three hundred",
            "four hundred",
            "five hundred",
            "six hundred",
            "seven hundred",
            "eight hundred",
            "nine hundred"
    };

        private static readonly string[][] ranges = new string[][]
        {
            hundreds,
            tens,
            ones
        };

        private StringBuilder sb;
        private decimal currency;

        public CurrencyWordValueBuilder(decimal currency)
        {
            this.currency = Math.Truncate(currency * 100) / 100; 
        }

        public CurrencyWordValueBuilder BeginComposingText()
        {
            sb = new StringBuilder();
            return this;
        }

        public string BuildWord(string number, int index)
        {
            StringBuilder sb = new StringBuilder();

            string word = string.Empty;

            number = number.PadLeft(3, '0');

            for (int i = 0; i < number.Length; i++)
            {
                if (i == 1)
                {
                    if (number[i].ToString() == "1")
                    {
                        word += ranges[i][int.Parse(number[i + 1].ToString())];
                        break;
                    }

                    if (number[i].ToString() != "0")
                    {
                        word += ranges[i][int.Parse(number[i].ToString()) - 1 + 9];
                        
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
                        word += ranges[i][int.Parse(number[i].ToString()) - 1];
                        word += " ";
                    }
                }
            }

            sb.Append(word.TrimEnd());

            if (index == 1 && word != string.Empty)
            {
                sb.Append(" ");
                sb.Append("thousand");
                sb.Append(" ");
            }
            else if (index == 2 && word != string.Empty)
            {
                sb.Append(" ");
                sb.Append("million");
                sb.Append(" ");
            }
            else if (index == 3 && word != string.Empty)
            {
                sb.Append(" ");
                sb.Append("billion");
                sb.Append(" ");
            }
            else if(sb.Length != 0)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public CurrencyWordValueBuilder ConvertInteger()
        {
            if (currency == 0) 
            {
                sb.Append("zero").Append(" ");
                return this;
            }

            string[] groups = StringComposer.SplitNumber((int)currency).Reverse().ToArray();

            for (int i = 0; i < groups.Length; i++)
            {
                string s = BuildWord(groups[i], i);
                sb.Insert(0, s);
            }
            return this;
        }

        public CurrencyWordValueBuilder AddDollarDeclination()
        {
            sb.Append(Math.Floor(currency) % 10 == 1 ? "dollar" : "dollars");
            return this;
        }

        public CurrencyWordValueBuilder AddCentDeclination()
        {
            sb.Append((int)(((decimal)currency % 1) * 100) % 10 == 1 ? "cent" : "cents");
            return this;
        }

        public CurrencyWordValueBuilder AddConjunction()
        {
            sb.Append(" and ");
            return this;
        }

        private bool HasCents()
        {
            return currency.ToString().IndexOf(',') > 0 && 
                currency.ToString().Substring(currency.ToString().LastIndexOf(',') + 1) != "00";
        }

        public CurrencyWordValueBuilder ComposeDecimal()
        {
            sb.Append(BuildWord(((int)(((decimal)currency % 1) * 100))
                .ToString(), -1));
            return this;
        }

        public CurrencyWordValueBuilder ConvertDecimal()
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
