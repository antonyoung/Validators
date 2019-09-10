using Validators;
using Validators.Interfaces;

using System;
using Xunit;


namespace Postcode.Tests
{

    // comment code: Build failure via linux:
    // ExceptionTests.cs(30,35): error CS0121: 
    // The call is ambiguous between the following methods or properties: 'Record.Exception(Action)' and 'Record.Exception(Func<Task>)' 
    // [/home/vsts/work/1/s/Postcode.Tests/Postcode.Tests.csproj]

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

        //[Fact]
        //public void ThrowsArgumentExceptionOfValue()
        //{
        //    var test = new PostcodeValidator();

        //    void value() => test.Validate(null, Countries.Netherlands, out string result);
        //    Exception ex = Record.Exception(value);

        //    Assert.IsType<ArgumentException>(ex);
        //}
    }
}
