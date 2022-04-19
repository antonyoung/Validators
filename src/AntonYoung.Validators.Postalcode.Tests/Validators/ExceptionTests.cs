using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode;
using FluentAssertions;
using System;
using Xunit;

namespace AntonYoung.Validators.Tests.Postcode
{
    public class ExceptionTests
    { 
        [Fact]
        public void ThrowsNotSupportedExceptionOfCountry()
        {
            Action act = () => new PostalcodeValidator()
                .TryValidate(string.Empty, Countries.Amsterdam, out string result);

            act.Should().Throw<NotSupportedException>()
                .And.Message.Should().Be("country");
        }
    }
}