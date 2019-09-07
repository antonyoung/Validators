using System;
using Xunit;

using Validators.Formatters;
using System.Collections.Generic;

namespace Formatters.Tests
{
    public class PostcodeFormatterTests
    {
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
            var result = value.Format(PostcodeFormatters.None);

            Assert.Equal(result, value);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Hyphens(string value)
        {
            var result = value.Format(PostcodeFormatters.Hyphens);

            Assert.Equal(result, value.Replace("-", string.Empty));
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void WhiteSpaces(string value)
        {
            var result = value.Format(PostcodeFormatters.WhiteSpaces);

            Assert.Equal(result, value.Replace(" ", string.Empty));
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void HyphenAndWhiteSpaces(string value)
        {
            var result = value.Format(PostcodeFormatters.HyphensAndWhiteSpaces);

            Assert.Equal(result, value.Replace(" ", string.Empty).Replace(" ", string.Empty));
        }
    }
}
