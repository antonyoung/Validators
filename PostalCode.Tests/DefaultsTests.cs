using PostalCode.Library.Core;
using Xunit;

namespace PostalCode.Tests
{

    /// <summary>
    ///     test class, to test constructor overloading and that the default country = Netherlands.
    ///     also tests valid and invalid postal codes of the default country.
    /// </summary>
    public class DefaultsTests
    {

        [Fact]
        public void DefaultCountry()
        {
            var postalCode = new Library.Core.PostalCode();

            Assert.Equal(Countries.Netherlands, postalCode.Country);
        }

        [Fact]
        public void Valid()
        { 
            var postalCode = new Library.Core.PostalCode("1015AA");

            Assert.Equal(Countries.Netherlands, postalCode.Country);
            Assert.True(postalCode.IsValid);
            Assert.Equal("1015 AA", postalCode.ToString());
        }

        [Fact]
        public void InValid()
        {
            var postalCode = new Library.Core.PostalCode("xyz");

            Assert.Equal(Countries.Netherlands, postalCode.Country);
            Assert.False(postalCode.IsValid);
            Assert.Equal("xyz", postalCode.ToString());
        }
    }
}
