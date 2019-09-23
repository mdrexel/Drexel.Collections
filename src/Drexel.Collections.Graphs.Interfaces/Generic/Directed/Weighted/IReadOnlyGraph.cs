using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyGraph<out T, out W, out E> :
        IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        new IReadOnlySet<E> Edges { get; }

        new IReadOnlyCollection<StronglyConnected.IReadOnlyGraph<T, W, E>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlyGraph<T, W, E>> GetWeaklyConnectedComponents();
    }
}
