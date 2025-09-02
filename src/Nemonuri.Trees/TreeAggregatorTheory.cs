using IndexesFromRoot = System.Collections.Immutable.ImmutableList<int>;

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

    public static TreeAggregator<TElement, TAggregation, int?, IndexesFromRoot>
    Create<TElement, TAggregation>
    (
        IContextualAggregator2D<TElement, TAggregation, IndexesFromRoot> contextualAggregator2D
    )
    {
        Guard.IsNotNull(contextualAggregator2D);

        return new TreeAggregator<TElement, TAggregation, int?, ImmutableList<int>>
        (
            ElementIndexAggregator.BoxedInstance,
            contextualAggregator2D,
            ElementIndexProjector<TElement>.BoxedInstance
        );
    }

    public static TreeAggregator<TElement, TAggregation, int?, IndexesFromRoot>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<IndexesFromRoot, TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation
    )
    { 
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);
        
        return Create
        (
            new AdHocContextualAggregator2D<TElement, TAggregation, IndexesFromRoot>
            (
                initialAggregationImplementation,
                aggregateImplementation
            )
        );
    }
}