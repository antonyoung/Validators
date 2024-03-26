using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Validators;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Domain.Tests.Validators
{
    public class IbanRequestValidatorTests
    {
        private readonly IIbanRequestValidator _validator;

        public IbanRequestValidatorTests()
            => _validator = new IbanRequestValidator();

        [Fact]
        public async Task NoValidationErrors()
        {
            var request = new IbanValidationRequest
            {
                Value = "value",
                Formatter = Formatters.None
            };

            var result = await _validator
                .ValidateAsync(request);

            result
                .Should()
                .BeEmpty();
        }

        [Theory]
        [InlineData("")]
        public async Task ValidationErrorNoValue(string value)
        {
            var request = new IbanValidationRequest
            {
                Value = value,
                Formatter = Formatters.None
            };

            var result = await _validator
                .ValidateAsync(request);

            result
                .Count()
                .Should()
                .Be(1);

            result
                .ElementAt(0)
                .Should()
                .Be("Value is obliged, to be able to do a iban validation.");
        }
    }
}
