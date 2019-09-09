using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    internal class Pseudograph<T, W, E> : IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        private readonly NullDictionary<T, FastRemoveCollection<E>> adjacencyLists;
        private readonly FastRemoveCollection<E> edges;

        internal Pseudograph(
            NullDictionary<T, FastRemoveCollection<E>> adjacencyLists,
            FastRemoveCollection<E> edges)
        {
            this.adjacencyLists = adjacencyLists;
            this.edges = edges;

            this.Vertices = new CollectionSetAdapter<T>(this.adjacencyLists.Keys);
            this.Edges = edges;
        }

        public IReadOnlySet<T> Vertices { get; }

        public IReadOnlyCollection<E> Edges { get; }

        public IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents()
        {
            return Utilities.CalculateStronglyConnectedComponents<T, W, E>(this.adjacencyLists, this.edges);
        }

        public IEnumerable<IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents()
        {
            yield return this;
        }
    }
}
