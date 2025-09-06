using IndexPathFromRoot = System.Collections.Immutable.ImmutableList<int>;

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
            NullAggregator.BoxedInstance,
            new NullContextualAggregator2D<TElement, TAggregation>(aggregator2D),
            NullAncestorConverter<TElement>.BoxedInstance
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

        return Create
        (
            new AdHocAggregator2D<TElement, TAggregation>(initialAggregationImplementation, aggregateImplementation)
        );
    }

#if false
    public static TreeAggregator<TElement, TAggregation, int?, IndexPathFromRoot>
    Create<TElement, TAggregation>
    (
        IContextualAggregator2D<TElement, TAggregation, IndexPathFromRoot> contextualAggregator2D
    )
    {
        Guard.IsNotNull(contextualAggregator2D);

        return new TreeAggregator<TElement, TAggregation, int?, IndexPathFromRoot>
        (
            ElementIndexAggregator.BoxedInstance,
            contextualAggregator2D,
            ElementIndexProjector<TElement>.BoxedInstance
        );
    }

    public static TreeAggregator<TElement, TAggregation, int?, IndexPathFromRoot>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<IndexPathFromRoot, TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation
    )
    { 
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);
        
        return Create
        (
            new AdHocContextualAggregator2D<TElement, TAggregation, IndexPathFromRoot>
            (
                initialAggregationImplementation,
                aggregateImplementation
            )
        );
    }
#endif
}