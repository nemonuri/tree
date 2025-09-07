using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static class TreeAggregatorTheory
{
    public static ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator3D<ITree<TElement>, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IAncestorConverter<ITree<TElement>, TAncestor> ancestorConverter
    )
    {
        return new TreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation>
        (
            aggregator3D, ancestorConverter
        );
    }

    public static ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator<TAncestor, TAncestorsAggregation> ancestorAggregator,
        IContextualAggregator2D<ITree<TElement>, TAggregation, TAncestorsAggregation> elementAggregator,
        IAncestorConverter<ITree<TElement>, TAncestor> ancestorConverter
    )
    {
        return Create
        (
            new Aggregator3D<ITree<TElement>, TAggregation, TAncestor, TAncestorsAggregation>
            (
                ancestorAggregator, elementAggregator
            ),
            ancestorConverter
        );
    }

    public static ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        Func<TAncestorsAggregation> initialAncestorsAggregationImplementation,
        Func<TAncestorsAggregation, TAncestor, TAncestorsAggregation> aggregateAncestorImplementation,
        Func<TAggregation> initialAggregationImplementation,
        Func<TAncestorsAggregation, TAggregation, TAggregation, ITree<TElement>, TAggregation> aggregateImplementation,
        Func<ITree<TElement>, int?, TAncestor> convertToAncestorImplementation
    )
    {
        return Create
        (
            new AdHocAggregator<TAncestor, TAncestorsAggregation>(initialAncestorsAggregationImplementation, aggregateAncestorImplementation),
            new AdHocContextualAggregator2D<ITree<TElement>, TAggregation, TAncestorsAggregation>(initialAggregationImplementation, aggregateImplementation),
            new AdHocAncestorConverter<ITree<TElement>, TAncestor>(convertToAncestorImplementation)
        );
    }

    public static ITreeAggregator<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        IAggregator2D<ITree<TElement>, TAggregation> aggregator2D
    )
    {
        Guard.IsNotNull(aggregator2D);

        return Create
        (
            NullAggregator.BoxedInstance,
            new NullContextualAggregator2D<ITree<TElement>, TAggregation>(aggregator2D),
            NullAncestorConverter<ITree<TElement>>.BoxedInstance
        );
    }

    public static ITreeAggregator<TElement, TAggregation, NullAggregation, NullAggregation>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TAggregation, TAggregation, ITree<TElement>, TAggregation> aggregateImplementation
    )
    {
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);

        return Create
        (
            new AdHocAggregator2D<ITree<TElement>, TAggregation>(initialAggregationImplementation, aggregateImplementation)
        );
    }

    public static ITreeAggregator<TElement, TAggregation, int?, IIndexPath>
    Create<TElement, TAggregation>
    (
        IContextualAggregator2D<ITree<TElement>, TAggregation, IIndexPath> contextualAggregator2D,
        IIndexPathFactory indexPathFactory
    )
    {
        Guard.IsNotNull(contextualAggregator2D);
        Guard.IsNotNull(indexPathFactory);

        return Create
        (
            new ElementIndexAggregator(indexPathFactory),
            contextualAggregator2D,
            ElementIndexProjector<ITree<TElement>>.BoxedInstance
        );
    }

    public static ITreeAggregator<TElement, TAggregation, int?, IIndexPath>
    Create<TElement, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<IIndexPath, TAggregation, TAggregation, ITree<TElement>, TAggregation> aggregateImplementation,
        IIndexPathFactory indexPathFactory
    )
    { 
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);
        Guard.IsNotNull(indexPathFactory);
        
        return Create
        (
            new AdHocContextualAggregator2D<ITree<TElement>, TAggregation, IIndexPath>
            (
                initialAggregationImplementation,
                aggregateImplementation
            ),
            indexPathFactory
        );
    }

}