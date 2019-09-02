using System.Collections.Generic;

namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyPseudograph<out T, out W>
    {
        IReadOnlySet<T> Vertices { get; }

        IReadOnlyCollection<IReadOnlyEdge<T, W>> Edges { get; }

        IEnumerable<StronglyConnected.IReadOnlyPseudograph<T, W>> GetStronglyConnectedComponents();

        IEnumerable<WeaklyConnected.IReadOnlyPseudograph<T, W>> GetWeaklyConnectedComponents();
    }
}
