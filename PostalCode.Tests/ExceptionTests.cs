using PostalCode.Library;

using System;
using Xunit;


namespace PostalCode.Tests
{

    public class ExceptionTests
    {
        [Fact]
        public void ThrowsArgumentException()
        {
            void unknownCountry() => new Library.PostalCode(Countries.Amsterdam);
            Exception ex = Record.Exception(unknownCountry);

            Assert.IsType<ArgumentException>(ex);
        }
    }
}
