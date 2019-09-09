namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlyPseudograph<out T, out W, out E> :
        Weighted.IReadOnlyPseudograph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
