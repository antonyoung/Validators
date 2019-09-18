using Xunit;
using Validators;

namespace Iban.Tests
{
    public class UnitTest1
    {
        private readonly IbanValidator _ibanValidator = new IbanValidator();

        // todo: add sanityextension tests

        // => Bulgaria BG07TTBB94004773868743
        
        // todo: add test of all european countries
        // todoL add test of all european countries lowercase
        // todo: add failure tests of all countries
        // todo: add failure length of all countries
        // todo: add failure sanity check of all countries
        // todo: add tests branch, bank, account, country and check didget checks.


        [Theory]
        [InlineData("NL71 INGB 1320 9490 10")]
        [InlineData("NL22 ABNA 6034 8378 98")]
        [InlineData("AT37 3219 5334 4715 7523")]
        [InlineData("BE83 5483 1874 9715")]
        [InlineData("HR55 2484 0084 6518 3417 9")]
        [InlineData("CZ65 5051 8353 7399 3389 7417")]
        [InlineData("DK65 5051 8748 7637 52")]
        [InlineData("PL50 1090 2402 3828 6354 4736 6724")]
        [InlineData("PT50 0035 0651 2159 7249 4481 3")]
        [InlineData("SK28 7742 4513 1288 6162 8345")]
        [InlineData("SI56 8339 1723 4746 131")]
        [InlineData("ES44 0075 3585 8163 4877 4767")]
        [InlineData("SE44 8755 6616 3143 4143 2154")]
        [InlineData("GB20 BARC 2003 1816 5285 58")]
        public void NoWhiteSpaces(string value)
        {
            // => validate iban values without whitespaces.
            var isValid = _ibanValidator.Validate(value.Replace(" ", string.Empty), out string result);

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
        [InlineData("AT37 3219 5334 4715 7523")]
        [InlineData("BE83 5483 1874 9715")]
        [InlineData("HR55 2484 0084 6518 3417 9")]
        [InlineData("CZ65 5051 8353 7399 3389 7417")]
        [InlineData("DK65 5051 8748 7637 52")]
        [InlineData("PL50 1090 2402 3828 6354 4736 6724")]
        [InlineData("PT50 0035 0651 2159 7249 4481 3")]
        [InlineData("SK28 7742 4513 1288 6162 8345")]
        [InlineData("SI56 8339 1723 4746 131")]
        [InlineData("ES44 0075 3585 8163 4877 4767")]
        [InlineData("SE44 8755 6616 3143 4143 2154")]
        [InlineData("GB20 BARC 2003 1816 5285 58")]
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
