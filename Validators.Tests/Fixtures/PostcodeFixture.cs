namespace Validators.Tests.Fixtures
{

    using Validators.Interfaces;


    /// <summary>
    ///     used as xUnit fixture for <see cref="PostcodeValidator"/>
    /// </summary>
    public class PostcodeFixture
    {

        /// <summary>
        ///     used as the interface <see cref="IPostcodeValidator"/> to be used for the test classes.
        /// </summary>
        public IPostcodeValidator Validator { get; private set; }


        /// <summary>
        ///     used as constructor logic to define the <see cref="Validator"/>
        /// </summary>
        public PostcodeFixture() => Validator = new PostcodeValidator();
    }
}
