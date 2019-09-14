using System.Collections.Generic;

using Validators.Interfaces;


namespace Validators.Models
{

    // DATA: based on https://en.wikipedia.org/wiki/International_Bank_Account_Number


    /// <summary>
    ///     used as complete model of all iban logic for each defined internal country <seealso cref="Rules"/>
    /// </summary>
    public class IbanModel
        : IIbanModel
    {


        /// <summary>
        ///     used as in memory data source as all intenral business iban logic of the set countries <see cref="Rules"/>.
        /// </summary>
        public Dictionary<Countries, IbanRuleSetModel> Rules
        {
            get => new Dictionary<Countries, IbanRuleSetModel>
            {
                { Countries.Netherlands, new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^[a-zA-Z]{2})(?<whitespace>\s?)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<name>[a-zA-Z]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country> <checksum> <account1> <account2> <account3>",
                        SanityFormat = "<name><account1><account2><account3><country><checksum>",
                        Example = "NL BANK NNNN NNNN NN",
                        Length = 18
                    }    
                }
            };
        }
    }
}
