using Validators.Enums;
using Validators.Interfaces;
using Validators.Tests.Attributes;
using Xunit;


namespace Validators.Tests.Postcode
{

    /// <summary>
    ///     used as test class of postcodes which has letters in their postcode.
    /// </summary>
    public class HasLettersTests
    {


        private readonly IPostcodeValidator _postcodeValidator = new PostcodeValidator();


        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";


        [Theory, Priority(0)]
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


        [Theory, Priority(10)]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postcode)
        {
            //=> validate postcode with whitespace
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result is with whitespace
            Assert.Equal(postcode, result);
        }


        [Theory, Priority(20)]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postcode)
        {
            // => validate postcodes with double whitespaces
            postcode = postcode.Replace(" ", "  ");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccesful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> unformatted result is postcode with double whitespaces
            Assert.Equal(postcode, result);
        }


        [Theory, Priority(30)]
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


        [Theory, Priority(40)]
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

            // => formatted result postcode with trim
            Assert.Equal(postcode, result);
        }


        [Theory, Priority(50)]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postcode)
        {
            //=> validate postalcode as lowercase
            bool isValid = _postcodeValidator.Validate(postcode.ToLowerInvariant(), country, out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> formatted result postcode in UPPERCASE
            Assert.Equal(postcode, result);
        }


        [Theory, Priority(60)]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postcode)
        {
            // => validate postalcode with leading zero
            postcode = postcode.Replace("1", "0");
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => unformatted result postcode with leading zero
            Assert.Equal(postcode, result);
        }
    }
}
