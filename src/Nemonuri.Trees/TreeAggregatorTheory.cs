using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static class TreeAggregatorTheory
{
    public static ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IAncestorConverter<TTree, TAncestor> ancestorConverter
    )
        where TTree : ITree<TTree>
    {
        return new TreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation>
        (
            aggregator3D, ancestorConverter
        );
    }

    public static ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator<TAncestor, TAncestorsAggregation> ancestorAggregator,
        IContextualAggregator2D<TTree, TAggregation, TAncestorsAggregation> elementAggregator,
        IAncestorConverter<TTree, TAncestor> ancestorConverter
    )
        where TTree : ITree<TTree>
    {
        return Create
        (
            new Aggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation>
            (
                ancestorAggregator, elementAggregator
            ),
            ancestorConverter
        );
    }

    public static ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    Create<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    (
        Func<TAncestorsAggregation> initialAncestorsAggregationImplementation,
        Func<TAncestorsAggregation, TAncestor, TAncestorsAggregation> aggregateAncestorImplementation,
        Func<TAggregation> initialAggregationImplementation,
        Func<TAncestorsAggregation, TAggregation, TAggregation, TTree, TAggregation> aggregateImplementation,
        Func<TTree, int?, TAncestor> convertToAncestorImplementation
    )
        where TTree : ITree<TTree>
    {
        return Create
        (
            new AdHocAggregator<TAncestor, TAncestorsAggregation>(initialAncestorsAggregationImplementation, aggregateAncestorImplementation),
            new AdHocContextualAggregator2D<TTree, TAggregation, TAncestorsAggregation>(initialAggregationImplementation, aggregateImplementation),
            new AdHocAncestorConverter<TTree, TAncestor>(convertToAncestorImplementation)
        );
    }

    public static ITreeAggregator<TTree, TAggregation, NullValue, NullValue>
    Create<TTree, TAggregation>
    (
        IAggregator2D<TTree, TAggregation> aggregator2D
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(aggregator2D);

        return Create
        (
            NullAggregator.BoxedInstance,
            new NullContextualAggregator2D<TTree, TAggregation>(aggregator2D),
            NullAncestorConverter<TTree>.BoxedInstance
        );
    }

    public static ITreeAggregator<TTree, TAggregation, NullValue, NullValue>
    Create<TTree, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TAggregation, TAggregation, TTree, TAggregation> aggregateImplementation
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);

        return Create
        (
            new AdHocAggregator2D<TTree, TAggregation>(initialAggregationImplementation, aggregateImplementation)
        );
    }

    public static ITreeAggregator<TTree, TAggregation, int?, IIndexPath>
    Create<TTree, TAggregation>
    (
        IContextualAggregator2D<TTree, TAggregation, IIndexPath> contextualAggregator2D,
        IIndexPathFactory indexPathFactory
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(contextualAggregator2D);
        Guard.IsNotNull(indexPathFactory);

        return Create
        (
            new TreeIndexAggregator(indexPathFactory),
            contextualAggregator2D,
            TreeIndexProjector<TTree>.BoxedInstance
        );
    }

    public static ITreeAggregator<TTree, TAggregation, int?, IIndexPath>
    Create<TTree, TAggregation>
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<IIndexPath, TAggregation, TAggregation, TTree, TAggregation> aggregateImplementation,
        IIndexPathFactory indexPathFactory
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(initialAggregationImplementation);
        Guard.IsNotNull(aggregateImplementation);
        Guard.IsNotNull(indexPathFactory);

        return Create
        (
            new AdHocContextualAggregator2D<TTree, TAggregation, IIndexPath>
            (
                initialAggregationImplementation,
                aggregateImplementation
            ),
            indexPathFactory
        );
    }
}
