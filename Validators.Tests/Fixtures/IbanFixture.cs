namespace Validators.Tests.Fixtures
{

    using Validators.Interfaces;


    /// <summary>
    ///     used as xUnit fixture for <see cref="IbanValidator"/>
    /// </summary>
    public class IbanFixture
    {

        /// <summary>
        ///     used as interface <see cref="IIbanValidator"/> to be used for the test classes.
        /// </summary>
        public IIbanValidator Validator;


        /// <summary>
        ///     used as constructor logic to define <see cref="Validator"/>
        /// </summary>
        public IbanFixture() => Validator = new IbanValidator();
    }
}
