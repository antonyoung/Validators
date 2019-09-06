using Validators;
using Validators.Interfaces;

using Xunit;


namespace Postcode.Tests
{
    public class AlphaNumericTests
    {

        private PostcodeValidator _postcodeValidator = new PostcodeValidator();

        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void Valid(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid,_postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "x2")]
        [InlineData(Countries.UnitedKingdom, "2344 AAA")]
        [InlineData(Countries.Ireland, "222 YD82")]
        public void InValid(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void WithOutSpace(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.Replace(" ", string.Empty), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }
    }
}
