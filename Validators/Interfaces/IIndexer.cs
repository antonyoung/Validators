using System.Collections;
using System.Collections.Generic;

namespace Validators.Interfaces
{
    public interface IIndexer<T>
        : IEnumerable<T>
    {
        //T this[int index] { get; set; }

        // new IEnumerator GetEnumerator();

        //new IEnumerator GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
