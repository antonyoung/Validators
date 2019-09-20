using Validators.Interfaces;
using Xunit;


namespace Validators.Tests.Postcode
{

    /// <summary>
    ///     used as test class of postcodes which are alpha numeric.
    /// </summary>
    public class AlphaNumericTests
    {

        private readonly IPostcodeValidator _postcodeValidator = new PostcodeValidator();


        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void Valid(Countries country, string postcode)
        {
            // => validate valid postcodes as expected format.
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            //=> success
            Assert.True(isValid);
            Assert.Equal(isValid,_postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> formatted result, as given postcode
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "x2")]
        [InlineData(Countries.UnitedKingdom, "2344 AAA")]
        [InlineData(Countries.Ireland, "222 YD82")]
        public void InValid(Countries country, string postcode)
        {
            // => validate invalid postcodes
            bool isValid = _postcodeValidator.Validate(postcode, country, out string result);

            // => unsuccessful
            Assert.False(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            //=> has error message
            Assert.False(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            //=> unformatted result as postcode
            Assert.Equal(postcode, result);
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void WithOutSpace(Countries country, string postcode)
        {
            // => validate postcode without whitespace
            bool isValid = _postcodeValidator.Validate(postcode.Replace(" ", string.Empty), country, out string result);
            
            //=> success
            Assert.True(isValid);
            Assert.Equal(isValid, _postcodeValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_postcodeValidator.ErrorMessage));

            // => formatted result has postcode with whitespace
            Assert.Equal(postcode, result);
        }
    }
}
