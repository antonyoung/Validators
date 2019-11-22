using System.Collections.Generic;

using Validators.Enums;
using Validators.Interfaces;


namespace Validators.Models
{

    // DATA: based on https://en.wikipedia.org/wiki/International_Bank_Account_Number

    //    Amsterdam,        // => argument exception
    //    * Austria,          // => 20,	16n,	    ATkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    * Belgium,          // => 16,	12n,	    BEkk bbbc cccc ccxx,	                b = National bank code, c = Account number, x = National check digits
    //    * Bulgaria,         // => 22,	4a,6n,8c,   BGkk bbbb ssss ttcc cccc cc,	        b = BIC bank code, s = Branch(BAE) number, t = Account type, c = Account number
    //    * Croatia,          // => 21,	17n,	    HRkk bbbb bbbc cccc cccc c, 	        b = Bank code, c = Account number
    //    * Cyprus,           // => 28,	8n,16c,	    CYkk bbbs ssss cccc cccc cccc cccc,	    b = National bank code, s = Branch code, c = Account number
    //    * Czechia,          // => 24,	20n,	    CZkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    * Denmark,          // => 18,	14n,	    DKkk bbbb cccc cccc cc,	                b = National bank code, c = Account number
    //    * Estonia,          // => 20,	16n,	    EEkk bbss cccc cccc cccx,	            b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    * Finland,          // => 18,	14n,	    FIkk bbbb bbcc cccc cx,	                b = Bank and branch code, c = Account number, x = National check digit
    //    France,           // => 27,	10n,11c,2n,	FRkk bbbb bsss sscc cccc cccc cxx,	    b = National bank code, s = Branch code(fr:code guichet), c = Account number, x = National check digits(fr:clé RIB)
    //    * Germany,          // => 22,	18n,	    DEkk bbbb bbbb cccc cccc cc,	        b = Bank and branch identifier (de:Bankleitzahl or BLZ), c = Account number
    //    * Greece,           // => 27,	7n,16c,	    GRkk bbbs sssc cccc cccc cccc ccc,	    b = National bank code, s = Branch code, c = Account number
    //    * Hungary,          // => 28,	24n,	    HUkk bbbs sssx cccc cccc cccc cccx,	    b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    Ireland,          // => 22,	4c,14n,	    IEkk aaaa bbbb bbcc cccc cc,	        a = BIC bank code, b = Bank/branch code(sort code), c = Account number
    //    * Italy,            // => 27,	1a,10n,12c,	ITkk xbbb bbss sssc cccc cccc ccc,	    x = Check char (CIN), b = National bank code(Associazione Bancaria Italiana or Codice ABI), s = Branch code(it:Coordinate bancarie or CAB – Codice d'Avviamento Bancario), c = Account number
    //    * Latvia,           // => 21,	4a,13c,	    LVkk bbbb cccc cccc cccc c,	            b = BIC Bank code, c = Account number
    //    * Lithuania,        // => 20,	16n,	    LTkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    * Luxembourg,       // => 20,	3n,13c,	    LUkk bbbc cccc cccc cccc,	            b = National bank code, c = Account number
    //    * Malta,            // => 31,	4a,5n,18c,	MTkk bbbb ssss sccc cccc cccc cccc ccc,	b = BIC bank code, s = Branch code, c = Account number
    //    * Netherlands,      // => 18,	4a,10n,	    NLkk bbbb cccc cccc cc,	                b = BIC Bank code, c = Account number
    //    * Poland,           // => 28,	24n,	    PLkk bbbs sssx cccc cccc cccc cccc,	    b = National bank code, s = Branch code, x = National check digit, c = Account number,
    //    * Portugal,         // => 25,	21n,	    PTkk bbbb ssss cccc cccc cccx x,	    k = IBAN check digits (always = "50"), b = National bank code(numeric only), s = Branch code(numeric only), c = Account number(numeric only), x = National check digits(numeric only)
    //    * Romania,          // => 24,	4a,16c,	    ROkk bbbb cccc cccc cccc cccc,	        b = BIC Bank code (first four alpha characters), c = Branch code and account number(bank-specific format)
    //    * Slovakia,         // => 24,	20n,	    SKkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    * Slovenia,         // => 19,	15n,	    SIkk bbss sccc cccc cxx,	            k = IBAN check digits (always = "56"), b = National bank code, s = Branch code, c = Account number, x = National check digits
    //    * Spain,            // => 24,	20n,	    ESkk bbbb ssss xxcc cccc cccc,	        b = National bank code, s = Branch code, x = Check digits, c = Account number
    //    * Sweden,           // => 24,	20n,	    SEkk bbbc cccc cccc cccc cccc,	        b = National bank code, c = Account number
    //    * UnitedKingdom     // => 22,	4a,14n,	    GBkk bbbb ssss sscc cccc cc,	        b = BIC bank code, s = Bank and branch code(sort code), c = Account number


    /// <summary>
    ///     used as complete model of all iban logic for each defined internal country <seealso cref="Rules"/>
    /// </summary>
    public class IbanModel
        : IIbanModel
    {

        // <country>    => 2 Letter ISO
        // <checksum>   => kk: 2 didgets
        // <bank>       => b: bank code
        // <account>    => c: account number
        // <ncheck>     => x: check didgets
        // <type>       => t: account type
        // <branch>     => s: branch code

        /// <summary>
        ///     used as in memory data source as all intenral business iban logic of the set TwoLetterISORegionName of countries <see cref="Rules"/>.
        /// </summary>
        public Dictionary<string, IbanRuleSetModel> Rules
        {
            get => new Dictionary<string, IbanRuleSetModel>
            {
                // => ATkk bbbb bccc cccc cccc
                { "AT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)AT)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{1})(?<account1>[0-9]{3})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2><account1> <account2> <account3>",
                        SanityFormat = "<bank1><bank2><account1><account2><account3><country><checksum>",
                        Example = "ATKK BBBB BNNN NNNN NNNN",
                        Length = 20,
                        Country = Countries.Austria
                    }
                },
                // => BEkk bbbc cccc ccxx
                { "BE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)BE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2})(?<whitespace>\s?)(?<ncheck>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank><account1> <account2> <account3><ncheck>",
                        SanityFormat = "<bank><account1><account2><account3><ncheck><country><checksum>",
                        Example = "BEKK BBBN NNNN NNXX",
                        Length = 16,
                        Country = Countries.Belgium
                    }
                },
                // => BGkk bbbb ssss ttcc cccc cc
                { "BG",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)BG)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<branch>[0-9]{4})(?<whitespace>\s?)(?<bban>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch> <bban><account1> <account2> <account3>",
                        SanityFormat = "<bank><branch><bban><account1><account2><account3><country><checksum>",
                        Example = "BGKK BBBB SSSS TTCC CCCC CC",
                        Length = 22,
                        Country = Countries.Bulgaria
                    }
                },
                // => HRkk bbbb bbbc cccc cccc c
                { "HR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)HR)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2><account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank1><bank2><account1><account2><account3><account4><country><checksum>",
                        Example = "HRKK BBBB BBBN NNNN NNNN N",
                        Length = 21,
                        Country = Countries.Croatia
                    }
                },
                // => CYkk bbbs ssss cccc cccc cccc cccc
                { "CY",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)CY)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<branch1>[0-9]{1})(?<whitespace>\s?)(?<branch2>[0-9]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank><branch1> <branch2> <account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><account4><country><checksum>",
                        Example = "CYKK BBBS SSSS CCCC CCCC CCCC CCCC",
                        Length = 28,
                        Country = Countries.Cyprus
                    }
                },
                // => CZkk bbbb ssss sscc cccc cccc
                { "CZ",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)CZ)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?$)",
                        DisplayFormat ="<country><checksum> <bank> <branch1> <branch2><account1> <account2> <account3>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><country><checksum>",
                        Example = "CZKK BBBB SSSS SSNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Czechia
                    }
                },
                // => DKkk bbbb cccc cccc cc
                { "DK",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)DK)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank> <account1> <account2> <account3>",
                        SanityFormat = "<bank><account1><account2><account3><country><checksum>",
                        Example = "DKKK BBBB NNNN NNNN NN",
                        Length = 18,
                        Country = Countries.Denmark
                    }
                },
                // => EEkk bbss cccc cccc cccx
				{ "EE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)EE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{2})(?<branch>[0-9]{2})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{3})(?<ncheck>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank><branch> <account1> <account2> <account3><ncheck>",
                        SanityFormat = "<bank><branch><account1><account2><account3><ncheck><country><checksum>",
                        Example = "EEKK BBSS NNNN NNNN NNNX",
                        Length = 20,
                        Country = Countries.Estonia
                    }
                },
				// => FIkk bbbb bbcc cccc cx
				{ "FI",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)FI)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{1})(?<ncheck>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2><account1> <account2> <account3><ncheck>",
                        SanityFormat = "<bank1><bank2><account1><account2><account3><ncheck><country><checksum>",
                        Example = "FIKK BBBB BBNN NNNN NX",
                        Length = 18,
                        Country = Countries.Finland
                    }
                },
                // => FRkk bbbb bsss sscc cccc cccc cxx
                { "FR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)FR)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{1})(?<branch1>[0-9]{3})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[a-zA-Z]{1})(?<ncheck>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2><branch1> <branch2><account1> <account2> <account3> <account4><ncheck>",
                        SanityFormat = "<bank1><bank2><branch1><branch2><account1><account2><account3><account4><ncheck><country><checksum>",
                        Example = "FRKK BBBB BSSS SSCC CCCC CCCC CXX",
                        Length = 27,
                        Country = Countries.France
                    }
                },
				// => DEkk bbbb bbbb cccc cccc cc
				{ "DE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)DE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2> <account1> <account2> <account3>",
                        SanityFormat = "<bank1><bank2><account1><account2><account3><country><checksum>",
                        Example = "DEKK BBBB BBBB NNNN NNNN NN",
                        Length = 22,
                        Country = Countries.Germany
                    }
                },				
                // => GRkk bbbs sssc cccc cccc cccc ccc
				{ "GR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)GR)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<branch1>[0-9]{1})(?<whitespace>\s?)(?<branch2>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4})(?<whitespace>\s?)(?<account5>[0-9]{3}$)",
                        DisplayFormat ="<country><checksum> <bank><branch1> <branch2><account1> <account2> <account3> <account4> <account5>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><account4><account5><country><checksum>",
                        Example = "GRKK BBBS SSSC CCCC CCCC CCCC CCC",
                        Length = 27,
                        Country = Countries.Greece
                    }
                },
                // => HUkk bbbs sssx cccc cccc cccc cccx
				{ "HU",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)HU)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<branch1>[0-9]{1})(?<whitespace>\s?)(?<branch2>[0-9]{3})(?<ncheck1>[0-9]{1})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{3})(?<ncheck2>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank><branch1> <branch2><ncheck1> <account1> <account2> <account3> <account4><ncheck2>",
                        SanityFormat = "<bank><branch1><branch2><ncheck1><account1><account2><account3><account4><ncheck2><country><checksum>",
                        Example = "HUKK BBBS SSSX NNNN NNNN NNNN NNNX",
                        Length = 28,
                        Country = Countries.Hungary
                    }
                },
                // => IEkk aaaa bbbb bbcc cccc cc
                { "IE",
                    //new IbanRuleSetModel
                    //{
                    //    RegexPattern = @"(?<country>^(?i)IE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                    //    DisplayFormat ="<country><checksum> <bank> <branch1> <branch2><account1> <account2> <account3>",
                    //    SanityFormat = "<bank><branch1><branch2><account1><account2><account3><country><checksum>",
                    //    Example = "IEKK BBBB SSSS SSCC CCCC CC",
                    //    Length = 22,
                    //    Country = Countries.Ireland
                    //}
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)IE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch1> <branch2><account1> <account2>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><country><checksum>",
                        Example = "IEKK BBBB SSSS SSCC CCCC",
                        Length = 22,
                        Country = Countries.Ireland
                    }
                },
                // => ITkk xbbb bbss sssc cccc cccc ccc
                { "IT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)IT)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<ncheck>[a-zA-Z]{1})(?<bank1>[0-9]{3})(?<whitespace>\s?)(?<bank2>[0-9]{2})(?<branch1>[0-9]{2})(?<whitespace>\s?)(?<branch2>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{3}$)",
                        DisplayFormat ="<country><checksum> <ncheck><bank1> <bank2><branch1> <branch2><account1> <account2> <account3> <account4>",
                        SanityFormat = "<ncheck><bank1><bank2><branch1><branch2><account1><account2><account3><account4><country><checksum>",
                        Example = "ITKK XBBB BBSS SSSC CCCC CCCC CCC",
                        Length = 27,
                        Country = Countries.Italy
                    }
                },
                // => LVkk bbbb cccc cccc cccc c
                { "LV",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)LV)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank> <account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank><account1><account2><account3><account4><country><checksum>",
                        Example = "LVKK BBBB CCCC CCCC CCCC C",
                        Length = 21,
                        Country = Countries.Latvia
                    }
                },
				// => LTkk bbbb bccc cccc cccc
				{ "LT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)LT)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank1>[0-9]{4})(?<whitespace>\s?)(?<bank2>[0-9]{1})(?<account1>[0-9]{3})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank1> <bank2><account1> <account2> <account3>",
                        SanityFormat = "<bank1><bank2><account1><account2><account3><country><checksum>",
                        Example = "LTKK BBBB BNNN NNNN NNNN",
                        Length = 20,
                        Country = Countries.Lithuania
                    }
                },
                // => LUkk bbbc cccc cccc cccc
                { "LU",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)LU)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank><account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank><account1><account2><account3><account4><country><checksum>",
                        Example = "LUKK BBBC CCCC CCCC CCCC",
                        Length = 20,
                        Country = Countries.Luxembourg
                    }
                },
                // => MTkk bbbb ssss sccc cccc cccc cccc ccc
                { "MT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)MT)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<branch2>[0-9]{1})(?<account1>[0-9]{3})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4})(?<whitespace>\s?)(?<account5>[0-9]{3}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch1> <branch2><account1> <account2> <account3> <account4> <account5>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><account4><account5><country><checksum>",
                        Example = "MTKK BBBB SSSS SCCC CCCC CCCC CCCC CCC",
                        Length = 31,
                        Country = Countries.Malta
                    }
                },
                // => NLkk bbbb cccc cccc cc
                { "NL",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)NL)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank> <account1> <account2> <account3>",
                        SanityFormat = "<bank><account1><account2><account3><country><checksum>",
                        Example = "NLKK BBBB NNNN NNNN NN",
                        Length = 18,
                        Country = Countries.Netherlands
                    }    
                },
                // => PLkk bbbs sssx cccc cccc cccc cccc
                { "PL",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)PL)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<branch1>[0-9]{1})(?<whitespace>\s?)(?<branch2>[0-9]{3})(?<ncheck>[0-9]{1})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank><branch1> <branch2><ncheck> <account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank><branch1><branch2><ncheck><account1><account2><account3><account4><country><checksum>",
                        Example = "PLKK BBBS SSSX NNNN NNNN NNNN NNNN",
                        Length = 28,
                        Country = Countries.Poland
                    }
                },
                // => PTkk bbbb ssss cccc cccc cccx x
                { "PT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)PT)(?<checksum>50)(?<whitespace>\s?)(?<bank>[0-9]{4})(?<whitespace>\s?)(?<branch>[0-9]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{3})(?<ncheck1>[0-9]{1})(?<whitespace>\s?)(?<ncheck2>[0-9]{1}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch> <account1> <account2> <account3><ncheck1> <ncheck2>",
                        SanityFormat = "<bank><branch><account1><account2><account3><ncheck1><ncheck2><country><checksum>",
                        Example = "PT50 BBBB SSSS NNNN NNNN NNNX X",
                        Length = 25,
                        Country = Countries.Portugal
                    }
                },
                // => ROkk bbbb cccc cccc cccc cccc
                { "RO",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)RO)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<account1>[0-9]{4})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4}$)",
                        DisplayFormat = "<country><checksum> <bank> <account1> <account2> <account3> <account4>",
                        SanityFormat = "<bank><account1><account2><account3><account4><country><checksum>",
                        Example = "ROKK BBBB CCCC CCCC CCCC CCCC",
                        Length = 24,
                        Country = Countries.Romania
                    }
                },
                // => SKkk bbbb ssss sscc cccc cccc
                { "SK",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)Sk)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4}$)",
                        DisplayFormat = "<country><checksum> <bank> <branch1> <branch2><account1> <account2> <account3>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><country><checksum>",
                        Example = "SKKK BBBB SSSS SSNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Slovakia
                    }
                },
                // => SIkk bbss sccc cccc cxx,
                { "SI",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)SI)(?<checksum>56)(?<whitespace>\s?)(?<bank>[0-9]{2})(?<branch1>[0-9]{2})(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{1})(?<ncheck>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank><branch1> <branch2><account1> <account2> <account3><ncheck>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><ncheck><country><checksum>",
                        Example = "SI56 BBSS SSNN NNNN NXX",
                        Length = 19,
                        Country = Countries.Slovenia
                    }
                },
                // => ESkk bbbb ssss xxcc cccc cccc
                { "ES",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)ES)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{4})(?<whitespace>\s?)(?<branch>[0-9]{4})(?<whitespace>\s?)(?<ncheck>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch> <ncheck><account1> <account2> <account3>",
                        SanityFormat = "<bank><branch><ncheck><account1><account2><account3><country><checksum>",
                        Example = "ESKK BBBB SSSS XXNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Spain
                    }
                },
                // => SEkk bbbc cccc cccc cccc cccc
                { "SE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)SE)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[0-9]{3})(?<account1>[0-9]{1})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{4})(?<whitespace>\s?)(?<account4>[0-9]{4})(?<whitespace>\s?)(?<account5>[0-9]{4}$)",
                        DisplayFormat ="<country><checksum> <bank><account1> <account2> <account3> <account4> <account5>",
                        SanityFormat = "<bank><account1><account2><account3><account4><account5><country><checksum>",
                        Example = "SEKK BBBN NNNN NNNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Sweden
                    }
                },
                // => GBkk bbbb ssss sscc cccc cc
                { "GB",
                    new IbanRuleSetModel
                    {
                        RegexPattern = @"(?<country>^(?i)GB)(?<checksum>[0-9]{2})(?<whitespace>\s?)(?<bank>[a-zA-Z]{4})(?<whitespace>\s?)(?<branch1>[0-9]{4})(?<whitespace>\s?)(?<whitespace>\s?)(?<branch2>[0-9]{2})(?<account1>[0-9]{2})(?<whitespace>\s?)(?<account2>[0-9]{4})(?<whitespace>\s?)(?<account3>[0-9]{2}$)",
                        DisplayFormat ="<country><checksum> <bank> <branch1> <branch2><account1> <account2> <account3>",
                        SanityFormat = "<bank><branch1><branch2><account1><account2><account3><country><checksum>",
                        Example = "GBKK BBBB SSSS SSNN NNNN NN",
                        Length = 22,
                        Country = Countries.UnitedKingdom
                    }
                }
            };
        }
    }
}
