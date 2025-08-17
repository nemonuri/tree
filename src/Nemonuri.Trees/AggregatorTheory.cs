namespace Nemonuri.Trees;

public static class AggregatorTheory
{
    public static
    TryAggregator<TSource, TTarget>
    ToTryAggregator<TSource, TTarget>
    (
        this OptionalAggregator<TSource, TTarget> optionalAggregator
    )
    {
        Debug.Assert(optionalAggregator is not null);

        return TryAggregatorImpl;

        bool TryAggregatorImpl
        (
            TTarget siblingsAggregated,
            TSource source,
            [NotNullWhen(true)] out TTarget? aggregated
        )
        {
            var result = optionalAggregator(siblingsAggregated, source);
            aggregated = result.Item2 ? result.Item1 : default;
            return aggregated is not null;
        }
    }

    public static
    TryAggregator2D<TSource, TTarget>
    ToTryAggregator2D<TSource, TTarget>
    (
        this OptionalAggregator2D<TSource, TTarget> optionalAggregator
    )
    {
        Debug.Assert(optionalAggregator is not null);

        return TryAggregatorImpl;

        bool TryAggregatorImpl
        (
            TTarget siblingsAggregated,
            TTarget childrenAggregated,
            TSource source,
            [NotNullWhen(true)] out TTarget? aggregated
        )
        {
            var result = optionalAggregator(siblingsAggregated, childrenAggregated, source);
            aggregated = result.Item2 ? result.Item1 : default;
            return aggregated is not null;
        }
    }

    public static
    TryAggregator2DWithContext<TSource, TTarget, TContext>
    ToTryAggregator2DWithContext<TSource, TTarget, TContext>
    (
        this OptionalAggregator2DWithContext<TSource, TTarget, TContext> optionalAggregator
    )
    {
        Debug.Assert(optionalAggregator is not null);

        return TryAggregatorImpl;

        bool TryAggregatorImpl
        (
            TContext context,
            TTarget siblingsAggregated,
            TTarget childrenAggregated,
            TSource source,
            [NotNullWhen(true)] out TTarget? aggregated
        )
        {
            var result = optionalAggregator(context, siblingsAggregated, childrenAggregated, source);
            aggregated = result.Item2 ? result.Item1 : default;
            return aggregated is not null;
        }
    }
}