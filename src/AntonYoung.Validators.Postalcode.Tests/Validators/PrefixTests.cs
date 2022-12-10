using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AntonYoung.Validators.Postalcode.Tests
{
    /// <summary>
    ///     used as test class of postalcodes with prefix
    /// </summary>
    public class PrefixTests 
        : IClassFixture<DependencyFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private const string PREFIX_CROATIA = "HR-";
        private const string PREFIX_LATIVIA = "LV-";
        private const string PREFIX_LITHUANIA = "LT-";
        private const string PREFIX_LUXEMBOURG = "L-";
        private const string PREFIX_SLOVENIA = "SI-";
        private const string PREFIX_FINLAND_1 = "FI-";
        private const string PREFIX_FINLAND_2 = "AX-";
        private const string PREFIX_SWEDEN = "SE-";

        private const string DIGITS_4 = "4231";
        private const string DIGITS_5 = "42310";

        private const string WHITESPACE = "434 78";

        public PrefixTests(DependencyFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;


        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithPrefix(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcodes with prefix
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode with prefix
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void LowerCasePrefix(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcodes with lowercase prefix.
            bool isValid = postalcodeValidator
                .TryValidate(postalCode.ToLowerInvariant(), country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode with prefix in UPPERCASE
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutPrefix(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcodes without prefix
            bool isValid = postalcodeValidator
                .TryValidate(postalCode.Remove(0, postalCode.IndexOf("-") + 1), country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode with prefix
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Finland, DIGITS_5)]
        public void WithOutPrefix_Default_Finland(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode Finland without prefix.
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode with default prefix FI-
            result
                .Should()
                .Be($"{PREFIX_FINLAND_1}{postalCode}");
        }

        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void LeadingZero(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcodes with leading zero
            postalCode = postalCode
                .Replace("4", "0");
            
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            isValid
                .Should()
                .BeFalse();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has error message
            postalcodeValidator.ErrorMessage
                .Should()
                .NotBeNullOrEmpty();

            //=> unformatted result postalcode with leading zero
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Croatia, PREFIX_CROATIA + DIGITS_5)]
        [InlineData(Countries.Latvia, PREFIX_LATIVIA + DIGITS_4)]
        [InlineData(Countries.Lithuania, PREFIX_LITHUANIA + DIGITS_5)]
        [InlineData(Countries.Luxembourg, PREFIX_LUXEMBOURG + DIGITS_4)]
        [InlineData(Countries.Slovenia, PREFIX_SLOVENIA + DIGITS_4)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_1 + DIGITS_5)]
        [InlineData(Countries.Finland, PREFIX_FINLAND_2 + DIGITS_5)]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void NoTrim(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode without trim
            bool isValid = postalcodeValidator
                .TryValidate($" {postalCode} ", country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode with trim
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Sweden, PREFIX_SWEDEN + WHITESPACE)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode Sweden without whitespace.
            bool isValid = postalcodeValidator
                .TryValidate(postalCode.Replace(" ", string.Empty), country, out string result);

            //=> success
            isValid
                .Should()
                .BeTrue();
            
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode Sweden with whitespace
            result
                .Should()
                .Be(postalCode);
        }
    }
}