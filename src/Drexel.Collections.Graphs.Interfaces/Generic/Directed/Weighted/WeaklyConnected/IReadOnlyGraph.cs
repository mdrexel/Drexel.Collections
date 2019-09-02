namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlyGraph<out T, out W> :
        IReadOnlyMultigraph<T, W>,
        Weighted.IReadOnlyGraph<T, W>
    {
    }
}
