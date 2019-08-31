using Xunit;
using PostalCode.Library;

namespace PostalCode.Tests
{
    public class PrefixTests
    {

        private const string PREFIX_CROATIA = "HR-";
        private const string PREFIX_LATIVIA = "LV-";
        private const string PREFIX_LITHUANIA = "LT-";
        private const string PREFIX_LUXEMBOURG = "L-";
        private const string PREFIX_SLOVENIA = "SI-";
        private const string PREFIX_FINLAND_1 = "FI-";
        private const string PREFIX_FINLAND_2 = "AX-";
        private const string PREFIX_SWEDEN = "SE-";

        private const string DIGITS_4 = "4231";
        private const string DIGITS_5 = "42310";

        private const string WHITESPACE = "434 78";


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithPrefix(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode);

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void LowerCasePrefix(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.ToLowerInvariant());

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutPrefix(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.Remove(0, postalCode.IndexOf("-") + 1));

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Finland, DIGITS_5)]
        public void WithOutPrefix_Default_Finland(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode);

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal($"{PREFIX_FINLAND_1}{postalCode}", test.ToString());
        }


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void LeadingZero(Countries country, string postalCode)
        {
            var leadingZero = postalCode.Replace("4", "0");
            var test = new Library.PostalCode(country, leadingZero);

            Assert.False(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(leadingZero, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void NoTrim(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, $" {postalCode} ");

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.Replace(" ", string.Empty));

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }
    }
}
