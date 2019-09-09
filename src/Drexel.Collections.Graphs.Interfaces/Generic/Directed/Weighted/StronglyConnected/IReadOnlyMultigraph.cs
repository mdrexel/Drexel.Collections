namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlyMultigraph<out T, out W, out E> :
        IReadOnlyPseudograph<T, W, E>,
        Weighted.IReadOnlyMultigraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
