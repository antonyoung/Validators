using System;
using Validators.Iban.Extensions;
using Validators.Iban.Tests.TestData;
using Xunit;

/// Ubuntu => Latest version, does not happen with windows? see also 
/// Restore completed in 49.54 ms for /home/vsts/work/1/s/Validators.Tests/Validators.Tests.csproj.
/// Restore completed in 49.44 ms for /home/vsts/work/1/s/Validators/Validators.csproj.
/// Validators -> /home/vsts/work/1/s/Validators/bin/Release/netcoreapp2.2/Validators.dll
/// Tests/Extensions/SanityExtensionTests.cs(68,20): error CS0121: The call is ambiguous between the following methods or properties: 'Assert.Throws<T>(Action)' and 'Assert.Throws<T>(Func<Task>)' [/home/vsts/work/1/s/Validators.Tests/Validators.Tests.csproj]
/// Tests/Extensions/SanityExtensionTests.cs(82,20): error CS0121: The call is ambiguous between the following methods or properties: 'Assert.Throws<T>(Action)' and 'Assert.Throws<T>(Func<Task>)' [/home/vsts/work/1/s/Validators.Tests/Validators.Tests.csproj]
/// => Build FAILED.

namespace Validators.Tests.Extensions
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
            // => convert char[1] as Int64 value
            var result = value.CharAsInt();

            // => succes, expectd = result.
            Assert.Equal(expected, result);
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
            // => convert char[] as Int64.
            var result = value.CharAsInt();

            // => success, expected = result.
            Assert.Equal(expected, result);
        }

        /// <summary>
        ///     used as to validate argument exception
        /// </summary>
        //[Fact]
        //public void ArgumentException()
        //{
        //    // => converted char[] is to large for Int64.
        //    void action() => "ABCDEFGHIJ".ToCharArray().CharAsInt();

        //    // => throws argument excpetion.
        //    Assert.Throws<ArgumentException>(action);
        //}


        ///// <summary>
        /////     used as to validate argument out of range exception.
        ///// </summary>
        //[Fact]
        //public void ArgumentOutOfRangeException()
        //{
        //    // => convert char[1] { 'a' } to an Int64, that does not exist. 
        //    void action() => "a".ToCharArray().CharAsInt();

        //    //=> throws argument out of range exception, char has not been found.
        //    Assert.Throws<ArgumentOutOfRangeException>(action);
        //}
    }
}