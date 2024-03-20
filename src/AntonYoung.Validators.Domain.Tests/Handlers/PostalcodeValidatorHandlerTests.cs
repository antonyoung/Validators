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
    public class PostalcodeValidatorHandlerTests
    {
        [Fact]
        public async Task NoValidationErrors()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture
                .Freeze<Mock<IPostalcodeRequestValidator>>()
                .Setup(_ => _.ValidateAsync(It.IsAny<PostalcodeValidaionRequest>(), It.IsAny<Countries>()))
                .ReturnsAsync(new List<string>());

            var result = await fixture
                .Create<PostalcodeValidatorHandler>()
                .HandleAsync(new PostalcodeValidaionRequest { Value = "value" }, CancellationToken.None);

            result
                .Should()
                .BeOfType<PostalcodeValidationResponse>();

            result.Country
                .Should()
                .Be(Countries.Amsterdam);

            result.ErrorMessage
                .Should()
                .BeNull();

            result.IsValid
                .Should() 
                .BeFalse();

            result.Result
                .Should()
                .BeNull();
        }
        
        [Fact]
        public async Task ThrowsRequestExceptionAsync()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture
                .Freeze<Mock<IPostalcodeRequestValidator>>()
                .Setup(_ => _.ValidateAsync(It.IsAny<PostalcodeValidaionRequest>(), Countries.Amsterdam))
                .ReturnsAsync(new List<string>
                { 
                    "Value is obliged, to be able to do a postalcode validation.",
                    "Amsterdam is not supported as country to be validated."
                });

            Func<Task> action = async ()
                => await fixture
                .Create<PostalcodeValidatorHandler>()
                .HandleAsync(new PostalcodeValidaionRequest { Value = "value" }, CancellationToken.None);

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
                .HaveCount(2);

            exception
                .Subject
                .ElementAt(0)
                .ErrorMessages
                .ElementAt(0)
                .Should()
                .Be("Value is obliged, to be able to do a postalcode validation.");

            exception
                .Subject
                .ElementAt(0)
                .ErrorMessages
                .ElementAt(1)
                .Should()
                .Be("Amsterdam is not supported as country to be validated.");
        }
    }
}