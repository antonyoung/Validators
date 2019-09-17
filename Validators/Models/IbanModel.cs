using System.Collections.Generic;

using Validators.Interfaces;


namespace Validators.Models
{

    // DATA: based on https://en.wikipedia.org/wiki/International_Bank_Account_Number

    //    Amsterdam,        // => argument exception
    //    Austria,          // => 20,	16n,	    ATkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    Belgium,          // => 16,	12n,	    BEkk bbbc cccc ccxx,	                b = National bank code, c = Account number, x = National check digits
    //    Bulgaria,         // => 22,	4a,6n,8c,   BGkk bbbb ssss ttcc cccc cc,	        b = BIC bank code, s = Branch(BAE) number, t = Account type, c = Account number
    //    Croatia,          // => 21,	17n,	    HRkk bbbb bbbc cccc cccc c, 	        b = Bank code, c = Account number
    //    Cyprus,           // => 28,	8n,16c,	    CYkk bbbs ssss cccc cccc cccc cccc,	    b = National bank code, s = Branch code, c = Account number
    //    Czechia,          // => 24,	20n,	    CZkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    Denmark,          // => 18,	14n,	    DKkk bbbb cccc cccc cc,	                b = National bank code, c = Account number
    //    Estonia,          // => 20,	16n,	    EEkk bbss cccc cccc cccx,	            b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    Finland,          // => 18,	14n,	    FIkk bbbb bbcc cccc cx,	                b = Bank and branch code, c = Account number, x = National check digit
    //    France,           // => 27,	10n,11c,2n,	FRkk bbbb bsss sscc cccc cccc cxx,	    b = National bank code, s = Branch code(fr:code guichet), c = Account number, x = National check digits(fr:clé RIB)
    //    Germany,          // => 22,	18n,	    DEkk bbbb bbbb cccc cccc cc,	        b = Bank and branch identifier (de:Bankleitzahl or BLZ), c = Account number
    //    Greece,           // => 27,	7n,16c,	    GRkk bbbs sssc cccc cccc cccc ccc,	    b = National bank code, s = Branch code, c = Account number
    //    Hungary,          // => 28,	24n,	    HUkk bbbs sssx cccc cccc cccc cccx,	    b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    Ireland,          // => 22,	4c,14n,	    IEkk aaaa bbbb bbcc cccc cc,	        a = BIC bank code, b = Bank/branch code(sort code), c = Account number
    //    Italy,            // => 27,	1a,10n,12c,	ITkk xbbb bbss sssc cccc cccc ccc,	    x = Check char (CIN), b = National bank code(Associazione Bancaria Italiana or Codice ABI), s = Branch code(it:Coordinate bancarie or CAB – Codice d'Avviamento Bancario), c = Account number
    //    Latvia,           // => 21,	4a,13c,	    LVkk bbbb cccc cccc cccc c,	            b = BIC Bank code, c = Account number
    //    Lithuania,        // => 20,	16n,	    LTkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    Luxembourg,       // => 20,	3n,13c,	    LUkk bbbc cccc cccc cccc,	            b = National bank code, c = Account number
    //    Malta,            // => 31,	4a,5n,18c,	MTkk bbbb ssss sccc cccc cccc cccc ccc,	b = BIC bank code, s = Branch code, c = Account number
    //    Netherlands,      // => 18,	4a,10n,	    NLkk bbbb cccc cccc cc,	                b = BIC Bank code, c = Account number
    //    Poland,           // => 28,	24n,	    PLkk bbbs sssx cccc cccc cccc cccc,	    b = National bank code, s = Branch code, x = National check digit, c = Account number,
    //    Portugal,         // => 25,	21n,	    PTkk bbbb ssss cccc cccc cccx x,	    k = IBAN check digits (always = "50"), b = National bank code(numeric only), s = Branch code(numeric only), c = Account number(numeric only), x = National check digits(numeric only)
    //    Romania,          // => 24,	4a,16c,	    ROkk bbbb cccc cccc cccc cccc,	        b = BIC Bank code (first four alpha characters), c = Branch code and account number(bank-specific format)
    //    Slovakia,         // => 24,	20n,	    SKkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    Slovenia,         // => 19,	15n,	    SIkk bbss sccc cccc cxx,	            k = IBAN check digits (always = "56"), b = National bank code, s = Branch code, c = Account number, x = National check digits
    //    Spain,            // => 24,	20n,	    ESkk bbbb ssss xxcc cccc cccc,	        b = National bank code, s = Branch code, x = Check digits, c = Account number
    //    Sweden,           // => 24,	20n,	    SEkk bbbc cccc cccc cccc cccc,	        b = National bank code, c = Account number
    //    UnitedKingdom     // => 22,	4a,14n,	    GBkk bbbb ssss sscc cccc cc,	        b = BIC bank code, s = Bank and branch code(sort code), c = Account number


    /// <summary>
    ///     used as complete model of all iban logic for each defined internal country <seealso cref="Rules"/>
    /// </summary>
    public class IbanModel
        : IIbanModel
    {


        /// <summary>
        ///     used as in memory data source as all intenral business iban logic of the set TwoLetterISORegionName of countries <see cref="Rules"/>.
        /// </summary>
        public Dictionary<string, IbanRuleSetModel> Rules
        {
            get => new Dictionary<string, IbanRuleSetModel>
            {
                { "AT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)AT)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<code1>[0-9]{4})(?<whitespace>\s?)(?<code2>[0-9]{1})(?<account1>[0-9]{3})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <code1> <code2><account1> <account2> <account3>",
                        SanityFormat = "<code1><code2><account1><account2><account3><country><checksum>",
                        Example = "NLKK CCCC CNNN NNNN NNNN",
                        Length = 20,
                        Country = Countries.Austria
                    }
                },
                { "BE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)BE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<code>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2})(?<whitespace>\s?)(?<check>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <code><account1> <account2> <account3><check>",
                        SanityFormat = "<code><account1><account2><account3><check><country><checksum>",
                        Example = "BEKK CCCN NNNN NNXX",
                        Length = 16,
                        Country = Countries.Belgium
                    }
                },
                { "NL",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)NL)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<name>[a-zA-Z]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <name> <account1> <account2> <account3>",
                        SanityFormat = "<name><account1><account2><account3><country><checksum>",
                        Example = "NLKK AAAA NNNN NNNN NN",
                        Length = 18,
                        Country = Countries.Netherlands
                    }    
                }
            };
        }
    }
}
