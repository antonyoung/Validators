using Xunit;
using Validators;
using Validators.Interfaces;

namespace Postcode.Tests
{
    public class PrefixTests
    {

        private readonly PostcodeValidator _postcodeValidator = new PostcodeValidator();

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
        public void WithPrefix(Countries country, string postcode)
        {
            //=> validate postcode with prefix.
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with prefix
            Assert.Equal(postcode, result);
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
        public void LowerCasePrefix(Countries country, string postcode)
        {
            // => validate postcode with lowercase prefix.
            bool isValid = _postcodeValidator.Validate(postcode.ToLowerInvariant(), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with prefix in uppercase.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutPrefix(Countries country, string postcode)
        {
            // => validate postcode without prefix
            bool isValid = _postcodeValidator.Validate(postcode.Remove(0, postcode.IndexOf("-") + 1), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with prefix
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Finland, DIGITS_5)]
        public void WithOutPrefix_Default_Finland(Countries country, string postcode)
        {
            // => validate postcode Finland without prefix.
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with default prefix FI-
            Assert.Equal($"{PREFIX_FINLAND_1}{postcode}", result);
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
        public void LeadingZero(Countries country, string postcode)
        { 
            // => validate postcode with leading zero
            postcode = postcode.Replace("4", "0");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => unformatted result postcode with leading zero.
            Assert.Equal(postcode, result);
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
        public void NoTrim(Countries country, string postcode)
        {
            // => validate postcode without trim
            bool isValid = _postcodeValidator.Validate($" {postcode} ", country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with trim
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutSpace(Countries country, string postcode)
        {
            //=> validate postcode sweden without whitespace.
            bool isValid = _postcodeValidator.Validate(postcode.Replace(" ", string.Empty), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode sweden with whitespace
            Assert.Equal(postcode, result);
        }
    }
}
