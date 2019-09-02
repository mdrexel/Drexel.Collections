namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlySimpleGraph<out T, out W> :
        IReadOnlyGraph<T, W>,
        Weighted.IReadOnlySimpleGraph<T, W>
    {
    }
}
