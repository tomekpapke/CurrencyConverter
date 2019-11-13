namespace CurrencyConverter.Common.Constants
{
    public static class Numbers
    {
        public const string THOUSAND = "thousand";
        public const string HUNDRED = "hundred";
        public const string MILLION = "million";
        public const string BILLION = "billion";

        public static readonly string[] SINGLE =
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

        public static readonly string[] TENTHS =
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
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        public static readonly string[] HUNDREDTHS =
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

        public static readonly string[][] NUMERICAL_RANGES = new string[][]
        {
            HUNDREDTHS,
            TENTHS,
            SINGLE
        };
    }
}
