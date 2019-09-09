using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlySimpleGraph<out T, out W, out E> :
        IReadOnlyGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        new IReadOnlyCollection<StronglyConnected.IReadOnlySimpleGraph<T, W, E>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlySimpleGraph<T, W, E>> GetWeaklyConnectedComponents();
    }
}
