using System.Collections;
using System.Collections.Generic;

namespace Validators.Tests.TestData
{
    public class PostcodeFormatterTestData 
        : IEnumerable<object[]>
    {

        /// <summary>
        ///     used as internal test data, for this test class. Add additonal test data as you wish.
        /// </summary>
        private readonly IEnumerable<object[]> _data = new List<object[]>
        {
            new object[] { "EC1A 1BB" },
            new object[] { "1062 GD" },
            new object[] { "EDG 1062" },
            new object[] { "54321" },
            new object[] { "123 45" },
            new object[] { "12-345" },
            new object[] { "123-45" },
            new object[] { "SE-434 78" },
            new object[] { "HR-12345" }
        };


        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
