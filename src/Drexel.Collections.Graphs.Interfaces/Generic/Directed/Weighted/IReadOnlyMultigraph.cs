using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyMultigraph<out T, out W, out E> :
        IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        new IEnumerable<StronglyConnected.IReadOnlyMultigraph<T, W, E>> GetStronglyConnectedComponents();

        new IEnumerable<WeaklyConnected.IReadOnlyMultigraph<T, W, E>> GetWeaklyConnectedComponents();
    }
}
