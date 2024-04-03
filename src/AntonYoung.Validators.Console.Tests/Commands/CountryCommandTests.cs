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
    public class CountryCommandTests
    {
        private readonly IFixture _fixture;

        public CountryCommandTests()
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
            var result = await _fixture.Create<CountryCommand>()
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Help);
        }

        [Theory]
        [InlineData("Netherlands")]
        [InlineData("nl")]
        [InlineData("nld")]
        [InlineData(" ")]
        public async Task ProcessAsArgumentArgumentAsync(string value)
        {
            ValidatorModel model = new();

            var result = await _fixture.Create<CountryCommand>()
                .ProcessAsync(value, model);

            result
                .Should()
                .Be(Arguments.Argument);

            model.Country
                .Should()
                .Be(value);
        }
    }
}