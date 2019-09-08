using Validators;
using Validators.Interfaces;

using System;
using Xunit;


namespace Postcode.Tests
{

    public class ExceptionTests
    {
        //[Fact]
        //public void ThrowsArgumentExceptionOfCountry()
        //{
        //    var test = new PostcodeValidator();
            
        //    void unknownCountry() => test.Validate(string.Empty, Countries.Amsterdam, out string result);
        //    Exception ex = Record.Exception(unknownCountry);

        //    Assert.IsType<ArgumentException>(ex);
        //}

        [Fact]
        public void ThrowsArgumentExceptionOfValue()
        {
            var test = new PostcodeValidator();

            void value() => test.Validate(null, Countries.Netherlands, out string result);
            Exception ex = Record.Exception(value);

            Assert.IsType<ArgumentException>(ex);
        }
    }
}
