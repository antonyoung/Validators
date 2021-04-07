using FluentAssertions;
using Validators.Postalcode;
using Validators.Abstractions.Enums;
using Validators.Postalcode.Infrastructure;
using Xunit;

namespace Validators.Tests.Postcode
{
    /// <summary>
    ///     used as test class of postalcodes which has letters in their postalcode.
    /// </summary>
    public class HasLettersTests
    {
        private readonly IPostalcodeValidator _postalcodeValidator = new PostalcodeValidator();

        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            //=> validate postalcode without space.
            bool isValid = _postalcodeValidator.TryValidate(postalCode.Replace(" ", string.Empty), country, out string result);

            //=> success
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result is with space.
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postalCode)
        {
            //=> validate postalcode with whitespace
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> success
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result is with whitespace
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postalCode)
        {
            //=> validate postalcodes with double whitespaces
            postalCode = postalCode.Replace(" ", "  ");
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccesful
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeFalse();

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result is postalcode with double whitespaces
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithHyphen(Countries country, string postalCode)
        {
            //=> replace postalcode whitespace with hyphen
            postalCode = postalCode.Replace(" ", "-");

            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeFalse();

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result as given postalcode
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void NoTrim(Countries country, string postalCode)
        {
            //=> validate postalcode without trim
            bool isValid = _postalcodeValidator.TryValidate($" {postalCode} ", country, out string result);

            //=> success
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeTrue();

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode with trim
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postalCode)
        {
            //=> validate postalcode as lowercase
            bool isValid = _postalcodeValidator.TryValidate(postalCode.ToLowerInvariant(), country, out string result);

            //=> success
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeTrue();

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode in UPPERCASE
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postalCode)
        {
            //=> validate postalcode with leading zero
            postalCode = postalCode.Replace("1", "0");
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeFalse();

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result postalcode with leading zero
            result.Should().Be(postalCode);
        }
    }
}