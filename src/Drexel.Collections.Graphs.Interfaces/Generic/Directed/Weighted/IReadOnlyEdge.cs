namespace Drexel.Collections.Generic.Directed.Weighted
{
    public interface IReadOnlyEdge<out T, out W> :
        Directed.IReadOnlyEdge<T>
    {
        W Weight { get; }
    }
}
