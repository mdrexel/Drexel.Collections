namespace Drexel.Collections.Generic.Directed.Weighted
{
    public class Edge<T, W> : IReadOnlyEdge<T, W>
    {
        public Edge(T head, T tail, W weight)
        {
            this.Head = head;
            this.Tail = tail;
            this.Weight = weight;
        }

        public T Head { get; set; }

        public T Tail { get; set; }

        public W Weight { get; set; }
    }
}
