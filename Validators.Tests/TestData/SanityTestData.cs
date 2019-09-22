using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

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
            },
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            //    10111213141516171819
            //},
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L' },
            //    BigInteger.Parse("1011121314151617181920")
            //},
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' },
            //    BigInteger.Parse("101112131415161718192021")
            //},
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N' },
            //    BigInteger.Parse("10111213141516171819202122")
            //},
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O' },
            //    BigInteger.Parse("1011121314151617181920212223")
            //},
            //new object[] 
            //{
            //    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P' },
            //    BigInteger.Parse("101112131415161718192021222324")
            //}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
