﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Drexel.Collections
{
    // This is a goofy way of sussing out the system "duplicate key" exception. Unfortunately, the ThrowHelper is
    // considered "internal" (which makes sense I guess, since it changed between Framework and Core), so there's no
    // way for us to easily throw the expected exception.
    internal static class NullDictionary
    {
#pragma warning disable CA1305 // Specify IFormatProvider - Intentional, we want to use the locale we're running in.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type. - Intentional
        private static string NullKey { get; } = Convert.ToString((object)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CA1305 // Specify IFormatProvider

        private static Dictionary<string, int> HasNull { get; } =
            new Dictionary<string, int>()
            {
                [NullKey] = 0
            };

        private static Dictionary<string, int> DoesntHaveNull { get; } =
            new Dictionary<string, int>(0);

        [DebuggerHidden]
        public static void ThrowDuplicateNullKeyException()
        {
            NullDictionary.HasNull.Add(NullDictionary.NullKey, 0);
        }

        [DebuggerHidden]
        public static void ThrowNullKeyNotFoundException()
        {
            _ = NullDictionary.DoesntHaveNull[NullDictionary.NullKey];
        }
    }

    internal sealed class NullDictionary<TKey, TValue> : IReadOnlyNullDictionary<TKey, TValue>
    {
        private readonly IEqualityComparer<TKey> comparer;
        private readonly Dictionary<TKey, TValue> dictionary;

        private bool hasNull;
        private TValue nullValue;

        public NullDictionary(IEqualityComparer<TKey> comparer)
        {
            IReadOnlyDictionary<TKey, TValue> foo = default;
            this.comparer = comparer;
            this.dictionary = new Dictionary<TKey, TValue>(comparer);
            this.hasNull = false;
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            this.nullValue = default; // Safe because access is guarded by `hasNull`.
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

            this.Keys = new KeysCollectionAdapter(this);
            this.Values = new ValuesCollectionAdapter(this);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (object.ReferenceEquals(key, null))
                {
                    if (this.hasNull)
                    {
                        return this.nullValue;
                    }
                    else
                    {
                        NullDictionary.ThrowNullKeyNotFoundException();
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                        return default; // This never gets hit, it's not smart enough to realize we threw an exception.
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
                    }
                }
                else
                {
                    return this.dictionary[key];
                }
            }

            set
            {
                if (object.ReferenceEquals(key, null))
                {
                    this.hasNull = true;
                    this.nullValue = value;
                }
                else
                {
                    this.dictionary[key] = value;
                }
            }
        }

        public int Count => this.hasNull ? this.dictionary.Count + 1 : this.dictionary.Count;

        public IReadOnlyCollection<TKey> Keys { get; }

        public IReadOnlyCollection<TValue> Values { get; }

        public void Add(TKey key, TValue value)
        {
            if (object.ReferenceEquals(key, null))
            {
                if (this.hasNull)
                {
                    NullDictionary.ThrowDuplicateNullKeyException();
                }
                else
                {
                    this.hasNull = true;
                    this.nullValue = value;
                }
            }
            else
            {
                this.dictionary.Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (object.ReferenceEquals(key, null))
            {
                return this.hasNull;
            }
            else
            {
                return this.dictionary.ContainsKey(key);
            }
        }

        public bool Remove(TKey key)
        {
            if (object.ReferenceEquals(key, null))
            {
                if (this.hasNull)
                {
                    this.hasNull = false;
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                    this.nullValue = default; // Probably unnecessary, but I don't want GC to hang onto values.
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return this.dictionary.Remove(key);
            }
        }

        public NullDictionary<TKey, TValue> ShallowClone()
        {
            // TODO: There's probably a more efficient way to handle this, but it's fine for now I guess
            NullDictionary<TKey, TValue> retVal = new NullDictionary<TKey, TValue>(this.comparer);
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                retVal.Add(kvp.Key, kvp.Value);
            }

            return retVal;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (object.ReferenceEquals(key, null))
            {
                if (this.hasNull)
                {
                    value = this.nullValue;
                    return true;
                }
                else
                {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                    // TODO: The ability to describe the TryGetValue pattern when using nullable reference types won't
                    // be available until .NET Standard 2.1.
                    // https://github.com/dotnet/standard/pull/1349
                    // https://docs.microsoft.com/en-us/dotnet/csharp/nullable-attributes
                    value = default;
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
                    return false;
                }
            }
            else
            {
                return this.dictionary.TryGetValue(key, out value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (this.hasNull
                ? this.dictionary.Concat(
                    new KeyValuePair<TKey, TValue>[1]
                    {
    #pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                        // This is safe because the only way `hasNull` was true is if they called `Add(null)`, which
                        // means the compiler already checked that `null` was legal.
                        new KeyValuePair<TKey, TValue>(default, this.nullValue)
    #pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
                    })
                : this.dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class KeysCollectionAdapter : IReadOnlyCollection<TKey>
        {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            // This is safe because the only way they introduced null was if the compiler checked it on Add.
            private static readonly IEnumerable<TKey> NullKey = new TKey[] { default };
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

            private readonly NullDictionary<TKey, TValue> parent;

            public KeysCollectionAdapter(NullDictionary<TKey, TValue> parent)
            {
                this.parent = parent;
            }

            public int Count
            {
                get
                {
                    return this.parent.hasNull
                        ? this.parent.dictionary.Keys.Count + 1
                        : this.parent.dictionary.Keys.Count;
                }
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                if (this.parent.hasNull)
                {
                    return this.parent.dictionary.Keys.Concat(KeysCollectionAdapter.NullKey).GetEnumerator();
                }
                else
                {
                    return this.parent.dictionary.Keys.GetEnumerator();
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }

        private class ValuesCollectionAdapter : IReadOnlyCollection<TValue>
        {
            private readonly NullDictionary<TKey, TValue> parent;

            public ValuesCollectionAdapter(NullDictionary<TKey, TValue> parent)
            {
                this.parent = parent;
            }

            public int Count => this.parent.hasNull
                ? this.parent.dictionary.Values.Count + 1
                : this.parent.dictionary.Values.Count;

            public IEnumerator<TValue> GetEnumerator()
            {
                if (this.parent.hasNull)
                {
                    return this
                        .parent
                        .dictionary
                        .Values
                        .Concat(new TValue[] { this.parent.nullValue })
                        .GetEnumerator();
                }
                else
                {
                    return this.parent.dictionary.Values.GetEnumerator();
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
