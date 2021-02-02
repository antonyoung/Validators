using Validators.Abstractions.Enums;
using Validators.Iban.Infrastructure;


namespace Validators.Iban.Models
{

    /// <summary>
    ///     interface used to define the logic needed for iban validation.
    /// </summary>
    public struct IbanRuleSetModel
        : IRuleSet
    {

        /// <summary>
        ///     used as the country of the iban value. 
        /// </summary>
        public Countries Country { get; set; }


        /// <summary>
        ///     used as how to format the given iban value, based on <see cref="System.Text.RegularExpressions.Group.Name"/>.
        /// </summary>
        public string DisplayFormat { get; set; }


        /// <summary>
        ///     used as an example as iban value of <see cref="Country"/>.
        /// </summary>
        public string Example { get; set; }


        /// <summary>
        ///     used as additional internal validation of expected length of iban value.
        /// </summary>
        public int Length { get; set; }


        /// <summary>
        ///     used as internal validation of the sanity check, based on <see cref="Country"/> and <see cref="System.Text.RegularExpressions.Group.Name"/>.
        /// </summary>
        public string SanityFormat { get; set; }


        /// <summary>
        ///     used as <see cref="System.Text.RegularExpressions.Group.Name"/> to validate, sanity check and format the iban value.
        /// </summary>
        public string RegexPattern { get; set; }
    }
}
