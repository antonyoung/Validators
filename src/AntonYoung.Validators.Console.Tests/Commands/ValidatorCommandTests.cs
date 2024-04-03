using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Commands
{
    public class ValidatorCommandTests
    {
        private readonly IFixture _fixture;

        public ValidatorCommandTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _fixture
                .Freeze<Mock<IConsoleWriter>>();
        }

        [Theory]
        [InlineData("-h")]
        [InlineData("-H")]
        [InlineData("--help")]
        [InlineData("--Help")]
        [InlineData("--HELP")]
        public async Task ProcessAsHelpArgumentAsync(string value)
        {
            var result = await _fixture
                .Create<ValidatorCommand>()
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Help);
        }


        [Theory]
        [InlineData("-v")]
        [InlineData("-V")]
        [InlineData("--version")]
        [InlineData("--Version")]
        [InlineData("--VERSION")]
        public async Task ProcessAsVersionArgumentAsync(string value)
        {
            var result = await _fixture
                .Create<ValidatorCommand>()
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Version);
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

            var result = await _fixture
                .Create<ValidatorCommand>()
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
            Func<Task> action = async () => await _fixture
                .Create<ValidatorCommand>()
                .ProcessAsync(value, new());

            var exception = await action
                .Should()
                .ThrowAsync<ArgumentException>();

            exception
                .WithMessage($"Unknown '{value}' as SDK command.");
        }
    }
}