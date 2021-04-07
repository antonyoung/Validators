using System.Collections;
using System.Collections.Generic;
using Validators.Abstractions.Enums;
using Validators.Iban.Tests.TestModels;

namespace Validators.Iban.Tests.TestData
{
    public class IbanValidTestData
        : IEnumerable<object[]>
    {
        //  todo: add following test ibans
        //  * BG77 STSA 9300 1398 8332 79
        //  * CY61 9441 1634 1695 6269 2359 9979
        //  FR94 1273 9000 3033 3857 4342 N27
        //  * GR52 0179 7165 4546 6367 7356 532
        //  * IE44 BOFI 9000 1739 2177
        //  IT11 M030 0203 2808 5811 3313 922
        //  * LV80 BANK 0000 4351 9500 1
        //  * LU89 0105 6393 2177 8274
        //  * MT46 OAVL 2242 3413 9526 3277 8597 695
        //  * RO04 RZBR 5898 6478 5349 8572

        /// <summary>
        ///     used as test data set for valid iban accounts, extend as you wish.
        /// </summary>
        private readonly IEnumerable<object[]> _data = new List<object[]>
        {
            new object[]
            {
                new IbanTestModel
                {
                    Value = "AT37 3219 5334 4715 7523",
                    Country = Countries.Austria,
                    CheckDigits = 37,
                    Example = "ATKK BBBB BNNN NNNN NNNN",
                    NationalBankCode = "32195",
                    AccountNumber = "33447157523"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "BE83 5483 1874 9715",
                    Country = Countries.Belgium,
                    CheckDigits = 83,
                    Example = "BEKK BBBN NNNN NNXX",
                    NationalBankCode = "548",
                    NationalCheckDigit = 15,
                    AccountNumber = "3187497"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "BG77 STSA 9300 1398 8332 79",
                    Country = Countries.Bulgaria,
                    CheckDigits = 77,
                    Example = "BGKK BBBB SSSS TTCC CCCC CC",
                    NationalBankCode = "STSA",
                    NationalBranchCode = "9300",
                    AccountNumber = "98833279",
                    AccountType = 13
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "HR55 2484 0084 6518 3417 9",
                    Country = Countries.Croatia,
                    CheckDigits = 55,
                    Example = "HRKK BBBB BBBN NNNN NNNN N",
                    NationalBankCode = "2484008",
                    AccountNumber = "4651834179"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "CY61 9441 1634 1695 6269 2359 9979",
                    Country = Countries.Cyprus,
                    CheckDigits = 61,
                    Example = "CYKK BBBS SSSS CCCC CCCC CCCC CCCC",
                    NationalBankCode = "944",
                    NationalBranchCode = "11634",
                    AccountNumber = "1695626923599979"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "CZ65 5051 8353 7399 3389 7417",
                    Country = Countries.Czechia,
                    CheckDigits = 65,
                    Example = "CZKK BBBB SSSS SSNN NNNN NNNN",
                    NationalBankCode = "5051",
                    NationalBranchCode = "835373",
                    AccountNumber = "9933897417"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "DK65 5051 8748 7637 52",
                    Country = Countries.Denmark,
                    CheckDigits = 65,
                    Example = "DKKK BBBB NNNN NNNN NN",
                    NationalBankCode = "5051",
                    AccountNumber = "8748763752"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "EE97 1275 4713 6579 3179",
                    Country = Countries.Estonia,
                    CheckDigits = 97,
                    Example = "EEKK BBSS NNNN NNNN NNNX",
                    NationalBankCode = "12",
                    NationalBranchCode = "75",
                    NationalCheckDigit = 9,
                    AccountNumber = "47136579317"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "FI78 4253 5591 1558 75",
                    Country = Countries.Finland,
                    CheckDigits = 78,
                    Example = "FIKK BBBB BBNN NNNN NX",
                    NationalBankCode = "425355",
                    NationalCheckDigit = 5,
                    AccountNumber = "9115587"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "FR94 1273 9000 3033 3857 4342 N27",
                    Country = Countries.France,
                    CheckDigits = 94,
                    Example = "FRKK BBBB BSSS SSCC CCCC CCCC CXX",
                    NationalBankCode = "12739",
                    NationalBranchCode = "00030",
                    NationalCheckDigit = 27,
                    AccountNumber = "3338574342N"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "DE48 5001 0517 9774 1229 48",
                    Country = Countries.Germany,
                    CheckDigits = 48,
                    Example = "DEKK BBBB BBBB NNNN NNNN NN",
                    NationalBankCode = "50010517",
                    AccountNumber = "9774122948"
                }
            },
           new object[]
            {
                new IbanTestModel
                {
                    Value = "GR52 0179 7165 4546 6367 7356 532",
                    Country = Countries.Greece,
                    CheckDigits = 52,
                    Example = "GRKK BBBS SSSC CCCC CCCC CCCC CCC",
                    NationalBankCode = "017",
                    NationalBranchCode = "9716",
                    AccountNumber = "5454663677356532"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "HU12 1070 0024 6863 1528 5668 4732",
                    Country = Countries.Hungary,
                    CheckDigits = 12,
                    Example = "HUKK BBBS SSSX NNNN NNNN NNNN NNNX",
                    NationalBankCode = "107",
                    NationalBranchCode = "0002",
                    NationalCheckDigit = 42,
                    AccountNumber = "686315285668473"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "IE44 BOFI 9000 1739 2177",
                    Country = Countries.Ireland,
                    CheckDigits = 44,
                    Example = "IEKK BBBB SSSS SSCC CCCC",
                    NationalBankCode = "BOFI",
                    NationalBranchCode = "900017",
                    AccountNumber = "392177"
                }
            },
            //    * Italy,            //=> 27,	1a,10n,12c,	ITkk xbbb bbss sssc cccc cccc ccc,	    x = Check char (CIN), b = National bank code(Associazione Bancaria Italiana or Codice ABI), s = Branch code(it:Coordinate bancarie or CAB – Codice d'Avviamento Bancario), c = Account number
            new object[]
            {
                new IbanTestModel
                {
                    Value = "IT11 M030 0203 2808 5811 3313 922",
                    Country = Countries.Italy,
                    CheckDigits = 11,
                    Example = "ITKK XBBB BBSS SSSC CCCC CCCC CCC",
                    NationalBankCode = "03002",
                    NationalBranchCode = "03280",
                    //=> NationalCheckDigit = "M",  feckin' Italians using a check char. instead of a digit!
                    AccountNumber = "858113313922"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "LV80 BANK 0000 4351 9500 1",
                    Country = Countries.Latvia,
                    CheckDigits = 80,
                    Example = "LVKK BBBB CCCC CCCC CCCC C",
                    NationalBankCode = "BANK",
                    AccountNumber = "0000435195001"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "LT22 6285 8538 7987 6641",
                    Country = Countries.Lithuania,
                    CheckDigits = 22,
                    Example = "LTKK BBBB BNNN NNNN NNNN",
                    NationalBankCode = "62858",
                    AccountNumber = "53879876641"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "LU89 0105 6393 2177 8274",
                    Country = Countries.Luxembourg,
                    CheckDigits = 89,
                    Example = "LUKK BBBC CCCC CCCC CCCC",
                    NationalBankCode = "010",
                    AccountNumber = "5639321778274"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "MT46 OAVL 2242 3413 9526 3277 8597 695",
                    Country = Countries.Malta,
                    CheckDigits = 46,
                    Example = "MTKK BBBB SSSS SCCC CCCC CCCC CCCC CCC",
                    NationalBankCode = "OAVL",
                    NationalBranchCode = "22423",
                    AccountNumber = "413952632778597695"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "NL22 ABNA 6034 8378 98",
                    Country = Countries.Netherlands,
                    CheckDigits = 22,
                    Example = "NLKK BBBB NNNN NNNN NN",
                    NationalBankCode = "ABNA",
                    AccountNumber = "6034837898"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "PL50 1090 2402 3828 6354 4736 6724",
                    Country = Countries.Poland,
                    CheckDigits = 50,
                    Example = "PLKK BBBS SSSX NNNN NNNN NNNN NNNN",
                    NationalBankCode = "109",
                    NationalBranchCode = "0240",
                    NationalCheckDigit = 2,
                    AccountNumber = "3828635447366724"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "PT50 0035 0651 2159 7249 4481 3",
                    Country = Countries.Portugal,
                    CheckDigits = 50,
                    Example = "PT50 BBBB SSSS NNNN NNNN NNNX X",
                    NationalBankCode = "0035",
                    NationalBranchCode = "0651",
                    NationalCheckDigit = 13,
                    AccountNumber = "21597249448"
                }
            },
             new object[]
            {
                new IbanTestModel
                {
                    Value = "RO04 RZBR 5898 6478 5349 8572",
                    Country = Countries.Romania,
                    CheckDigits = 04,
                    Example = "ROKK BBBB CCCC CCCC CCCC CCCC",
                    NationalBankCode = "RZBR",
                    AccountNumber = "5898647853498572"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SK28 7742 4513 1288 6162 8345",
                    Country = Countries.Slovakia,
                    CheckDigits = 28,
                    Example = "SKKK BBBB SSSS SSNN NNNN NNNN",
                    NationalBankCode = "7742",
                    NationalBranchCode = "451312",
                    AccountNumber = "8861628345"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SI56 8339 1723 4746 131",
                    Country = Countries.Slovenia,
                    CheckDigits = 56,
                    Example = "SI56 BBSS SSNN NNNN NXX",
                    NationalBankCode = "83",
                    NationalBranchCode = "3917",
                    NationalCheckDigit = 31,
                    AccountNumber = "2347461"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "ES44 0075 3585 8163 4877 4767",
                    Country = Countries.Spain,
                    CheckDigits = 44,
                    Example = "ESKK BBBB SSSS XXNN NNNN NNNN",
                    NationalBankCode = "0075",
                    NationalBranchCode = "3585",
                    NationalCheckDigit = 81,
                    AccountNumber = "6348774767"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SE44 8755 6616 3143 4143 2154",
                    Country = Countries.Sweden,
                    CheckDigits = 44,
                    Example = "SEKK BBBN NNNN NNNN NNNN NNNN",
                    NationalBankCode = "875",
                    AccountNumber = "56616314341432154"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "GB20 BARC 2003 1816 5285 58",
                    Country = Countries.UnitedKingdom,
                    CheckDigits = 20,
                    Example = "GBKK BBBB SSSS SSNN NNNN NN",
                    NationalBankCode = "BARC",
                    NationalBranchCode = "200318",
                    AccountNumber = "16528558"
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}