namespace Drexel.Collections.Generic.Directed
{
    public interface IReadOnlyEdge<out T>
    {
        T Head { get; }

        T Tail { get; }
    }
}
