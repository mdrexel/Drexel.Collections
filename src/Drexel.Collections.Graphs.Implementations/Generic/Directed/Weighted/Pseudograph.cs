using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drexel.Collections.Generic.Directed.Weighted.StronglyConnected;
using Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected;
using Drexel.Collections.ObjectModel;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public class Pseudograph<T, W, E> : IReadOnlyPseudograph<T, W, E>
        where E : class, IReadOnlyEdge<T, W>
    {
        private readonly IEqualityComparer<T> vertexComparer;
        private readonly IEqualityComparer<E> edgeComparer;

        private readonly NullDictionary<T, FastRemoveCollection<E>> adjacencyLists;
        private readonly FastRemoveCollection<E> edges;

        public Pseudograph(
            IEqualityComparer<T> vertexComparer,
            IEqualityComparer<E> edgeComparer)
        {
            this.vertexComparer = vertexComparer ?? throw new ArgumentNullException(nameof(vertexComparer));
            this.edgeComparer = edgeComparer ?? throw new ArgumentNullException(nameof(edgeComparer));

            this.adjacencyLists = new NullDictionary<T, FastRemoveCollection<E>>(this.vertexComparer);
            this.edges = new FastRemoveCollection<E>(edgeComparer);

            this.Vertices = new CollectionSetAdapter<T>(this.adjacencyLists.Keys);
            this.Edges = this.edges;
        }

        public IReadOnlySet<T> Vertices { get; }

        public IReadOnlyCollection<E> Edges { get; }

        public bool Add(T vertex)
        {
            if (this.adjacencyLists.ContainsKey(vertex))
            {
                return false;
            }
            else
            {
                this.adjacencyLists.Add(vertex, new FastRemoveCollection<E>(this.edgeComparer));
                return true;
            }
        }

        public void Add(E edge)
        {
            if (object.ReferenceEquals(edge, null))
            {
                throw new ArgumentNullException(nameof(edge));
            }

            // Since our vertices are a set, adding the vertices of the edge will do nothing if we already had them.
            this.Add(edge.Head);
            this.Add(edge.Tail);

            this.adjacencyLists[edge.Head].Add(edge);
            this.adjacencyLists[edge.Tail].Add(edge);

            this.edges.Add(edge);
        }

        public bool Remove(T vertex, out IReadOnlyCollection<E> removedEdges)
        {
            if (!this.adjacencyLists.TryGetValue(vertex, out FastRemoveCollection<E> list))
            {
                removedEdges = Array.Empty<E>();
                return false;
            }
            else
            {
                foreach (E edge in list)
                {
                    this.edges.Remove(edge);
                }

                this.adjacencyLists.Remove(vertex);
                removedEdges = list;
                return true;
            }
        }

        public bool Remove(E edge)
        {
            if (object.ReferenceEquals(edge, null))
            {
                throw new ArgumentNullException(nameof(edge));
            }

            bool retVal = this.edges.Remove(edge) > 0;

            this.adjacencyLists[edge.Head].Remove(edge);
            this.adjacencyLists[edge.Tail].Remove(edge);

            return retVal;
        }

        public IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents()
        {
            return Utilities.CalculateStronglyConnectedComponents<T, W, E>(
                this.adjacencyLists,
                this.Edges);
        }

        public IEnumerable<WeaklyConnected.IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents()
        {
            return Utilities.CalculateWeaklyConnectedComponents<T, W, E>(
                this.adjacencyLists,
                this.Edges);
        }
    }
}
