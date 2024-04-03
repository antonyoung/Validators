using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Commands
{
    public class ApplicationCommandTests
    {
        private readonly ApplicationCommand _command;

        public ApplicationCommandTests() 
        { 
            _command = new ApplicationCommand();
        }

        [Theory]
        [InlineData("iban", Applications.Iban)]
        [InlineData("Iban", Applications.Iban)]
        [InlineData("IBAN", Applications.Iban)]
        [InlineData("post", Applications.Post)]
        [InlineData("Post", Applications.Post)]
        [InlineData("POST", Applications.Post)]
        public async Task ProcessAsArgumentValueAsync(string value, Applications application)
        {
            ValidatorModel model = new();

            var result = await _command
                .ProcessAsync(value, model);

            result
                .Should()
                .Be(Arguments.Value);

            model.Application
                .Should()
                .Be(application);
        }

        [Theory]
        [InlineData("")]
        [InlineData("bian")]
        [InlineData("opst")]
        [InlineData("posted")]
        [InlineData("ibanid")]
        public async Task ThrowsArgumentExceptionAsync(string value)
        {
            Func<Task> action = async () => await _command
                .ProcessAsync(value, new());

            var exception = await action
                .Should()
                .ThrowAsync<ArgumentException>();

            exception
                .WithMessage($"Unknown '{value}' as SDK command.");
        }
    }
}