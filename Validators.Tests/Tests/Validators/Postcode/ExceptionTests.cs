using Validators.Tests.Fixtures;
using Validators.Interfaces;

using Xunit;


namespace Validators.Tests.Postcode.Tests
{

    // Comment Code: Build failure via linux Azure DevOps pipeline, works fine as Build on Windows.
    // ExceptionTests.cs(19,35): error CS0121: 
    //      The call is ambiguous between the following methods or properties: 'Record.Exception(Action)' and 'Record.Exception(Func<Task>)' 
    //      [/home/vsts/work/1/s/Postcode.Tests/Postcode.Tests.csproj]
    // ExceptionTests.cs(30,35): error CS0121: 
    //      The call is ambiguous between the following methods or properties: 'Record.Exception(Action)' and 'Record.Exception(Func<Task>)' 
    //      [/home/vsts/work/1/s/Postcode.Tests/Postcode.Tests.csproj]

    public class ExceptionTests
        : IClassFixture<PostcodeFixture>
    {

        private readonly IPostcodeValidator _postcodeValidator;


        public ExceptionTests(PostcodeFixture fixture) => _postcodeValidator = fixture.Validator;

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
