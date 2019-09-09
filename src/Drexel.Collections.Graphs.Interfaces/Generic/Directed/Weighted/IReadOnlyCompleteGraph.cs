namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyCompleteGraph<out T, out W, out E> :
        IReadOnlySimpleGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
