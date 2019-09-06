using Xunit;
using Validators;
using Validators.Interfaces;

namespace PostalCode.Tests
{
    public class OnlyDigitsTests
    {

        private PostcodeValidator _postcodeValidator = new PostcodeValidator();

        private const string DIGITS_4 = "4321";
        private const string DIGITS_5 = "54321";
        private const string DIGITS_6 = "654321";
        private const string WHITESPACE = "123 45";
        private const string POLAND = "12-345";
        private const string PORTUGAL = "1234-567";


        [Theory]
        [InlineData(Countries.Austria, DIGITS_4)]
        [InlineData(Countries.Belgium, DIGITS_4)]
        [InlineData(Countries.Bulgaria, DIGITS_4)]
        [InlineData(Countries.Cyprus, DIGITS_4)]
        [InlineData(Countries.Denmark, DIGITS_4)]
        [InlineData(Countries.Estonia, DIGITS_5)]
        [InlineData(Countries.France, DIGITS_5)]
        [InlineData(Countries.Germany, DIGITS_5)]
        [InlineData(Countries.Hungary, DIGITS_4)]
        [InlineData(Countries.Italy, DIGITS_5)]
        [InlineData(Countries.Spain, DIGITS_5)]
        [InlineData(Countries.Romania, DIGITS_6)]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void Valid(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Austria, DIGITS_4)]
        [InlineData(Countries.Belgium, DIGITS_4)]
        [InlineData(Countries.Bulgaria, DIGITS_4)]
        [InlineData(Countries.Cyprus, DIGITS_4)]
        [InlineData(Countries.Denmark, DIGITS_4)]
        [InlineData(Countries.Estonia, DIGITS_5)]
        [InlineData(Countries.France, DIGITS_5)]
        [InlineData(Countries.Germany, DIGITS_5)]
        [InlineData(Countries.Hungary, DIGITS_4)]
        [InlineData(Countries.Italy, DIGITS_5)]
        [InlineData(Countries.Spain, DIGITS_5)]
        [InlineData(Countries.Romania, DIGITS_6)]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void NoTrim(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse($" {postalCode} ", country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Austria, DIGITS_4)]
        [InlineData(Countries.Belgium, DIGITS_4)]
        [InlineData(Countries.Bulgaria, DIGITS_4)]
        [InlineData(Countries.Cyprus, DIGITS_4)]
        [InlineData(Countries.Denmark, DIGITS_4)]
        [InlineData(Countries.Estonia, DIGITS_5)]
        [InlineData(Countries.France, DIGITS_5)]
        [InlineData(Countries.Germany, DIGITS_5)]
        [InlineData(Countries.Hungary, DIGITS_4)]
        [InlineData(Countries.Italy, DIGITS_5)]
        [InlineData(Countries.Spain, DIGITS_5)]
        [InlineData(Countries.Romania, DIGITS_6)]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void InValid(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace("2", "A");
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Austria, DIGITS_4)]
        [InlineData(Countries.Belgium, DIGITS_4)]
        [InlineData(Countries.Bulgaria, DIGITS_4)]
        [InlineData(Countries.Cyprus, DIGITS_4)]
        [InlineData(Countries.Denmark, DIGITS_4)]
        [InlineData(Countries.Estonia, DIGITS_5)]
        [InlineData(Countries.France, DIGITS_5)]
        [InlineData(Countries.Germany, DIGITS_5)]
        [InlineData(Countries.Hungary, DIGITS_4)]
        [InlineData(Countries.Italy, DIGITS_5)]
        [InlineData(Countries.Spain, DIGITS_5)]
        [InlineData(Countries.Romania, DIGITS_6)]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void LeadingZero(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace(postalCode.Substring(0, 1), "0");
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.Replace(" ", string.Empty), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void WithOutHyphen(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.Replace("-", string.Empty), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }
    }
}
