
namespace Nemonuri.Trees;

using Abstractions;

public static class TreeAggregatorTheory
{
    public static TreeAggregator<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        IAggregator2D<TElement, TAggregation> aggregator2D
    )
    {
        Guard.IsNotNull(aggregator2D);

        return new TreeAggregator<TElement, TAggregation, NullAggregation, NullAggregation>
        (
            (NullAggregator)default,
            new NullContextualAggregator2D<TElement, TAggregation>(aggregator2D),
            (NullAncestorConverter<TElement>)default
        );
    }

    public static TreeAggregator<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation
    )
    {
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);

        return Create<TElement, TAggregation>
        (
            new AdHocAggregator2D<TElement, TAggregation>(initialAggregationImplementation, aggregateImplementation)
        );
    }
}