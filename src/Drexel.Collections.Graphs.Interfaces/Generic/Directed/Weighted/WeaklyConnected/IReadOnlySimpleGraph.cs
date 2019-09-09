namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlySimpleGraph<out T, out W, out E> :
        IReadOnlyGraph<T, W, E>,
        Weighted.IReadOnlySimpleGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
