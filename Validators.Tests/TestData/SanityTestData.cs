using System.Collections;
using System.Collections.Generic;


namespace Validators.Tests.TestData
{
    public class SanityTestData
         : IEnumerable<object[]>
    {

        private readonly IEnumerable<object[]> _data = new List<object[]>
        {
            new object[] 
            {   new char[] { 'A', 'B' },
                1011
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C' },
                101112
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C', 'D' },
                10111213
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C', 'D', 'E' },
                1011121314
            },
            new object[] {
                new char[] { 'A', 'B', 'C', 'D', 'E', 'F' },
                101112131415
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' },
                10111213141516
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' },
                1011121314151617
            },
            new object[] 
            {
                new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' },
                101112131415161718
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
