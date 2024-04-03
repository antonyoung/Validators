using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Mappers
{
    public class IbanRequestMapperTests
    {
        private readonly IIbanRequestMapper _mapper;

        public IbanRequestMapperTests()
            => _mapper = new IbanRequestMapper();

        [Theory]
        [InlineData(Formatters.WhiteSpaces, ".", "AT37 3219 5334 4715 7523")]
        [InlineData(Formatters.WhiteSpaces, ".", "BE83 5483 1874 9715")]
        [InlineData(Formatters.WhiteSpaces, "", "BG77 STSA 9300 1398 8332 79")]
        public async Task MapAsPostalcodeRequestAsync(
            Formatters formatter,
            string replace,
            string value)
        {
            ValidatorModel model = new()
            {
                Formatter = formatter,
                Replace = replace,
                Value = value
            };

            var result = await _mapper
                .MapAsync(model);

            result
                .Should()
                .BeOfType<IbanValidationRequest>();

            result.Formatter
                .Should()
                .Be(formatter);

            result.Value
                .Should()
                .Be(value);

            result.Replace
                .Should()
                .Be(replace);
        }
    }
}