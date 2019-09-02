namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlyAcyclicGraph<out T, out W> :
        IReadOnlySimpleGraph<T, W>,
        Weighted.IReadOnlyAcyclicGraph<T, W>
    {
    }
}
