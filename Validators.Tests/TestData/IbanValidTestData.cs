using System.Collections;
using System.Collections.Generic;

using Validators.Interfaces;
using Validators.Tests.TestModels;


namespace Validators.Tests.TestData
{
    public class IbanValidTestData
        : IEnumerable<object[]>
    {


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
                    CheckDidgets = 37,
                    Example = "ATKK BBBB BNNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "32195",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "33447157523"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "BE83 5483 1874 9715",
                    Country = Countries.Belgium,
                    CheckDidgets = 83,
                    Example = "BEKK BBBN NNNN NNXX",
                    ErrorMessage = null,
                    NationalBankCode = "548",
                    NationalBranchCode = null,
                    NationalCheckDidget = 15,
                    AccountNumber = "3187497"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "HR55 2484 0084 6518 3417 9",
                    Country = Countries.Croatia,
                    CheckDidgets = 55,
                    Example = "HRKK BBBB BBBN NNNN NNNN N",
                    ErrorMessage = null,
                    NationalBankCode = "2484008",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "4651834179"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "CZ65 5051 8353 7399 3389 7417",
                    Country = Countries.Czechia,
                    CheckDidgets = 65,
                    Example = "CZKK BBBB SSSS SSNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "5051",
                    NationalBranchCode = "835373",
                    NationalCheckDidget = null,
                    AccountNumber = "9933897417"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "DK65 5051 8748 7637 52",
                    Country = Countries.Denmark,
                    CheckDidgets = 65,
                    Example = "DKKK BBBB NNNN NNNN NN",
                    ErrorMessage = null,
                    NationalBankCode = "5051",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "8748763752"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "EE97 1275 4713 6579 3179",
                    Country = Countries.Estonia,
                    CheckDidgets = 97,
                    Example = "EEKK BBSS NNNN NNNN NNNX",
                    ErrorMessage = null,
                    NationalBankCode = "12",
                    NationalBranchCode = "75",
                    NationalCheckDidget = 9,
                    AccountNumber = "47136579317"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "FI78 4253 5591 1558 75",
                    Country = Countries.Finland,
                    CheckDidgets = 78,
                    Example = "FIKK BBBB BBNN NNNN NX",
                    ErrorMessage = null,
                    NationalBankCode = "425355",
                    NationalBranchCode = null,
                    NationalCheckDidget = 5,
                    AccountNumber = "9115587"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "DE48 5001 0517 9774 1229 48",
                    Country = Countries.Germany,
                    CheckDidgets = 48,
                    Example = "DEKK BBBB BBBB NNNN NNNN NN",
                    ErrorMessage = null,
                    NationalBankCode = "50010517",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "9774122948"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "HU12 1070 0024 6863 1528 5668 4732",
                    Country = Countries.Hungary,
                    CheckDidgets = 12,
                    Example = "HUKK BBBS SSSX NNNN NNNN NNNN NNNX",
                    ErrorMessage = null,
                    NationalBankCode = "107",
                    NationalBranchCode = "0002",
                    NationalCheckDidget = 42,
                    AccountNumber = "686315285668473"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "LT22 6285 8538 7987 6641",
                    Country = Countries.Lithuania,
                    CheckDidgets = 22,
                    Example = "LTKK BBBB BNNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "62858",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "53879876641"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "NL22 ABNA 6034 8378 98",
                    Country = Countries.Netherlands,
                    CheckDidgets = 22,
                    Example = "NLKK BBBB NNNN NNNN NN",
                    ErrorMessage = null,
                    NationalBankCode = "ABNA",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "6034837898"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "PL50 1090 2402 3828 6354 4736 6724",
                    Country = Countries.Poland,
                    CheckDidgets = 50,
                    Example = "PLKK BBBS SSSX NNNN NNNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "109",
                    NationalBranchCode = "0240",
                    NationalCheckDidget = 2,
                    AccountNumber = "3828635447366724"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "PT50 0035 0651 2159 7249 4481 3",
                    Country = Countries.Portugal,
                    CheckDidgets = 50,
                    Example = "PT50 BBBB SSSS NNNN NNNN NNNX X",
                    ErrorMessage = null,
                    NationalBankCode = "0035",
                    NationalBranchCode = "0651",
                    NationalCheckDidget = 13,
                    AccountNumber = "21597249448"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SK28 7742 4513 1288 6162 8345",
                    Country = Countries.Slovakia,
                    CheckDidgets = 28,
                    Example = "SKKK BBBB SSSS SSNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "7742",
                    NationalBranchCode = "451312",
                    NationalCheckDidget = null,
                    AccountNumber = "8861628345"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SI56 8339 1723 4746 131",
                    Country = Countries.Slovenia,
                    CheckDidgets = 56,
                    Example = "SI56 BBSS SSNN NNNN NXX",
                    ErrorMessage = null,
                    NationalBankCode = "83",
                    NationalBranchCode = "3917",
                    NationalCheckDidget = 31,
                    AccountNumber = "2347461"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "ES44 0075 3585 8163 4877 4767",
                    Country = Countries.Spain,
                    CheckDidgets = 44,
                    Example = "ESKK BBBB SSSS XXNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "0075",
                    NationalBranchCode = "3585",
                    NationalCheckDidget = 81,
                    AccountNumber = "6348774767"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "SE44 8755 6616 3143 4143 2154",
                    Country = Countries.Sweden,
                    CheckDidgets = 44,
                    Example = "SEKK BBBN NNNN NNNN NNNN NNNN",
                    ErrorMessage = null,
                    NationalBankCode = "875",
                    NationalBranchCode = null,
                    NationalCheckDidget = null,
                    AccountNumber = "56616314341432154"
                }
            },
            new object[]
            {
                new IbanTestModel
                {
                    Value = "GB20 BARC 2003 1816 5285 58",
                    Country = Countries.UnitedKingdom,
                    CheckDidgets = 20,
                    Example = "GBKK BBBB SSSS SSNN NNNN NN",
                    ErrorMessage = null,
                    NationalBankCode = "BARC",
                    NationalBranchCode = "200318",
                    NationalCheckDidget = null,
                    AccountNumber = "16528558"
                }
            }
        };


        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}