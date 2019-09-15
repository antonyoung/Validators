using Xunit;
using Validators;

namespace Iban.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("NL71INGB1320949010")]
        [InlineData("NL22ABNA6034837898")]
        public void NoWhiteSpaces(string value)
        {
            var test = new IbanValidator();
            var isValid = test.Validate(value.Replace(" ", string.Empty), out _);
            Assert.True(isValid);
        }


        [Theory]
        [InlineData("NL71 INGB 1320 9490 10")]
        [InlineData("NL22 ABNA 6034 8378 98")]
        public void WithWhiteSpaces(string value)
        {
            var test = new IbanValidator();
            var isValid = test.Validate(value, out _);
            Assert.True(isValid);
        }
    }
}
