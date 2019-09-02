namespace Drexel.Collections.Generic.Directed.Weighted.StronglyConnected
{
    public interface IReadOnlyMultigraph<out T, out W> :
        IReadOnlyPseudograph<T, W>,
        Weighted.IReadOnlyMultigraph<T, W>
    {
    }
}
