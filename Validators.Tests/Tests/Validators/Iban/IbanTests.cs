using Xunit;
using Validators.Tests.Fixtures;
using Validators.Tests.TestData;
using Validators.Tests.TestModels;

using Validators.Interfaces;

namespace Validators.Tests.Iban
{

    public class IbanTests
      //  : IClassFixture<IbanFixture>
    {

        private readonly IIbanValidator _ibanValidator = new IbanValidator();

        //public IbanTests(IbanFixture fixture) => _ibanValidator = fixture.Validator;

        // todo: add sanityextension tests

        // => Bulgaria BG07TTBB94004773868743

        // todo: add test of all european countries
        // todo: add failure tests of all countries
        // todo: add failure length of all countries
        // todo: add failure sanity check of all countries


        [Theory]
        [ClassData(typeof(IbanValidTestData))]
        public void Everything(IbanTestModel model)
        {
            // => validate iban values without whitespaces.
            var isValid = _ibanValidator.Validate(model.Value.Replace(" ", string.Empty), out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => check all properties of given iban value.
            Assert.Equal(model.AccountNumber, _ibanValidator.AccountNumber);
            Assert.Equal(model.Country, _ibanValidator.Country);
            Assert.Equal(model.CheckDidgets, _ibanValidator.CheckDidgets);
            Assert.Equal(model.Example, _ibanValidator.Example);
            Assert.Equal(model.NationalBankCode, _ibanValidator.NationalBankCode);
            Assert.Equal(model.NationalBranchCode, _ibanValidator.NationalBranchCode);
            Assert.Equal(model.NationalCheckDidget, _ibanValidator.NationalCheckDidget);

            // => formatted result as iban value with whitespaces
            Assert.Equal(model.Value, result);
        }


        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoWhiteSpaces(string value)
        {
            // => validate iban values with whitespaces.
            var isValid = _ibanValidator.Validate(value.Replace(" ", string.Empty), out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => formatted result as iban value with whitespaces.
            Assert.Equal(value, result);
        }


        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void LowerCase(string value)
        {
            // => validate iban values with whitespaces.
            var isValid = _ibanValidator.Validate(value.ToLowerInvariant(), out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => formatted result as iban value with whitespaces.
            Assert.Equal(value, result);
        }


        [Theory]
        [ClassData(typeof(IbanTestData))]
        public void NoTrim(string value)
        {
            // => validate iban values with whitespaces.
            var isValid = _ibanValidator.Validate($" {value} ", out string result);

            // => success
            Assert.True(isValid);
            Assert.Equal(isValid, _ibanValidator.IsValid);

            // => has no error message
            Assert.True(string.IsNullOrEmpty(_ibanValidator.ErrorMessage));

            // => formatted result as iban value with whitespaces.
            Assert.Equal(value, result);
        }
    }
}
