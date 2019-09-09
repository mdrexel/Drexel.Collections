namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlyAcyclicGraph<out T, out W, out E> :
        IReadOnlySimpleGraph<T, W, E>,
        WeaklyConnected.IReadOnlyAcyclicGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
