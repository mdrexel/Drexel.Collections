using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyPseudograph<out T, out W, out E>
        where E : IReadOnlyEdge<T, W>
    {
        IReadOnlySet<T> Vertices { get; }

        IReadOnlyCollection<E> Edges { get; }

        IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W, E>> GetStronglyConnectedComponents();

        IEnumerable<WeaklyConnected.IReadOnlyPseudograph<T, W, E>> GetWeaklyConnectedComponents();
    }
}
