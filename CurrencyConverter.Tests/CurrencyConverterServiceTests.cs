using CurrencyConverter.Service;
using Xunit;

namespace CurrencyConverter.Tests
{
    public class CurrencyConverterServiceTests
    {
        [Theory]

        [InlineData(999999999, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars")]
        [InlineData(999999999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [InlineData(1000000, "one million dollars")]
        [InlineData(1000001, "one million one dollar")]
        [InlineData(1000010, "one million ten dollars")]
        [InlineData(1000100, "one million one hundred dollars")]
        [InlineData(1001000, "one million one thousand dollars")]
        [InlineData(1010000, "one million ten thousand dollars")]
        [InlineData(1100001, "one million one hundred thousand one dollar")]
        [InlineData(1111111, "one million one hundred eleven thousand one hundred eleven dollar")]
        [InlineData(1000, "one thousand dollars")]
        [InlineData(999, "nine hundred ninety-nine dollars")]
        [InlineData(213, "two hundred thirteen dollars")]
        [InlineData(10, "ten dollars")]
        [InlineData(3, "three dollars")]
        [InlineData(235, "two hundred thirty-five dollars")]
        [InlineData(230, "two hundred thirty dollars")]
        [InlineData(2235, "two thousand two hundred thirty-five dollars")]
        [InlineData(230.10d, "two hundred thirty dollars and ten cents")]
        [InlineData(199, "one hundred ninety-nine dollars")]
        [InlineData(0, "zero dollars")]
        [InlineData(1.02, "one dollar and two cents")]
        [InlineData(1.01, "one dollar and one cent")]
        [InlineData(1.1, "one dollar and ten cents")]
        [InlineData(1.2, "one dollar and twenty cents")]
        [InlineData(1.021, "one dollar and two cents")]
        [InlineData(1.029, "one dollar and two cents")]
        [InlineData(45100, "forty-five thousand one hundred dollars")]
        [InlineData(0.01, "zero dollars and one cent")]
        [InlineData(0.1, "zero dollars and ten cents")]
        public void ConvertCurrencyToWordsTests(double value, string expected)
        {
            CurrencyService ccs = new CurrencyService();
            string result = ccs.GetCurrencyWordValue((decimal)value);
            Assert.Equal(expected, result);
        }
    }
}
