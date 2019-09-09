using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyAcyclicGraph<out T, out W, out E> :
        IReadOnlySimpleGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
        new IReadOnlyCollection<StronglyConnected.IReadOnlyAcyclicGraph<T, W, E>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlyAcyclicGraph<T, W, E>> GetWeaklyConnectedComponents();
    }
}
