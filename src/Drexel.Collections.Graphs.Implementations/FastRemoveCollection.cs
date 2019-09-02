using System;
using System.Collections;
using System.Collections.Generic;

namespace Drexel.Collections
{
    internal sealed class FastRemoveCollection<T> : IReadOnlyCollection<T>
    {
        private readonly FastDictionary<T, int> occurrences;

        public FastRemoveCollection(IEqualityComparer<T> comparer)
        {
            this.occurrences = new FastDictionary<T, int>(comparer);
        }

        public int Count { get; private set; }

        public int Add(T value)
        {
            this.Count++;
            return this.occurrences.AddOrUpdate(
                key: value,
                add: x => 1,
                update: (x, i) => i + 1);
        }

        public int Remove(T value)
        {
            this.Count--;
            return this.occurrences.RemoveOrUpdate(
                key: value,
                defaultValue: -1,
                removeOrUpdate: (T x, int i, out bool remove) =>
                {
                    int newValue = i - 1;
                    remove = newValue == 0;
                    return newValue;
                });
        }

        public IEnumerator<T> GetEnumerator()
        {
            int counter;
            foreach (KeyValuePair<T, int> occurrence in this.occurrences)
            {
                for (counter = 0; counter < occurrence.Value; counter++)
                {
                    yield return occurrence.Key;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class FastDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        {
            // TODO: I don't want to bother actually implementing this, but I suspect that it's going to need to
            // happen for performance at some point, so I'm just faking it for now.
            private readonly Dictionary<TKey, TValue> dictionary;

            public FastDictionary(IEqualityComparer<TKey> comparer)
            {
                this.dictionary = new Dictionary<TKey, TValue>(comparer);
            }

            public delegate TValue RemoveOrUpdateDelegate(TKey key, TValue value, out bool remove);

            public TValue AddOrUpdate(
                TKey key,
                Func<TKey, TValue> add,
                Func<TKey, TValue, TValue> update)
            {
                if (this.dictionary.TryGetValue(key, out TValue value))
                {
                    value = update.Invoke(key, value);
                }
                else
                {
                    value = add.Invoke(key);
                }

                this.dictionary[key] = value;
                return value;
            }

            public TValue RemoveOrUpdate(
                TKey key,
                TValue defaultValue,
                RemoveOrUpdateDelegate removeOrUpdate)
            {
                if (this.dictionary.TryGetValue(key, out TValue value))
                {
                    value = removeOrUpdate.Invoke(key, value, out bool remove);
                    if (remove)
                    {
                        this.dictionary.Remove(key);
                    }
                    else
                    {
                        this.dictionary[key] = value;
                    }
                }
                else
                {
                    value = defaultValue;
                }

                return value;
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.dictionary.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => this.dictionary.GetEnumerator();
        }
    }
}
