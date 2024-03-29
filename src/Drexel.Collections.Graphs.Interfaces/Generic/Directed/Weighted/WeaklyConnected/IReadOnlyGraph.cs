﻿namespace Drexel.Collections.Generic.Directed.Weighted.WeaklyConnected
{
    public interface IReadOnlyGraph<out T, out W, out E> :
        IReadOnlyPseudograph<T, W, E>,
        Weighted.IReadOnlyGraph<T, W, E>
        where E : IReadOnlyEdge<T, W>
    {
    }
}
