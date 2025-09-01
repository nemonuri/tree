
namespace Nemonuri.Trees;

using Abstractions;

public static class TreeWalkerTheory
{
    public static TreeWalker<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        IAggregator2D<TElement, TAggregation> aggregator2D,
        IChildrenProvider<TElement> childrenProvider
    )
    {
        Guard.IsNotNull(aggregator2D);
        Guard.IsNotNull(childrenProvider);

        return new TreeWalker<TElement, TAggregation, NullAggregation, NullAggregation>
        (
            (NullAggregator)default,
            new NullContextualAggregator2D<TElement, TAggregation>(aggregator2D),
            childrenProvider,
            (NullAncestorConverter<TElement>)default
        );
    }

    public static TreeWalker<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation,
        Func<TElement, IEnumerable<TElement>> getChildrenImplementation
    )
    {
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);
        Guard.IsNotNull(getChildrenImplementation);

        return Create<TElement, TAggregation>
        (
            new AdHocAggregator2D<TElement, TAggregation>(initialAggregationImplementation, aggregateImplementation),
            new AdHocChildrenProvider<TElement>(getChildrenImplementation)
        );
    }
}