using System.Collections.Generic;
using Drexel.Collections.Generic;

namespace Drexel.Collections
{
    internal interface IReadOnlyAdjacencyLists<TKey, TValue, TInnerValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
        where TValue : IReadOnlyCollection<TInnerValue>
    {
        TValue this[TKey key] { get; }

        IReadOnlySet<TKey> Keys { get; }

        IReadOnlyCollection<TValue> Values { get; }

        bool ContainsKey(TKey key);

        bool TryGetValue(TKey key, out TValue value);
    }
}