using AntonYoung.Validators.Iban.Extensions;
using AntonYoung.Validators.Iban.Tests.TestData;
using FluentAssertions;
using System;
using Xunit;

namespace AntonYoung.Validators.Tests.Extensions
{
    /// <summary>
    ///     used as test class, for testing the SanityExtension.
    /// </summary>
    public class SanityExtensionTests
    {       
        /// <summary>
        ///     used as test, to test every available index. 
        /// </summary>
        /// <param name="value">
        ///     used as <see cref="Char[1]"/> value to be tested.
        /// </param>
        /// <param name="expected">
        ///     used as the expected result.
        /// </param>
        [Theory]
        [ClassData(typeof(SanityIndexTestData))]
        public void ValidateIndex(char[] value, Int64 expected)
        {
            //=> convert char[1] as Int64 value
            var result = value.CharAsInt();

            //=> succes, expectd = result.
            expected.Should().Be(result);
        }

        /// <summary>
        ///     used as test of Sanity check values.
        /// </summary>
        /// <param name="value">
        ///     used as <see cref="Char[]"/> to be tested.
        /// </param>
        /// <param name="expected">
        ///     used as the expected result of <paramref name="value"/>
        /// </param>
        [Theory]
        [ClassData(typeof(SanityTestData))]
        public void Validate(char[] value, Int64 expected)
        {
            //=> convert char[] as Int64.
            var result = value.CharAsInt();

            //=> success, expected = result.
            expected.Should().Be(result);
        }

        /// <summary>
        ///     used as to validate argument exception
        /// </summary>
        [Fact]
        public void ThrowsArgumentException()
        {
            //=> converted char[] is to large for Int64.
            Action act = () => "ABCDEFGHIJ".ToCharArray().CharAsInt();

            //=> throws argument excpetion.
            act.Should().Throw<ArgumentException>();
        }

        ///// <summary>
        /////     used as to validate argument out of range exception.
        ///// </summary>
        [Fact]
        public void ThrowsArgumentOutOfRangeException()
        {
            //=> convert char[1] { 'a' } to an Int64, that does not exist. 
            Action act = () => "a".ToCharArray().CharAsInt();

            //=> throws argument out of range exception, char has not been found.
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}