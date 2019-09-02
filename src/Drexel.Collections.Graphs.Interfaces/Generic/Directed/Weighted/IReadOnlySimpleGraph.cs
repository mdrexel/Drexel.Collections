using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlySimpleGraph<out T, out W> :
        IReadOnlyGraph<T, W>
    {
        new IReadOnlyCollection<StronglyConnected.IReadOnlySimpleGraph<T, W>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlySimpleGraph<T, W>> GetWeaklyConnectedComponents();
    }
}
