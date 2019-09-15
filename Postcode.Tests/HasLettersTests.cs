using Validators;
using Validators.Interfaces;

using Xunit;


namespace Postcode.Tests
{
    public class HasLettersTests
    {

        private PostcodeValidator _postcodeValidator = new PostcodeValidator();

        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithOutSpace(Countries country, string postcode)
        {
            // => validate postcode without space.
            bool isValid = _postcodeValidator.Validate(postcode.Replace(" ", string.Empty), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> formatted result is with space.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postcode)
        {
            //=> validate postcode with space
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result is with space.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postcode)
        {
            // => set postcode with double space.
            postcode = postcode.Replace(" ", "  ");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccesful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> unformatted result is postcode with double space.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithHyphen(Countries country, string postcode)
        {
            // => replace postcode whitespace with hyphen
            postcode = postcode.Replace(" ", "-");

            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => unformatted result as given postcode
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void NoTrim(Countries country, string postcode)
        {
            //=> validate postcode without trim
            bool isValid = _postcodeValidator.Validate($" {postcode} ", country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // formatted result postcode with trim
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postcode)
        {
            //=> validate postalcode as lowercase.
            bool isValid = _postcodeValidator.Validate(postcode.ToLowerInvariant(), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> formatted result postcode in uppercase.
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postcode)
        {
            // => validate postalcode with leading zero.
            postcode = postcode.Replace("1", "0");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => unformatted result postcode as given.
            Assert.Equal(postcode, result);
        }
    }
}
