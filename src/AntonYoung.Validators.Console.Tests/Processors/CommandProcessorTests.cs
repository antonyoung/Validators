using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Processors;
using AntonYoung.Validators.Console.Writters;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Processors
{
    public class CommandProcessorTests
    {
        private readonly IFixture _fixture;

        public CommandProcessorTests() 
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _fixture
                .Freeze<Mock<IConsoleWriter>>();
        }

        [Theory]
        [InlineData(Arguments.Application, typeof(ApplicationCommand))]
        [InlineData(Arguments.Argument, typeof(ArgumentCommand))]
        [InlineData(Arguments.Country, typeof(CountryCommand))]
        [InlineData(Arguments.Formatter, typeof(FormatterCommand))]
        [InlineData(Arguments.Replace, typeof(ReplaceCommand))]
        [InlineData(Arguments.Value, typeof(ValueCommand))]
        [InlineData(Arguments.Validators, typeof(ValidatorCommand))]
        [InlineData(Arguments.Help, typeof(ArgumentCommand))]
        [InlineData(Arguments.Unknown, typeof(ArgumentCommand))]
        [InlineData(Arguments.Version, typeof(ArgumentCommand))]
        public async Task ProcessAsArgumentAsync(Arguments argument, Type commandType)
        {
            var result = await _fixture
                .Create<CommandProcessor>()
                .ProcessAsync(argument);

            result
                .Should()
                .BeOfType(commandType);
        }
    }
}