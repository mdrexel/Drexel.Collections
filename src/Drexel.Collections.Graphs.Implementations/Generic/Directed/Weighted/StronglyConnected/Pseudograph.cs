using System;
using System.Collections.Generic;
using System.Text;
using Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected;

namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    internal class Pseudograph<T, W, E> : IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        internal Pseudograph(
            IReadOnlySet<T> vertices,
            IReadOnlyCollection<E> edges)
        {
            this.Vertices = vertices;
            this.Edges = edges;
        }

        public IReadOnlySet<T> Vertices { get; }

        public IReadOnlyCollection<E> Edges { get; }

        public IEnumerable<IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents()
        {
            yield return this;
        }

        public IEnumerable<WeaklyConnected.IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents()
        {
            throw new NotImplementedException();
        }
    }
}
