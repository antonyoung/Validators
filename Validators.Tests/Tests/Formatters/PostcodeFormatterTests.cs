using Xunit;

using Validators.Formatters;
using System.Collections.Generic;


namespace Validators.Tests.Formatters
{
    public class PostcodeFormatterTests
    {
        /// <summary>
        ///     used as test data.
        /// </summary>
        public static readonly IEnumerable<object[]> TestData = new List<object[]>
        {
            new object[] { "EC1A 1BB" },
            new object[] { "1062 GD" },
            new object[] { "EDG 1062" },
            new object[] { "54321" },
            new object[] { "123 45" },
            new object[] { "12-345" },
            new object[] { "123-45" },
            new object[] { "SE-434 78" },
            new object[] { "HR-12345" }
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public void None(string value)
        {
            // => validate without formatting
            var result = value.Format(PostcodeFormatters.None);

            // => success, result == value
            Assert.Equal(result, value);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Hyphens(string value)
        {
            // => validate with hyphen formatter.
            var result = value.Format(PostcodeFormatters.Hyphens);

            // => success, result == value without hyphens.
            Assert.Equal(result, value.Replace("-", string.Empty));
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void WhiteSpaces(string value)
        {
            // => validate with whitespace formatter
            var result = value.Format(PostcodeFormatters.WhiteSpaces);

            // => success, result == value without whitespaces.
            Assert.Equal(result, value.Replace(" ", string.Empty));
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void HyphenAndWhiteSpaces(string value)
        {
            // => validate with hyphens and whitespaces formatters
            var result = value.Format(PostcodeFormatters.HyphensAndWhiteSpaces);

            // => success, result == value without hyphens and or whitespaces.
            Assert.Equal(result, value.Replace(" ", string.Empty).Replace(" ", string.Empty));
        }
    }
}
