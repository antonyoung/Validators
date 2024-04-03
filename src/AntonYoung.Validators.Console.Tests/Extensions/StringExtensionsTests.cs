using AntonYoung.Validators.Console.Extensions;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Console.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("-h")]
        [InlineData("-H")]
        [InlineData("--help")]
        [InlineData("--HELP")]
        public void IsHelpValidatesAsTrue(string value)
        {
            var result = value
                .IsHelp();

            result
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("-v")]
        [InlineData("--version")]
        [InlineData("--h")]
        [InlineData("--H")]
        [InlineData("-help")]
        [InlineData("-HELP")]
        public void IsHelpValidatesAsFalse(string value) 
        {
            var result = value
                .IsHelp();

            result
                .Should()
                .BeFalse();
        }
    }
}