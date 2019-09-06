using Validators;
using Validators.Interfaces;

using System;
using Xunit;


namespace Postcode.Tests
{

    public class ExceptionTests
    {
        [Fact]
        public void ThrowsArgumentException()
        {
            var test = new PostcodeValidator();
            
            void unknownCountry() => test.TryParse(string.Empty, Countries.Amsterdam, out string result);
            Exception ex = Record.Exception(unknownCountry);

            Assert.IsType<ArgumentException>(ex);
        }
    }
}
