using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Mappers
{
    public class PostalcodeRequestMapperTests
    {
        private readonly IPostalcodeRequestMapper _mapper;

        public PostalcodeRequestMapperTests()
            => _mapper = new PostalcodeRequestMapper();

        [Theory]
        [InlineData("MLT", Formatters.WhiteSpaces, ".", "EDG 1062")]
        [InlineData("NL", Formatters.WhiteSpaces, ".", "1062 GD")]
        [InlineData("HR", Formatters.Hyphens, "", "HR-43210")]
        public async Task MapAsPostalcodeRequestAsync(
            string country, 
            Formatters formatter, 
            string replace, 
            string value)
        {
            ValidatorModel model = new()
            {
                Country = country,
                Formatter = formatter,
                Replace = replace,
                Value = value
            };

            var result = await _mapper
                .MapAsync(model);

            result
                .Should()
                .BeOfType<PostalcodeValidaionRequest>();

            result.Country
                .Should()
                .Be(country);

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