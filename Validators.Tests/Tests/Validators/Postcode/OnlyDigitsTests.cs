using Validators.Interfaces;
using Xunit;


namespace Validators.Tests.Postcode
{

    /// <summary>
    ///     used as test class of postcodes with has only didgets.
    ///     these postcodes could have whitespace or an hyphen as format.
    /// </summary>
    public class OnlyDigitsTests
    {

        private readonly IPostcodeValidator _postcodeValidator = new PostcodeValidator();


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
        public void Valid(Countries country, string postcode)
        {
            // => validate postcodes as expected formatted result.
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with whitespace or not.
            Assert.Equal(postcode, result);
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
        public void NoTrim(Countries country, string postcode)
        {
            // => validate postcodes without trim
            bool isValid = _postcodeValidator.Validate($" {postcode} ", country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with trim and whitespace or not.
            Assert.Equal(postcode, result);
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
        public void InValid(Countries country, string postcode)
        { 
            // => validate postcodes with alpha, only digits allowed. 
            postcode = postcode.Replace("2", "A");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => unformatted result postcode with alpha
            Assert.Equal(postcode, result);
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
        public void LeadingZero(Countries country, string postcode)
        {
            // => validate postcodes with leading zero
            postcode = postcode.Replace(postcode.Substring(0, 1), "0");
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
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        public void WithOutSpace(Countries country, string postcode)
        {
            // => validate postcodes without whitespace
            bool isValid = _postcodeValidator.Validate(postcode.Replace(" ", string.Empty), country, out string result);

            // => succes
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with whitespace.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void WithOutHyphen(Countries country, string postalCode)
        {
            // => validate postcodes without hyphen.
            bool isValid = _postcodeValidator.Validate(postalCode.Replace("-", string.Empty), country, out string result);
            
            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result postcode with hyphen.   
            Assert.Equal(postalCode, result);
        }
    }
}
