using Xunit;
using Validators;

namespace Iban.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("NL52INGB0005174422")]
        [InlineData("NL52 INGB 0005 1744 22")]
        public void Test1(string value)
        {
            var test = new IbanValidator();
            var isValid = test.Validate(value, out _);
            Assert.True(isValid);
        }
    }
}
