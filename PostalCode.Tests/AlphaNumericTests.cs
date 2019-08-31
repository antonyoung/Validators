using PostalCode.Library;
using Xunit;

namespace PostalCode.Tests
{
    public class AlphaNumericTests
    {

        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void Valid(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode);

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "x2")]
        [InlineData(Countries.UnitedKingdom, "2344 AAA")]
        [InlineData(Countries.Ireland, "222 YD82")]
        public void InValid(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode);

            Assert.False(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }


        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var test = new Library.PostalCode(country, postalCode.Replace(" ", string.Empty));

            Assert.True(test.IsValid);
            Assert.Equal(country, test.Country);
            Assert.Equal(postalCode, test.ToString());
        }
    }
}
