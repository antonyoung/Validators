using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AntonYoung.Validators.Postalcode.Tests
{
    /// <summary>
    ///     used as test class of postalcodes which has letters in their postalcode.
    /// </summary>
    public class HasLettersTests 
        : IClassFixture<DependencyFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";

        public HasLettersTests(DependencyFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode without space.
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

            //=> formatted result is with space.
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode with whitespace
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

            //=> formatted result is with whitespace
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcodes with double whitespaces
            postalCode = postalCode.Replace(" ", "  ");
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> unsuccesful
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeFalse();

            //=> has error message
            postalcodeValidator.ErrorMessage
                .Should()
                .NotBeNullOrEmpty();

            //=> unformatted result is postalcode with double whitespaces
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithHyphen(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> replace postalcode whitespace with hyphen
            postalCode = postalCode.Replace(" ", "-");

            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeFalse();

            //=> has error message
            postalcodeValidator.ErrorMessage
                .Should()
                .NotBeNullOrEmpty();

            //=> unformatted result as given postalcode
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void NoTrim(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode without trim
            bool isValid = postalcodeValidator
                .TryValidate($" {postalCode} ", country, out string result);

            //=> success
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            isValid
                .Should()
                .BeTrue();

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
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode as lowercase
            bool isValid = postalcodeValidator
                .TryValidate(postalCode.ToLowerInvariant(), country, out string result);

            //=> success
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeTrue();

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result postalcode in UPPERCASE
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            postalCode = postalCode
                .Replace("1", "0");

            //=> validate postalcode with leading zero
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeFalse();

            //=> has error message
            postalcodeValidator.ErrorMessage
                .Should()
                .NotBeNullOrEmpty();

            //=> unformatted result postalcode with leading zero
            result
                .Should()
                .Be(postalCode);
        }
    }
}