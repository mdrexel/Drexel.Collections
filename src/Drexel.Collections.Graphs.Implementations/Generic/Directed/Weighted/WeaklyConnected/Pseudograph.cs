using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
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

        public IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents()
        {
            return Utilities.CalculateStronglyConnectedComponents<T, W, E>(this.Vertices, this.Edges);
        }

        public IEnumerable<IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents()
        {
            yield return this;
        }
    }
}
