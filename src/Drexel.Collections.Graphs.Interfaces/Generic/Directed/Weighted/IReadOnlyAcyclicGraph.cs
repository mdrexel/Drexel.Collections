using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyAcyclicGraph<out T, out W> :
        IReadOnlySimpleGraph<T, W>
    {
        new IReadOnlyCollection<StronglyConnected.IReadOnlyAcyclicGraph<T, W>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlyAcyclicGraph<T, W>> GetWeaklyConnectedComponents();
    }
}
