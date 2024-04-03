using AntonYoung.Validators.Abstractions.Enums;
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
    public class FormatterCommandTests
    {
        private readonly IFixture _fixture
            ;
        public FormatterCommandTests()
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
        [InlineData("--HELP")]
        public async Task ProcessAsHelpArgumentAsync(string value)
        {
            var result = await _fixture
                .Create<FormatterCommand>()
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Help);
        }

        [Theory]
        [InlineData("whitespaces", Formatters.WhiteSpaces)]
        [InlineData("WhiteSpaces", Formatters.WhiteSpaces)]
        [InlineData("WHITESPACES", Formatters.WhiteSpaces)]
        [InlineData("hyphensandwhitespaces", Formatters.HyphensAndWhiteSpaces)]
        [InlineData("HyphensAndWhiteSpaces", Formatters.HyphensAndWhiteSpaces)]
        [InlineData("HYPHENSANDWHITESPACES", Formatters.HyphensAndWhiteSpaces)]
        [InlineData("hyphens", Formatters.Hyphens)]
        [InlineData("Hyphens", Formatters.Hyphens)]
        [InlineData("HYPHENS", Formatters.Hyphens)]
        [InlineData("none", Formatters.None)]
        [InlineData("None", Formatters.None)]
        [InlineData("NONE", Formatters.None)]
        public async Task ProcessAsArgumentArgumentAsync(string value, Formatters formatter)
        {
            ValidatorModel model = new();

            var result = await _fixture
                .Create<FormatterCommand>()
                .ProcessAsync(value, model);

            result
                .Should()
                .Be(Arguments.Argument);

            model.Formatter
                .Should() 
                .Be(formatter);
        }

        [Theory]
        [InlineData("unknown")]
        [InlineData("formatter")]
        [InlineData("WhitePlaces")]
        [InlineData("Hiphens")]
        public async Task ThrowsArgumentExceptionAsync(string value)
        {
            Func<Task> action = async () => await _fixture
                .Create<FormatterCommand>()
                .ProcessAsync(value, new());

            var exception = await action
                .Should()
                .ThrowAsync<ArgumentException>();

            exception
                .WithMessage($"Unknown '{value}' as formatter option.");
        }
    }
}