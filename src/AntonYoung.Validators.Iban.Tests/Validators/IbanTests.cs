using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Tests.Fixtures;
using AntonYoung.Validators.Iban.Tests.TestData;
using AntonYoung.Validators.Iban.Tests.TestModels;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AntonYoung.Validators.Tests.Iban
{
    public class IbanTests 
        : IClassFixture<DependencyFixture>
    {
        //=> Bulgaria BG07TTBB94004773868743

        // todo: add test of all european countries
        // todo: add failure tests of all countries
        // todo: add failure length of all countries
        // todo: add failure sanity check of all countries

        private readonly ServiceProvider _serviceProvider;

        public IbanTests(DependencyFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;
 
        [Theory]
        [ClassData(typeof(IbanValidTestData))]
        public void Everything(IbanTestModel model)
        {
            var ibanValidator = _serviceProvider
                .GetService<IIbanValidator>();

            //=> validate iban values without whitespaces.
            var isValid = ibanValidator
                .TryValidate(model.Value.Replace(" ", string.Empty), out string result);

            //=> success
            ibanValidator.IsValid
                .Should()
                .BeTrue();
            
            ibanValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            ibanValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> check all properties of given iban value.
            ibanValidator.AccountNumber
                .Should()
                .Be(model.AccountNumber);
            
            ibanValidator.AccountType
                .Should()
                .Be(model.AccountType);
            
            ibanValidator.Country
                .Should()
                .Be(model.Country);
            
            ibanValidator.CheckDigits
                .Should()
                .Be(model.CheckDigits);
            
            ibanValidator.Example
                .Should()
                .Be(model.Example);
            
            ibanValidator.NationalBankCode
                .Should()
                .Be(model.NationalBankCode);
            
            ibanValidator.NationalBranchCode
                .Should()
                .Be(model.NationalBranchCode);
            
            ibanValidator.NationalCheckDigit
                .Should()
                .Be(model.NationalCheckDigit);

            //=> formatted result as iban value with whitespaces
            result
                .Should()
                .BeEquivalentTo(model.Value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoWhiteSpaces(string value)
        {
            var ibanValidator = _serviceProvider
                .GetService<IIbanValidator>();

            //=> validate iban values with whitespaces.
            var isValid = ibanValidator
                .TryValidate(value.Replace(" ", string.Empty), out string result);

            //=> success
            ibanValidator.IsValid
                .Should()
                .BeTrue();
            
            ibanValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            ibanValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result
                .Should()
                .BeEquivalentTo(value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void LowerCase(string value)
        {
            var ibanValidator = _serviceProvider
                .GetService<IIbanValidator>();

            //=> validate iban values with whitespaces.
            var isValid = ibanValidator
                .TryValidate(value.ToLowerInvariant(), out string result);

            //=> success
            ibanValidator.IsValid
                .Should()
                .BeTrue();
            
            ibanValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            ibanValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result
                .Should()
                .BeEquivalentTo(value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoTrim(string value)
        {
            var ibanValidator = _serviceProvider
                .GetService<IIbanValidator>();

            //=> validate iban values with whitespaces.
            var isValid = ibanValidator
                .TryValidate($" {value} ", out string result);

            //=> success
            ibanValidator.IsValid
                .Should()
                .BeTrue();
            
            ibanValidator.IsValid
                .Should()
                .Be(isValid);

            //=> has no error message
            ibanValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result
                .Should()
                .BeEquivalentTo(value);
        }
    }
}