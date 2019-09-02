using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyMultigraph<out T, out W> :
        IReadOnlyPseudograph<T, W>
    {
        new IEnumerable<StronglyConnected.IReadOnlyMultigraph<T, W>> GetStronglyConnectedComponents();

        new IEnumerable<WeaklyConnected.IReadOnlyMultigraph<T, W>> GetWeaklyConnectedComponents();
    }
}
