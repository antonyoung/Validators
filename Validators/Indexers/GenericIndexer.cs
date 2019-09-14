using System;
using System.Collections.Generic;


namespace Validators.Indexers
{


    /// <summary>
    ///     useed as a symplystic indexer of T. Nice additional feature to use.
    /// </summary>
    /// <typeparam name="T"><
    ///     used as the anonymous type, that has to be indexed?
    /// /typeparam>
    public class GenericIndexer<T>
    {


        /// <summary>
        ///     used as construcor to initiliaze this as indexer of anonymous T.
        /// </summary>
        /// <param name="items">
        ///     used as the anonymous T collection,
        /// </param>
        /// <exception cref="System.ArgumentNullException(nameof(items))">
        ///     throws new ArgumentNullException in case <paramref name="items"/> is null.
        /// </exception>
        public GenericIndexer(IEnumerable<T> items)
            => Items = items
                ?? throw new ArgumentNullException(nameof(items));
       

        public IEnumerable<T> Items { private get; set; }



        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

    }
}
