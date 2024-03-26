using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Exceptions;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using AntonYoung.Validators.Domain.Validators;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Xunit;

namespace AntonYoung.Validators.Domain.Tests.Handlers
{
    public class IbanValidatorHandlerTests
    {
        [Fact]
        public async Task NoValidationErrors()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture
                .Freeze<Mock<IIbanRequestValidator>>()
                .Setup(_ => _.ValidateAsync(It.IsAny<IbanValidationRequest>()))
                .ReturnsAsync(new List<string>());

            var result = await fixture
                .Create<IbanValidatorHandler>()
                .HandleAsync(new IbanValidationRequest { Value = "value" }, CancellationToken.None);

            result
                .Should()
                .BeOfType<IbanValidationResponse>();

            result.AccountNumber
                .Should()
                .BeNull();

            result.AccountType
                .Should()
                .BeNull();

            result.CheckDigits
                .Should()
                .BeNull();

            result.Country
                .Should()
                .Be(Countries.Amsterdam);

            result.ErrorMessage
                .Should() 
                .BeNull();

            result.Result
                .Should() 
                .BeNull();

            result.IsValid
                .Should()
                .BeFalse();

            result.NationalBankCode
                .Should()
                .BeNull();

            result.NationalBranchCode
                .Should()
                .BeNull();

            result.NationalCheckDigit
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task ThrowsRequestExceptionAsync()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture
                .Freeze<Mock<IIbanRequestValidator>>()
                .Setup(_ => _.ValidateAsync(It.IsAny<IbanValidationRequest>()))
                .ReturnsAsync(new List<string> { "Value is obliged, to be able to do a iban validation." });

            Func<Task> action = async ()
                => await fixture
                .Create<IbanValidatorHandler>()
                .HandleAsync(new IbanValidationRequest { Value = "value" }, CancellationToken.None);

            var exception = await action
                .Should()
                .ThrowAsync<RequestException>();

            exception
                .Subject
                .ElementAt(0)
                .Message
                .Should()
                .Be("PostalcodeValidaionRequest is not valid.");

            exception
                .Subject
                .ElementAt(0)
                .ErrorMessages
                .Should()
                .HaveCount(1);

            exception
                .Subject
                .ElementAt(0)
                .ErrorMessages
                .ElementAt(0)
                .Should()
                .Be("Value is obliged, to be able to do a iban validation.");
        }
    }
}
