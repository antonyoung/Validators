using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Commands
{
    public class ReplaceCommandTests
    {
        private readonly ICommand _command;

        public ReplaceCommandTests()
            => _command = new ReplaceCommand();

        [Theory]
        [InlineData("")]
        [InlineData(".")]
        [InlineData(" ")]
        [InlineData("-")]
        public async Task ProcessAsArgumentArgumentAsync(string value)
        {
            ValidatorModel model = new();

            var result = await _command
                .ProcessAsync(value, model);

            result
                .Should()
                .Be(Arguments.Argument);

            model.Replace
                .Should()
                .Be(value);
        }
    }
}