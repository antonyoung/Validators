using Validators.Tests.Fixtures;

using Xunit;


namespace Validators.Tests.Collections
{
    [CollectionDefinition("Postcodes")]
    public class PostalcodeCollection
        : ICollectionFixture<PostalcodeFixture>
    { }
}
