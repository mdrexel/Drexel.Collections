using System.Collections.Generic;

namespace Drexel.Collections
{
    internal interface IReadOnlyNullDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        TValue this[TKey key] { get; }

        IReadOnlyCollection<TKey> Keys { get; }

        IReadOnlyCollection<TValue> Values { get; }

        bool ContainsKey(TKey key);

        bool TryGetValue(TKey key, out TValue value);
    }
}