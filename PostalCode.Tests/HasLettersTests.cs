using PostalCode.Library;
using Xunit;


namespace PostalCode.Tests
{
    public class HasLettersTests
    {

        private const string NETHERLANDS = "1062 GD";
        private const string MALTA = "EDG 1062";


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.Replace(" ", string.Empty));

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithSingleSpace(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode);

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithDoubleSpace(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace(" ", "  ");

            var test = new Library.PostalCode(country, postalCode);

            Assert.False(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void WithHyphen(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace(" ", "-");

            var test = new Library.PostalCode(country, postalCode);

            Assert.False(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void NoTrim(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, $" {postalCode} ");

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LowerCase(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.ToLowerInvariant());

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.Netherlands, NETHERLANDS)]
        [InlineData(Countries.Malta, MALTA)]
        public void LeadingZero(Countries country, string postalCode)
        {
            postalCode = postalCode.Replace("1", "0");
            var test = new Library.PostalCode(country, postalCode);

            Assert.False(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }
    }
}
