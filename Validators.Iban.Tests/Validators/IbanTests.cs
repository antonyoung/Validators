using FluentAssertions; 
using Validators.Iban;
using Validators.Iban.Infrastructure;
using Validators.Iban.Tests.TestData;
using Validators.Iban.Tests.TestModels;
using Xunit;

namespace Validators.Tests.Iban
{
    public class IbanTests
    {
        private readonly IIbanValidator _ibanValidator = new IbanValidator();

        //=> Bulgaria BG07TTBB94004773868743

        // todo: add test of all european countries
        // todo: add failure tests of all countries
        // todo: add failure length of all countries
        // todo: add failure sanity check of all countries

        [Theory]
        [ClassData(typeof(IbanValidTestData))]
        public void Everything(IbanTestModel model)
        {
            //=> validate iban values without whitespaces.
            var isValid = _ibanValidator.TryValidate(model.Value.Replace(" ", string.Empty), out string result);

            //=> success
            _ibanValidator.IsValid.Should().BeTrue();
            _ibanValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _ibanValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> check all properties of given iban value.
            _ibanValidator.AccountNumber.Should().Be(model.AccountNumber);
            _ibanValidator.AccountType.Should().Be(model.AccountType);
            _ibanValidator.Country.Should().Be(model.Country);
            _ibanValidator.CheckDigits.Should().Be(model.CheckDigits);
            _ibanValidator.Example.Should().Be(model.Example);
            _ibanValidator.NationalBankCode.Should().Be(model.NationalBankCode);
            _ibanValidator.NationalBranchCode.Should().Be(model.NationalBranchCode);
            _ibanValidator.NationalCheckDigit.Should().Be(model.NationalCheckDigit);

            //=> formatted result as iban value with whitespaces
            result.Should().BeEquivalentTo(model.Value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoWhiteSpaces(string value)
        {
            //=> validate iban values with whitespaces.
            var isValid = _ibanValidator.TryValidate(value.Replace(" ", string.Empty), out string result);

            //=> success
            _ibanValidator.IsValid.Should().BeTrue();
            _ibanValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _ibanValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result.Should().BeEquivalentTo(value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void LowerCase(string value)
        {
            //=> validate iban values with whitespaces.
            var isValid = _ibanValidator.TryValidate(value.ToLowerInvariant(), out string result);

            //=> success
            _ibanValidator.IsValid.Should().BeTrue();
            _ibanValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _ibanValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result.Should().BeEquivalentTo(value);
        }

        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoTrim(string value)
        {
            //=> validate iban values with whitespaces.
            var isValid = _ibanValidator.TryValidate($" {value} ", out string result);

            //=> success
            _ibanValidator.IsValid.Should().BeTrue();
            _ibanValidator.IsValid.Should().Be(isValid);

            //=> has no error message
            _ibanValidator.ErrorMessage.Should().BeNullOrEmpty();

            //=> formatted result as iban value with whitespaces.
            result.Should().BeEquivalentTo(value);
        }
    }
}