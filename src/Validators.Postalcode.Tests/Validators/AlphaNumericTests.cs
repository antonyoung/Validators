using FluentAssertions;
using Validators.Abstractions.Enums;
using Validators.Postalcode.Infrastructure;
using Xunit;

namespace Validators.Postalcode.Tests
{
    /// <summary>
    ///     used as test class of postalcodes which are alpha numeric.
    /// </summary>
    public class AlphaNumericTests
    {
        private readonly IPostalcodeValidator _postalcodeValidator = new PostalcodeValidator();

        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void IsValid(Countries country, string postalCode)
        {
            //=> validate valid postalcodes as expected format.
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> success
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeTrue();

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result, as given postalcode
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.UnitedKingdom, "x2")]
        [InlineData(Countries.UnitedKingdom, "2344 AAA")]
        [InlineData(Countries.Ireland, "222 YD82")]
        public void InValid(Countries country, string postalCode)
        {
            //=> validate invalid postalcodes
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeFalse();

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result as postalcode
            result.Should().Be(postalCode);
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
            //=> validate postalcode without whitespace
            bool isValid = _postalcodeValidator.TryValidate(postalCode.Replace(" ", string.Empty), country, out string result);

            //=> success
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeTrue();

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result has postalcode with whitespace
            result.Should().Be(postalCode);
        }
    }
}