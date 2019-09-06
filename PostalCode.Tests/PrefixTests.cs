using Xunit;
using Validators;
using Validators.Interfaces;

namespace PostalCode.Tests
{
    public class PrefixTests
    {

        private PostcodeValidator _postcodeValidator = new PostcodeValidator();

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
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
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
            bool isValid = _postcodeValidator.TryParse(postalCode.ToLowerInvariant(), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
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
            bool isValid = _postcodeValidator.TryParse(postalCode.Remove(0, postalCode.IndexOf("-") + 1), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Finland, DIGITS_5)]
        public void WithOutPrefix_Default_Finland(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal($"{PREFIX_FINLAND_1}{postalCode}", result);
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
            postalCode = postalCode.Replace("4", "0");
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
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
            bool isValid = _postcodeValidator.TryParse($" {postalCode} ", country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.Replace(" ", string.Empty), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }
    }
}
