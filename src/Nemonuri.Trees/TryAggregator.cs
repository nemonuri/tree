#if false

namespace Nemonuri.Trees;

public delegate bool TryAggregator<TSource, TTarget>
(
    TTarget siblingsAggregated,
    TSource source,
    [NotNullWhen(true)] out TTarget? aggregated
);

public delegate (TTarget?, bool) OptionalAggregator<TSource, TTarget>
(
    TTarget siblingsAggregated,
    TSource source
);

public delegate bool TryAggregator2D<TSource, TTarget>
(
    TTarget siblingsAggregated,
    TTarget childrenAggregated,
    TSource source,
    [NotNullWhen(true)] out TTarget? aggregated
);

public delegate (TTarget?, bool) OptionalAggregator2D<TSource, TTarget>
(
    TTarget siblingsAggregated,
    TTarget childrenAggregated,
    TSource source
);

public delegate bool TryAggregator2DWithContext<TSource, TTarget, TContext>
(
    TContext context,
    TTarget siblingsAggregated,
    TTarget childrenAggregated,
    TSource source,
    [NotNullWhen(true)] out TTarget? aggregated
);

public delegate (TTarget?, bool) OptionalAggregator2DWithContext<TSource, TTarget, TContext>
(
    TContext context,
    TTarget siblingsAggregated,
    TTarget childrenAggregated,
    TSource source
);

#endif