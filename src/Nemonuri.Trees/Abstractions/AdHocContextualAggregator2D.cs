
namespace Nemonuri.Trees.Abstractions;

public readonly struct AdHocContextualAggregator2D<TElement, TAggregation, TContext> :
    IContextualAggregator2D<TElement, TAggregation, TContext>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TContext : allows ref struct
#endif
{
    private readonly Func<TAggregation> _initialAggregationImplementation;
    private readonly Func<TContext, TAggregation, TAggregation, TElement, TAggregation> _aggregateImplementation;

    public AdHocContextualAggregator2D
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TContext, TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation
    )
    {
        Debug.Assert(initialAggregationImplementation is not null);
        Debug.Assert(aggregateImplementation is not null);

        _initialAggregationImplementation = initialAggregationImplementation;
        _aggregateImplementation = aggregateImplementation;
    }

    public TAggregation InitialAggregation => _initialAggregationImplementation();

    public TAggregation Aggregate(TContext context, TAggregation siblingsAggregation, TAggregation childrenAggregation, TElement element) =>
        _aggregateImplementation(context, siblingsAggregation, childrenAggregation, element);
}
