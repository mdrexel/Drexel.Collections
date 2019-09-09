using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    internal class Pseudograph<T, W, E> : IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        private readonly IReadOnlyNullDictionary<T, IReadOnlyCollection<E>> adjacencyLists;

        internal Pseudograph(
            IReadOnlyNullDictionary<T, IReadOnlyCollection<E>> adjacencyLists,
            IReadOnlyCollection<E> edges)
        {
            this.adjacencyLists = adjacencyLists;

            this.Vertices = new CollectionSetAdapter<T>(this.adjacencyLists.Keys);
            this.Edges = edges;
        }

        public IReadOnlySet<T> Vertices { get; }

        public IReadOnlyCollection<E> Edges { get; }

        public IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents()
        {
            return Utilities.CalculateStronglyConnectedComponents<T, W, E, IReadOnlyCollection<E>>(
                this.adjacencyLists,
                this.Edges);
        }

        public IEnumerable<IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents()
        {
            yield return this;
        }
    }
}
