using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Common.Literature
{
    public static class StringComposer
    {
        public static string[] SplitNumber(long value)
        {
            Stack<string> results = new Stack<string>();

            do
            {
                string current = (value % 1000).ToString().PadLeft(3, '0');

                results.Push(current);
                value /= 1000;
            } while (value > 0);

            return results.ToArray();
        }
    }
}
