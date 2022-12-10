using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace AntonYoung.Validators.Postalcode.Tests
{
    public class ExceptionTests
        : IClassFixture<DependencyFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        public ExceptionTests(DependencyFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;

        [Fact]
        public void ThrowsNotSupportedExceptionOfCountry()
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            Action act = () => postalcodeValidator
                .TryValidate(string.Empty, Countries.Amsterdam, out string result);

            act.Should().Throw<NotSupportedException>()
                .And.Message.Should().Be("country");
        }
    }
}