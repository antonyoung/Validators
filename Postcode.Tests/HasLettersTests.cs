using Validators;
using Validators.Interfaces;

using Xunit;


namespace Postcode.Tests
{
    public class HasLettersTests
    {

        private PostcodeValidator _postcodeValidator = new PostcodeValidator();

        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.Replace(" ", string.Empty), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace(" ", "  ");
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithHyphen(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace(" ", "-");

            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void NoTrim(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse($" {postalCode} ", country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postalCode)
        {
            bool isValid = _postcodeValidator.TryParse(postalCode.ToLowerInvariant(), country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace("1", "0");
            bool isValid = _postcodeValidator.TryParse(postalCode, country, out string result);

            Assert.Equal(isValid, _postcodeValidator.IsValid);
            Assert.Equal(postalCode, result);
        }
    }
}
