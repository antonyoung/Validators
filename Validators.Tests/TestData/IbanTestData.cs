using System.Collections;
using System.Collections.Generic;


namespace Validators.Tests.TestData
{
    public class IbanTestData
        : IEnumerable<object[]>
    {

        private readonly IEnumerable<object[]> _data = new List<object[]>()
        {
            new object[] {"AT37 3219 5334 4715 7523" },
            new object[] {"BE83 5483 1874 9715" },
            new object[] {"HR55 2484 0084 6518 3417 9" },
            new object[] {"CZ65 5051 8353 7399 3389 7417" },
            new object[] {"DK65 5051 8748 7637 52" },
            new object[] {"EE97 1275 4713 6579 3179" },
            new object[] {"FI78 4253 5591 1558 75" },
            new object[] {"DE48 5001 0517 9774 1229 48" },
            new object[] {"HU12 1070 0024 6863 1528 5668 4732" },
            new object[] {"LT22 6285 8538 7987 6641" },
            new object[] {"NL22 ABNA 6034 8378 98" },
            new object[] {"PL50 1090 2402 3828 6354 4736 6724" },
            new object[] {"PT50 0035 0651 2159 7249 4481 3" },
            new object[] {"SK28 7742 4513 1288 6162 8345" },
            new object[] {"SI56 8339 1723 4746 131" },
            new object[] {"ES44 0075 3585 8163 4877 4767" },
            new object[] {"SE44 8755 6616 3143 4143 2154" },
            new object[] {"GB20 BARC 2003 1816 5285 58" }
        };


        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
