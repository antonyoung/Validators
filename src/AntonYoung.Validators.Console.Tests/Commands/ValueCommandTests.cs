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
    public class ValueCommandTests
    {
        private readonly IFixture _fixture;

        public ValueCommandTests()
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
                .Create<ValueCommand>()
                .ProcessAsync(value, new());

            result
                .Should()
                .Be(Arguments.Help);
        }

        [Fact]
        public async Task ProcessAsUnknownArgumentAsync()
        {
            var result = await _fixture
                .Create<ValueCommand>()
                .ProcessAsync(string.Empty, new());

            result
                .Should()
                .Be(Arguments.Unknown);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("postal code")]
        [InlineData("iban value")]
        [InlineData("Bank Account")]
        [InlineData("Zip Code")]
        public async Task ProcessAsArgumentArgumentAsync(string value)
        {
            ValidatorModel model = new();

            var result = await _fixture
                .Create<ValueCommand>()
                .ProcessAsync(value, model);

            result
                .Should()
                .Be(Arguments.Argument);

            model.Value
                .Should()
                .Be(value); 
        }
    }
}