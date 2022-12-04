using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Iban.Constants;
using AntonYoung.Validators.Iban.Infrastructure;
using System.Collections.Generic;

namespace AntonYoung.Validators.Iban.Models
{
    // DATA: based on https://en.wikipedia.org/wiki/International_Bank_Account_Number

    //    Amsterdam,        //=> argument exception
    //    * Austria,          //=> 20,	16n,	    ATkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    * Belgium,          //=> 16,	12n,	    BEkk bbbc cccc ccxx,	                b = National bank code, c = Account number, x = National check digits
    //    * Bulgaria,         //=> 22,	4a,6n,8c,   BGkk bbbb ssss ttcc cccc cc,	        b = BIC bank code, s = Branch(BAE) number, t = Account type, c = Account number
    //    * Croatia,          //=> 21,	17n,	    HRkk bbbb bbbc cccc cccc c, 	        b = Bank code, c = Account number
    //    * Cyprus,           //=> 28,	8n,16c,	    CYkk bbbs ssss cccc cccc cccc cccc,	    b = National bank code, s = Branch code, c = Account number
    //    * Czechia,          //=> 24,	20n,	    CZkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    * Denmark,          //=> 18,	14n,	    DKkk bbbb cccc cccc cc,	                b = National bank code, c = Account number
    //    * Estonia,          //=> 20,	16n,	    EEkk bbss cccc cccc cccx,	            b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    * Finland,          //=> 18,	14n,	    FIkk bbbb bbcc cccc cx,	                b = Bank and branch code, c = Account number, x = National check digit
    //    France,           //=> 27,	10n,11c,2n,	FRkk bbbb bsss sscc cccc cccc cxx,	    b = National bank code, s = Branch code(fr:code guichet), c = Account number, x = National check digits(fr:clé RIB)
    //    * Germany,          //=> 22,	18n,	    DEkk bbbb bbbb cccc cccc cc,	        b = Bank and branch identifier (de:Bankleitzahl or BLZ), c = Account number
    //    * Greece,           //=> 27,	7n,16c,	    GRkk bbbs sssc cccc cccc cccc ccc,	    b = National bank code, s = Branch code, c = Account number
    //    * Hungary,          //=> 28,	24n,	    HUkk bbbs sssx cccc cccc cccc cccx,	    b = National bank code, s = Branch code, c = Account number, x = National check digit
    //    Ireland,          //=> 22,	4c,14n,	    IEkk aaaa bbbb bbcc cccc cc,	        a = BIC bank code, b = Bank/branch code(sort code), c = Account number
    //    Italy,            //=> 27,	1a,10n,12c,	ITkk xbbb bbss sssc cccc cccc ccc,	    x = Check char (CIN), b = National bank code(Associazione Bancaria Italiana or Codice ABI), s = Branch code(it:Coordinate bancarie or CAB – Codice d'Avviamento Bancario), c = Account number
    //    * Latvia,           //=> 21,	4a,13c,	    LVkk bbbb cccc cccc cccc c,	            b = BIC Bank code, c = Account number
    //    * Lithuania,        //=> 20,	16n,	    LTkk bbbb bccc cccc cccc,	            b = National bank code, c = Account number
    //    * Luxembourg,       //=> 20,	3n,13c,	    LUkk bbbc cccc cccc cccc,	            b = National bank code, c = Account number
    //    * Malta,            //=> 31,	4a,5n,18c,	MTkk bbbb ssss sccc cccc cccc cccc ccc,	b = BIC bank code, s = Branch code, c = Account number
    //    * Netherlands,      //=> 18,	4a,10n,	    NLkk bbbb cccc cccc cc,	                b = BIC Bank code, c = Account number
    //    * Poland,           //=> 28,	24n,	    PLkk bbbs sssx cccc cccc cccc cccc,	    b = National bank code, s = Branch code, x = National check digit, c = Account number,
    //    * Portugal,         //=> 25,	21n,	    PTkk bbbb ssss cccc cccc cccx x,	    k = IBAN check digits (always = "50"), b = National bank code(numeric only), s = Branch code(numeric only), c = Account number(numeric only), x = National check digits(numeric only)
    //    * Romania,          //=> 24,	4a,16c,	    ROkk bbbb cccc cccc cccc cccc,	        b = BIC Bank code (first four alpha characters), c = Branch code and account number(bank-specific format)
    //    * Slovakia,         //=> 24,	20n,	    SKkk bbbb ssss sscc cccc cccc,	        b = National bank code, s = Account number prefix, c = Account number
    //    * Slovenia,         //=> 19,	15n,	    SIkk bbss sccc cccc cxx,	            k = IBAN check digits (always = "56"), b = National bank code, s = Branch code, c = Account number, x = National check digits
    //    * Spain,            //=> 24,	20n,	    ESkk bbbb ssss xxcc cccc cccc,	        b = National bank code, s = Branch code, x = Check digits, c = Account number
    //    * Sweden,           //=> 24,	20n,	    SEkk bbbc cccc cccc cccc cccc,	        b = National bank code, c = Account number
    //    * UnitedKingdom     //=> 22,	4a,14n,	    GBkk bbbb ssss sscc cccc cc,	        b = BIC bank code, s = Bank and branch code(sort code), c = Account number


    /// <summary>
    ///     used as complete model of all iban logic for each defined internal country <seealso cref="Rules"/>
    /// </summary>
    internal class IbanModel
        : IIbanModel
    {
        private readonly string OptionalWhiteSpace = $"(?<{GroupNames.WhiteSpace}>\\s?)";
        
        /// <summary>
        ///     used as in memory data source as all intenral business iban logic of the set TwoLetterISORegionName of countries <see cref="Rules"/>.
        /// </summary>
        public IDictionary<string, IbanRuleSetModel> Rules
        {
            get => new Dictionary<string, IbanRuleSetModel>
            {
                //=> ATkk bbbb bccc cccc cccc
                { "AT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)AT)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{1}})(?<{GroupNames.Account}1>[0-9]{{3}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "ATKK BBBB BNNN NNNN NNNN",
                        Length = 20,
                        Country = Countries.Austria
                    }
                },
                //=> BEkk bbbc cccc ccxx
                { "BE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)BE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.NationalCheckDigit}>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3><{GroupNames.NationalCheckDigit}>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.NationalCheckDigit}><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "BEKK BBBN NNNN NNXX",
                        Length = 16,
                        Country = Countries.Belgium
                    }
                },
                //=> BGkk bbbb ssss ttcc cccc cc
                { "BG",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)BG)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.AccountType}>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}> <{GroupNames.AccountType}><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}><{GroupNames.AccountType}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "BGKK BBBB SSSS TTCC CCCC CC",
                        Length = 22,
                        Country = Countries.Bulgaria
                    }
                },
                //=> HRkk bbbb bbbc cccc cccc c
                { "HR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)HR)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "HRKK BBBB BBBN NNNN NNNN N",
                        Length = 21,
                        Country = Countries.Croatia
                    }
                },
                //=> CYkk bbbs ssss cccc cccc cccc cccc
                { "CY",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)CY)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Branch}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}1> <{GroupNames.Branch}2> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "CYKK BBBS SSSS CCCC CCCC CCCC CCCC",
                        Length = 28,
                        Country = Countries.Cyprus
                    }
                },
                //=> CZkk bbbb ssss sscc cccc cccc
                { "CZ",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)CZ)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><checksum> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "CZKK BBBB SSSS SSNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Czechia
                    }
                },
                //=> DKkk bbbb cccc cccc cc
                { "DK",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)DK)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "DKKK BBBB NNNN NNNN NN",
                        Length = 18,
                        Country = Countries.Denmark
                    }
                },
                //=> EEkk bbss cccc cccc cccx
				{ "EE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)EE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{2}})(?<{GroupNames.Branch}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{3}})(?<ncheck>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3><ncheck>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><ncheck><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "EEKK BBSS NNNN NNNN NNNX",
                        Length = 20,
                        Country = Countries.Estonia
                    }
                },
				//=> FIkk bbbb bbcc cccc cx
				{ "FI",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)FI)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{1}})(?<ncheck>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3><ncheck>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><ncheck><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "FIKK BBBB BBNN NNNN NX",
                        Length = 18,
                        Country = Countries.Finland
                    }
                },
                //=> FRkk bbbb bsss sscc cccc cccc cxx
                { "FR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)FR)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{1}})(?<{GroupNames.Branch}1>[0-9]{{3}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[a-zA-Z]{{1}})(?<{GroupNames.NationalCheckDigit}>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4><ncheck>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><ncheck><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "FRKK BBBB BSSS SSCC CCCC CCCC CXX",
                        Length = 27,
                        Country = Countries.France
                    }
                },
				//=> DEkk bbbb bbbb cccc cccc cc
				{ "DE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)DE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "DEKK BBBB BBBB NNNN NNNN NN",
                        Length = 22,
                        Country = Countries.Germany
                    }
                },				
                //=> GRkk bbbs sssc cccc cccc cccc ccc
				{ "GR",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)GR)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Branch}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}5>[0-9]{{3}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4> <{GroupNames.Account}5>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Account}5><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "GRKK BBBS SSSC CCCC CCCC CCCC CCC",
                        Length = 27,
                        Country = Countries.Greece
                    }
                },
                //=> HUkk bbbs sssx cccc cccc cccc cccx
				{ "HU",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)HU)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Branch}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{3}})(?<ncheck1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{3}})(?<ncheck2>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}1> <{GroupNames.Branch}2><ncheck1> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4><ncheck2>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><ncheck1><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><ncheck2><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "HUKK BBBS SSSX NNNN NNNN NNNN NNNX",
                        Length = 28,
                        Country = Countries.Hungary
                    }
                },
                //=> IEkk aaaa bbbb bbcc cccc cc
                { "IE",
                    //new IbanRuleSetModel
                    //{
                    //    RegexPattern = $"(?<{GroupNames.Country}>^(?i)IE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                    //    DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                    //    SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                    //    Example = "IEKK BBBB SSSS SSCC CCCC CC",
                    //    Length = 22,
                    //    Country = Countries.Ireland
                    //}
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)IE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "IEKK BBBB SSSS SSCC CCCC",
                        Length = 22,
                        Country = Countries.Ireland
                    }
                },
                //=> ITkk xbbb bbss sssc cccc cccc ccc
                { "IT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)IT)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<ncheck>[a-zA-Z]{{1}})(?<{GroupNames.Bank}1>[0-9]{{3}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{2}})(?<{GroupNames.Branch}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{3}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <ncheck><{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<ncheck><{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "ITKK XBBB BBSS SSSC CCCC CCCC CCC",
                        Length = 27,
                        Country = Countries.Italy
                    }
                },
                //=> LVkk bbbb cccc cccc cccc c
                { "LV",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)LV)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "LVKK BBBB CCCC CCCC CCCC C",
                        Length = 21,
                        Country = Countries.Latvia
                    }
                },
				//=> LTkk bbbb bccc cccc cccc
				{ "LT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)LT)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Bank}2>[0-9]{{1}})(?<{GroupNames.Account}1>[0-9]{{3}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}1> <{GroupNames.Bank}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}1><{GroupNames.Bank}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "LTKK BBBB BNNN NNNN NNNN",
                        Length = 20,
                        Country = Countries.Lithuania
                    }
                },
                //=> LUkk bbbc cccc cccc cccc
                { "LU",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)LU)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "LUKK BBBC CCCC CCCC CCCC",
                        Length = 20,
                        Country = Countries.Luxembourg
                    }
                },
                //=> MTkk bbbb ssss sccc cccc cccc cccc ccc
                { "MT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)MT)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{1}})(?<{GroupNames.Account}1>[0-9]{{3}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}5>[0-9]{{3}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4> <{GroupNames.Account}5>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Account}5><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "MTKK BBBB SSSS SCCC CCCC CCCC CCCC CCC",
                        Length = 31,
                        Country = Countries.Malta
                    }
                },
                //=> NLkk bbbb cccc cccc cc
                { "NL",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)NL)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "NLKK BBBB NNNN NNNN NN",
                        Length = 18,
                        Country = Countries.Netherlands
                    }    
                },
                //=> PLkk bbbs sssx cccc cccc cccc cccc
                { "PL",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)PL)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Branch}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{3}})(?<ncheck>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}1> <{GroupNames.Branch}2><ncheck> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><ncheck><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "PLKK BBBS SSSX NNNN NNNN NNNN NNNN",
                        Length = 28,
                        Country = Countries.Poland
                    }
                },
                //=> PTkk bbbb ssss cccc cccc cccx x
                { "PT",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)PT)(?<{GroupNames.CheckDigits}>50){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{3}})(?<ncheck1>[0-9]{{1}}){OptionalWhiteSpace}(?<ncheck2>[0-9]{{1}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3><ncheck1> <ncheck2>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><ncheck1><ncheck2><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "PT50 BBBB SSSS NNNN NNNN NNNX X",
                        Length = 25,
                        Country = Countries.Portugal
                    }
                },
                //=> ROkk bbbb cccc cccc cccc cccc
                { "RO",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)RO)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "ROKK BBBB CCCC CCCC CCCC CCCC",
                        Length = 24,
                        Country = Countries.Romania
                    }
                },
                //=> SKkk bbbb ssss sscc cccc cccc
                { "SK",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)Sk)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "SKKK BBBB SSSS SSNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Slovakia
                    }
                },
                //=> SIkk bbss sccc cccc cxx,
                { "SI",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)SI)(?<{GroupNames.CheckDigits}>56){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{2}})(?<{GroupNames.Branch}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{1}})(?<ncheck>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3><ncheck>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><ncheck><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "SI56 BBSS SSNN NNNN NXX",
                        Length = 19,
                        Country = Countries.Slovenia
                    }
                },
                //=> ESkk bbbb ssss xxcc cccc cccc
                { "ES",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)ES)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}>[0-9]{{4}}){OptionalWhiteSpace}(?<ncheck>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}> <ncheck><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}><ncheck><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "ESKK BBBB SSSS XXNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Spain
                    }
                },
                //=> SEkk bbbc cccc cccc cccc cccc
                { "SE",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)SE)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[0-9]{{3}})(?<{GroupNames.Account}1>[0-9]{{1}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}4>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}5>[0-9]{{4}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3> <{GroupNames.Account}4> <{GroupNames.Account}5>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Account}4><{GroupNames.Account}5><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "SEKK BBBN NNNN NNNN NNNN NNNN",
                        Length = 24,
                        Country = Countries.Sweden
                    }
                },
                //=> GBkk bbbb ssss sscc cccc cc
                { "GB",
                    new IbanRuleSetModel
                    {
                        RegexPattern = $"(?<{GroupNames.Country}>^(?i)GB)(?<{GroupNames.CheckDigits}>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Bank}>[a-zA-Z]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Branch}1>[0-9]{{4}}){OptionalWhiteSpace}{OptionalWhiteSpace}(?<{GroupNames.Branch}2>[0-9]{{2}})(?<{GroupNames.Account}1>[0-9]{{2}}){OptionalWhiteSpace}(?<{GroupNames.Account}2>[0-9]{{4}}){OptionalWhiteSpace}(?<{GroupNames.Account}3>[0-9]{{2}}$)",
                        DisplayFormat = $"<{GroupNames.Country}><{GroupNames.CheckDigits}> <{GroupNames.Bank}> <{GroupNames.Branch}1> <{GroupNames.Branch}2><{GroupNames.Account}1> <{GroupNames.Account}2> <{GroupNames.Account}3>",
                        SanityFormat = $"<{GroupNames.Bank}><{GroupNames.Branch}1><{GroupNames.Branch}2><{GroupNames.Account}1><{GroupNames.Account}2><{GroupNames.Account}3><{GroupNames.Country}><{GroupNames.CheckDigits}>",
                        Example = "GBKK BBBB SSSS SSNN NNNN NN",
                        Length = 22,
                        Country = Countries.UnitedKingdom
                    }
                }
            };
        }
    }
}