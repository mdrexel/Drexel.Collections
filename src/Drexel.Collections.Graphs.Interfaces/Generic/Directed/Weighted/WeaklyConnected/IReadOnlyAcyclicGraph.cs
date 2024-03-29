﻿namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlyAcyclicGraph<out T, out W, out E> :
        IReadOnlySimpleGraph<T, W, E>,
        Weighted.IReadOnlyAcyclicGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
