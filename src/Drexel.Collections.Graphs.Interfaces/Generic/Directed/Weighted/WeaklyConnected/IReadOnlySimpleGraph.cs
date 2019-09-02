namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlySimpleGraph<out T, out W> :
        IReadOnlyGraph<T, W>,
        Weighted.IReadOnlySimpleGraph<T, W>
    {
    }
}
