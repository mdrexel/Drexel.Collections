namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlyAcyclicGraph<out T, out W> :
        IReadOnlySimpleGraph<T, W>,
        Weighted.IReadOnlyAcyclicGraph<T, W>
    {
    }
}
