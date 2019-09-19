using System.Collections.Generic;
using Validators.Formatters;
using Xunit;


namespace Validators.Tests.Formatters
{

    /// <summary>
    ///     used as test class, to test the all postalcode formatters.
    /// </summary>
    public class PostcodeFormatterTests
    {

        /// <summary>
        ///     used as internal test data, for this test class. Add additonal test data as you wish.
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


        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="PostcodeFormatters.None"/>, 
        ///     no formatting.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void None(string value)
        {
            // => validate without formatting
            var result = value.Format(PostcodeFormatters.None);

            // => success, result == value
            Assert.Equal(result, value);
        }


        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="PostcodeFormatters.Hyphens"/>, 
        ///     formatting removes all hyphens if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void Hyphens(string value)
        {
            // => validate with hyphen formatter.
            var result = value.Format(PostcodeFormatters.Hyphens);

            // => success, result == value without hyphens.
            Assert.Equal(result, value.Replace("-", string.Empty));
        }


        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="PostcodeFormatters.WhiteSpaces"/>, 
        ///     formatting removes all whitespaces if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void WhiteSpaces(string value)
        {
            // => validate with whitespace formatter
            var result = value.Format(PostcodeFormatters.WhiteSpaces);

            // => success, result == value without whitespaces.
            Assert.Equal(result, value.Replace(" ", string.Empty));
        }


        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="PostcodeFormatters.HyphensAndWhiteSpaces"/>, 
        ///     formatting removes all hyphens and whitespaces if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void HyphenAndWhiteSpaces(string value)
        {
            // => validate with hyphens and whitespaces formatter
            var result = value.Format(PostcodeFormatters.HyphensAndWhiteSpaces);

            // => success, result == value without hyphens and or whitespaces.
            Assert.Equal(result, value.Replace(" ", string.Empty).Replace(" ", string.Empty));
        }
    }
}
