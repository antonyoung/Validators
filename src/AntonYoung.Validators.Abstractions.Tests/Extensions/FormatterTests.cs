using AntonYoung.Validators.Abstractions.Extensions;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace AntonYoung.Validators.Abstractions.Tests.Extensions
{
    /// <summary>
    ///     used as test class, to test the all postalcode formatters.
    /// </summary>
    public class FormatterTests
    {
        private const string ReplaceValue = ".";

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.None"/>, 
        ///     no formatting.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void None(string value)
        {
            //=> validate without formatting
            var result = value.Format(Enums.Formatters.None);

            //=> success, result == value
            result.Should().Be(value);
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.Hyphens"/>, 
        ///     formatting removes all hyphens if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void Hyphens(string value)
        {
            //=> validate with hyphen formatter.
            var result = value.Format(Enums.Formatters.Hyphens);

            //=> success, result == value without hyphens.
            result.Should().Be(value.Replace("-", string.Empty));
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.WhiteSpaces"/>, 
        ///     formatting removes all whitespaces if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void WhiteSpaces(string value)
        {
            //=> validate with whitespace formatter
            var result = value.Format(Enums.Formatters.WhiteSpaces);

            //=> success, result == value without whitespaces.
            result.Should().Be(value.Replace(" ", string.Empty));
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.HyphensAndWhiteSpaces"/>, 
        ///     formatting removes all hyphens and whitespaces if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void HyphenAndWhiteSpaces(string value)
        {
            //=> validate with hyphens and whitespaces formatter
            var result = value.Format(Enums.Formatters.HyphensAndWhiteSpaces);

            //=> success, result == value without hyphens and or whitespaces.
            result.Should().Be(value
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty));
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.None"/>, 
        ///     no formatting with replace value.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void ReplaceNone(string value)
        {
            //=> validate without formatting
            var result = value.Format(Enums.Formatters.None, ReplaceValue);

            //=> success, result == value ( no replace shoud happen )
            result.Should().Be(value);
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.Hyphens"/>, 
        ///     formatting replaces all hyphens with replace value, if any
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void ReplaceHyphens(string value)
        {
            //=> validate with hyphen formatter.
            var result = value.Format(Enums.Formatters.Hyphens, ReplaceValue);

            //=> success, result == value without hyphens are replaced with replace value.
            result.Should().Be(value.Replace("-", ReplaceValue));
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.WhiteSpaces"/>, 
        ///     formatting replaces all whitespaces with replace value, if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void ReplaceWhiteSpaces(string value)
        {
            //=> validate with whitespace formatter
            var result = value.Format(Enums.Formatters.WhiteSpaces, ReplaceValue);

            //=> success, result == value without whitespaces are replaced with replace value.
            result.Should().Be(value.Replace(" ", ReplaceValue));
        }

        /// <summary>
        ///     used as tests <see cref="TestData"/> with <see cref="Formatters.HyphensAndWhiteSpaces"/>, 
        ///     formatting replaces all hyphens and whitespaces with replace value, if any.
        /// </summary>
        /// <param name="value">
        ///     used as the value to be formatted.
        /// </param>
        [Theory]
        [ClassData(typeof(PostalcodeFormatterTestData))]
        public void ReplaceHyphenAndWhiteSpaces(string value)
        {
            //=> validate with hyphens and whitespaces formatter
            var result = value.Format(Enums.Formatters.HyphensAndWhiteSpaces, ReplaceValue);

            //=> success, result == value without hyphens and or whitespaces are replaces with replace value.
            result.Should().Be(value
                .Replace("-", ReplaceValue)
                .Replace(" ", ReplaceValue));
        }
    }

    public class PostalcodeFormatterTestData : IEnumerable<object[]>
    {
        /// <summary>
        ///     used as internal test data, for this test class. Add additonal test data as you wish.
        /// </summary>
        private readonly IEnumerable<object[]> _data = new List<object[]>
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

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}