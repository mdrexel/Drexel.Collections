using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Drexel.Collections.Generic;

namespace Drexel.Collections
{
    internal sealed class CollectionSetAdapter<T> : IReadOnlySet<T>
    {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
        // Intentionally allow possible illegal null, because it's only used when the constructor that allows null is
        // used. Since this class is internal, no external user can introduce an illegal null.
        private static readonly IReadOnlyCollection<T> DefaultArray = new T[1] { default };
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

        private readonly Func<int> countDelegate;
        private readonly Func<IEnumerator<T>> enumeratorDelegate;

        public CollectionSetAdapter(IReadOnlyCollection<T> collection)
        {
            this.countDelegate = () => collection.Count;
            this.enumeratorDelegate = collection.GetEnumerator;
        }

        public CollectionSetAdapter(IReadOnlyCollection<T> collection, Func<bool> includeNull)
        {
            this.countDelegate = () => includeNull.Invoke() ? collection.Count + 1 : collection.Count;
            this.enumeratorDelegate = () => includeNull.Invoke()
                ? collection.Concat(DefaultArray).GetEnumerator()
                : collection.GetEnumerator();
        }

        public CollectionSetAdapter(IReadOnlyCollection<T> collectionOne, IReadOnlyCollection<T> collectionTwo)
        {
            this.countDelegate = () => collectionOne.Count + collectionTwo.Count;
            this.enumeratorDelegate = () => collectionOne.Concat(collectionTwo).GetEnumerator();
        }

        public int Count => this.countDelegate.Invoke();

        public IEnumerator<T> GetEnumerator() => this.enumeratorDelegate.Invoke();

        IEnumerator IEnumerable.GetEnumerator() => this.enumeratorDelegate.Invoke();
    }
}
