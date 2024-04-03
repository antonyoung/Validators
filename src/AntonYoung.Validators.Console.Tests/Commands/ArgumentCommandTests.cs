using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Commands
{
    public class ArgumentCommandTests
    {
        private readonly ICommand _command;

        public ArgumentCommandTests()
            => _command = new ArgumentCommand();

        [Theory]
        [InlineData("-c")]
        [InlineData("-C")]
        [InlineData("--country")]
        [InlineData("--COUNTRY")]
        [InlineData("--Country")]
        public async Task ProcessAsCountryArgumentAsync(string value)
        {
            var result = await _command
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Country);
        }

        [Theory]
        [InlineData("-f")]
        [InlineData("-F")]
        [InlineData("--formatter")]
        [InlineData("--FORMATTER")]
        [InlineData("--Formatter")]
        public async Task ProcessAsFormatterArgumentAsync(string value)
        {
            var result = await _command
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Formatter);
        }

        [Theory]
        [InlineData("-r")]
        [InlineData("-R")]
        [InlineData("--replace")]
        [InlineData("--REPLACE")]
        [InlineData("--Replace")]
        public async Task ProcessAsReplaceArgumentAsync(string value)
        {
            var result = await _command
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Replace);
        }

        [Theory]
        [InlineData("-a")]
        [InlineData("-B")]
        [InlineData("--version")]
        [InlineData("--help")]
        [InlineData("--iban")]
        [InlineData("--post")]
        public async Task ProcessAsUnknownArgumentAsync(string value)
        {
            var result = await _command
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Unknown);
        }
    }
}