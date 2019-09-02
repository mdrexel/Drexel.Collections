namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlyGraph<out T, out W> :
        IReadOnlyMultigraph<T, W>,
        Weighted.IReadOnlyGraph<T, W>
    {
    }
}
