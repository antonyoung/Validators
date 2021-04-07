using FluentAssertions;
using System;
using Validators.Abstractions.Enums;
using Validators.Postalcode;
using Xunit;

namespace Validators.Tests.Postcode
{
    public class ExceptionTests
    { 
        [Fact]
        public void ThrowsArgumentExceptionOfCountry()
        {
            Action act = () => new PostalcodeValidator().TryValidate(string.Empty, Countries.Amsterdam, out string result);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionOfValue()
        {
            Action act = () => new PostalcodeValidator().TryValidate(null, Countries.Netherlands, out string result);

            act.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("value");
        }
    }
}