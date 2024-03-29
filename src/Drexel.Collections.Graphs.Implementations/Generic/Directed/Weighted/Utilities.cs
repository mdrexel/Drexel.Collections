﻿using System;
using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    internal static class Utilities
    {
        public static IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> CalculateStronglyConnectedComponents<T, W, E, L>(
            IReadOnlyAdjacencyLists<T, L, E> adjacencyLists,
            IReadOnlyCollection<E> edgeCollection)
            where E : IReadOnlyEdge<T, W>
            where L : IReadOnlyCollection<E>
        {
            // A pseudograph is just a graph with parallel edges, so the algorithm for calculating the strongly
            // connected components shouldn't be any different than the algorithm for a regular graph. Could probably
            // figure out some way of calling the normal IReadOnlyGraph version here.
            throw new NotImplementedException();
        }

        public static IEnumerable<WeaklyConnected.IReadOnlyPseudograph<T, W, E>> CalculateWeaklyConnectedComponents<T, W, E, L>(
            IReadOnlyAdjacencyLists<T, L, E> adjacencyLists,
            IReadOnlyCollection<E> edgeCollection)
            where E : IReadOnlyEdge<T, W>
            where L : IReadOnlyCollection<E>
        {
            // A pseudograph is just a graph with parallel edges, so the algorithm for calculating the weakly
            // connected components shouldn't be any different than the algorithm for a regular graph. Could probably
            // figure out some way of calling the normal IReadOnlyGraph version here.
            throw new NotImplementedException();
        }

        public static IEnumerable<StronglyConnected.IReadOnlyGraph<T, W, E>> CalculateStronglyConnectedComponents<T, W, E, L>(
            IReadOnlyAdjacencyLists<T, L, E> adjacencyLists,
            IReadOnlySet<E> edgeSet)
            where E : IReadOnlyEdge<T, W>
            where L : IReadOnlyCollection<E>
        {
            // TODO: Implement Tarjan's algorithm
            throw new NotImplementedException();
        }

        public static IEnumerable<WeaklyConnected.IReadOnlyGraph<T, W, E>> CalculateWeaklyConnectedComponents<T, W, E, L>(
            IReadOnlyAdjacencyLists<T, L, E> adjacencyLists,
            IReadOnlySet<E> edgeSet)
            where E : IReadOnlyEdge<T, W>
            where L : IReadOnlyCollection<E>
        {
            // TODO: A directed graph's weakly connected components are the same as an undirected graph's connected
            // components. So, to calculate the weakly connected components, we can just ignore the directionality of
            // the supplied edges and perform an undirected DFS.
            throw new NotImplementedException();
        }
    }
}
