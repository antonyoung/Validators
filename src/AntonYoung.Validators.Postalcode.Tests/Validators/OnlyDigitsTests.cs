using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode;
using AntonYoung.Validators.Postalcode.Infrastructure;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Tests.Postcode
{
    /// <summary>
    ///     used as test class of postalcodes with has only didgets.
    ///     these postalcodes could have whitespace or an hyphen as format.
    /// </summary>
    public class OnlyDigitsTests
    {
        private readonly IPostalcodeValidator _postalcodeValidator = new PostalcodeValidator();

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
        public void Valid(Countries country, string postalCode)
        {
            //=> validate postalcodes as expected formatted result.
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> success
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode with whitespace or not.
            result.Should().Be(postalCode);
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
        public void NoTrim(Countries country, string postalCode)
        {
            //=> validate postalcodes without trim
            bool isValid = _postalcodeValidator.TryValidate($" {postalCode} ", country, out string result);

            //=> success
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode with trim and whitespace or not.
            result.Should().Be(postalCode);
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
        public void InValid(Countries country, string postalCode)
        { 
            //=> validate postalcodes with alpha, only digits allowed. 
            postalCode = postalCode.Replace("2", "A");
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            _postalcodeValidator.IsValid.Should().Be(isValid);
            isValid.Should().BeFalse();

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result postalcode with alpha
            result.Should().Be(postalCode);
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
        public void LeadingZero(Countries country, string postalCode)
        {
            //=> validate postalcodes with leading zero
            postalCode = postalCode.Replace(postalCode.Substring(0, 1), "0");
            bool isValid = _postalcodeValidator.TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            isValid.Should().BeFalse();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has error message
            _postalcodeValidator.ErrorMessage.Should().NotBeNullOrEmpty();

            //=> unformatted result postalcode with leading zero.
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Czechia, WHITESPACE)]
        [InlineData(Countries.Greece, WHITESPACE)]
        [InlineData(Countries.Slovakia, WHITESPACE)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            //=> validate postalcodes without whitespace
            bool isValid = _postalcodeValidator.TryValidate(postalCode.Replace(" ", string.Empty), country, out string result);

            //=> succes
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode with whitespace.
            result.Should().Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Poland, POLAND)]
        [InlineData(Countries.Portugal, PORTUGAL)]
        public void WithOutHyphen(Countries country, string postalCode)
        {
            //=> validate postalcodes without hyphen.
            bool isValid = _postalcodeValidator.TryValidate(postalCode.Replace("-", string.Empty), country, out string result);

            //=> success
            isValid.Should().BeTrue();
            _postalcodeValidator.IsValid.Should().Be(isValid);
 
            //=> has no error message
            _postalcodeValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result postalcode with hyphen.   
            result.Should().Be(postalCode);
        }
    }
}