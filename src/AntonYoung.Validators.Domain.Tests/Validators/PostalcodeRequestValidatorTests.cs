using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Validators;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Domain.Tests.Validators
{
    public class PostalcodeRequestValidatorTests
    {
        private readonly IPostalcodeRequestValidator _validator;

        public PostalcodeRequestValidatorTests()
            => _validator = new PostalcodeRequestValidator();

        [Fact]
        public async Task NoValidationErrors()
        {
            var request = new PostalcodeValidaionRequest
            {
                Value = "postalcode value",
                Country = Countries.Latvia.ToString()
            };

            var result = await _validator
                .ValidateAsync(request, Countries.Netherlands);

            result
                .Should()
                .BeEmpty();
        }

        [Theory]
        [InlineData("")]
        public async Task ValidationErrorNoValue(string value)
        {
            var request = new PostalcodeValidaionRequest
            {
                Value = value
            };

            var result = await _validator
                .ValidateAsync(request, Countries.Netherlands);

            result
                .Count()
                .Should()
                .Be(1);

            result
                .ElementAt(0)
                .Should()
                .Be("Value is obliged, to be able to do a postalcode validation.");
        }

        [Fact]
        public async Task ValidationErrorAmsterdamAsCountry()
        {
            var request = new PostalcodeValidaionRequest
            {
                Value = "postalcode",
            };

            var result = await _validator
                .ValidateAsync(request, Countries.Amsterdam);

            result
                .Count()
                .Should()
                .Be(1);

            result
                .ElementAt(0)
                .Should()
                .Be("Amsterdam is not supported as country to be validated.");
        }

        [Theory]
        [InlineData("")]
        public async Task ValidationErrorsNoValueAndAmsterdamAsCountry(string value)
        {
            var request = new PostalcodeValidaionRequest
            {
                Value = value,
            };

            var result = await _validator
                .ValidateAsync(request, Countries.Amsterdam);

            result
                .Count()
                .Should()
                .Be(2);

            result
                .ElementAt(0)
                .Should()
                .Be("Value is obliged, to be able to do a postalcode validation.");

            result
                .ElementAt(1)
                .Should()
                .Be("Amsterdam is not supported as country to be validated.");
        }
    }
}
