using Xunit;
using Validators;

namespace Iban.Tests
{
    public class UnitTest1
    {
        private readonly IbanValidator _ibanValidator = new IbanValidator();

        [Theory]
        [InlineData("NL71INGB1320949010")]
        [InlineData("NL22ABNA6034837898")]
        public void NoWhiteSpaces(string value)
        {
            // => validate iban values without whitespaces.
            var isValid = _ibanValidator.Validate(value, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => formatted result as iban value with whitespaces
            Assert.Equal(value, result);
        }
        

        [Theory]
        [InlineData("NL71 INGB 1320 9490 10")]
        [InlineData("NL22 ABNA 6034 8378 98")]
        public void WithWhiteSpaces(string value)
        {
            // => validate iban values with whitespaces.
            var isValid = _ibanValidator.Validate(value, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => formatted result as iban value with whitespaces.
            Assert.Equal(value, result);
        }
    }
}
