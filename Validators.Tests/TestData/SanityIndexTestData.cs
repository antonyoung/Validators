using System.Collections;
using System.Collections.Generic;


namespace Validators.Tests.TestData
{
    public class SanityIndexTestData
        : IEnumerable<object[]>
    {

        private readonly IEnumerable<object[]> _data = new List<object[]>
        {
            new object[] { new char[1] { '0' }, 0 },
            new object[] { new char[1] { '1' }, 1 },
            new object[] { new char[1] { '2' }, 2 },
            new object[] { new char[1] { '3' }, 3 },
            new object[] { new char[1] { '4' }, 4 },
            new object[] { new char[1] { '5' }, 5 },
            new object[] { new char[1] { '6' }, 6 },
            new object[] { new char[1] { '7' }, 7 },
            new object[] { new char[1] { '8' }, 8 },
            new object[] { new char[1] { '9' }, 9 },
            new object[] { new char[1] { 'A' }, 10 },
            new object[] { new char[1] { 'B' }, 11 },
            new object[] { new char[1] { 'C' }, 12 },
            new object[] { new char[1] { 'D' }, 13 },
            new object[] { new char[1] { 'E' }, 14 },
            new object[] { new char[1] { 'F' }, 15 },
            new object[] { new char[1] { 'G' }, 16 },
            new object[] { new char[1] { 'H' }, 17 },
            new object[] { new char[1] { 'I' }, 18 },
            new object[] { new char[1] { 'J' }, 19 },
            new object[] { new char[1] { 'K' }, 20 },
            new object[] { new char[1] { 'L' }, 21 },
            new object[] { new char[1] { 'M' }, 22 },
            new object[] { new char[1] { 'N' }, 23 },
            new object[] { new char[1] { 'O' }, 24 },
            new object[] { new char[1] { 'P' }, 25 },
            new object[] { new char[1] { 'Q' }, 26 },
            new object[] { new char[1] { 'R' }, 27 },
            new object[] { new char[1] { 'S' }, 28 },
            new object[] { new char[1] { 'T' }, 29 },
            new object[] { new char[1] { 'U' }, 30 },
            new object[] { new char[1] { 'V' }, 31 },
            new object[] { new char[1] { 'W' }, 32 },
            new object[] { new char[1] { 'X' }, 33 },
            new object[] { new char[1] { 'Y' }, 34 },
            new object[] { new char[1] { 'Z' }, 35 }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
