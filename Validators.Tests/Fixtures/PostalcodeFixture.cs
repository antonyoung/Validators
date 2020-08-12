namespace Validators.Tests.Fixtures
{

    using Validators.Interfaces;


    /// <summary>
    ///     used as xUnit fixture for <see cref="PostalcodeValidator"/>
    /// </summary>
    public class PostalcodeFixture
    {

        /// <summary>
        ///     used as interface <see cref="IPostalcodeValidator"/> to be used for the test classes.
        /// </summary>
        public IPostalcodeValidator Validator { get; private set; }


        /// <summary>
        ///     used as constructor logic to define <see cref="Validator"/>
        /// </summary>
        public PostalcodeFixture() => Validator = new PostalcodeValidator();
    }
}
