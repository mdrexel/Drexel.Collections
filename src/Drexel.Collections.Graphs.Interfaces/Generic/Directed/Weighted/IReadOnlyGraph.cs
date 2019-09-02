using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyGraph<out T, out W> :
        IReadOnlyMultigraph<T, W>
    {
        IReadOnlySet<IReadOnlyEdge<T, W>> Edges { get; }

        new IReadOnlyCollection<StronglyConnected.IReadOnlyGraph<T, W>> GetStronglyConnectedComponents();

        new IReadOnlyCollection<WeaklyConnected.IReadOnlyGraph<T, W>> GetWeaklyConnectedComponents();
    }
}
