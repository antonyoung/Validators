using System;
using System.Collections.Generic;


namespace Validators.Indexers
{

    /// <summary>
    ///     useed as a symplistic indexer of T.
    /// </summary>
    /// <typeparam name="T"><
    ///     used as the anonymous type, that has to be indexed.
    /// /typeparam>
    public class GenericIndexer<T>
    {

        /// <summary>
        ///     used as internal IEnumerable of T.
        /// </summary>
        private readonly IEnumerable<T> _items;

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
            => _items = items
                ?? throw new ArgumentNullException(nameof(items));


        /// <summary>
        ///     used as to enumerate over the items.
        /// </summary>
        /// <returns>
        ///     IEnumerator<typeparamref name="T"/> of the items.
        /// </returns>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    }
}
