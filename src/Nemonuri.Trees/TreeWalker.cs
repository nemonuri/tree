
namespace Nemonuri.Trees;

using Abstractions;

/// <summary>The default implementation of <see cref="ITreeWalker{_,_,_,_}"/></summary>
/// <inheritdoc cref="ITreeWalker{_,_,_,_}" path="/typeparam"/>
public class TreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation>
{
    private readonly IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation> _aggregator3D;
    private readonly IChildrenProvider<TElement> _childrenProvider;
    private readonly IAncestorConverter<TElement, TAncestor> _ancestorConverter;

    public TreeWalker
    (
        IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IChildrenProvider<TElement> childrenProvider,
        IAncestorConverter<TElement, TAncestor> ancestorConverter
    )
    {
        Guard.IsNotNull(aggregator3D);
        Guard.IsNotNull(childrenProvider);
        Guard.IsNotNull(ancestorConverter);

        _aggregator3D = aggregator3D;
        _childrenProvider = childrenProvider;
        _ancestorConverter = ancestorConverter;
    }

    public TreeWalker
    (
        IAggregator<TAncestor, TAncestorsAggregation> ancestorAggregator,
        IContextualAggregator2D<TElement, TAggregation, TAncestorsAggregation> elementAggregator,
        IChildrenProvider<TElement> childrenProvider,
        IAncestorConverter<TElement, TAncestor> ancestorConverter
    ) :
        this
        (
            new Aggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
            (
                ancestorAggregator, elementAggregator
            ),
            childrenProvider, ancestorConverter
        )
    { }

    public TreeWalker
    (
        Func<TAncestorsAggregation> initialAncestorsAggregationImplementation,
        Func<TAncestorsAggregation, TAncestor, TAncestorsAggregation> aggregateAncestorImplementation,
        Func<TAggregation> initialAggregationImplementation,
        Func<TAncestorsAggregation, TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation,
        Func<TElement, IEnumerable<TElement>> getChildrenImplementation,
        Func<TElement, int?, TAncestor> convertToAncestorImplementation
    ) :
        this
        (
            new AdHocAggregator<TAncestor, TAncestorsAggregation>(initialAncestorsAggregationImplementation, aggregateAncestorImplementation),
            new AdHocContextualAggregator2D<TElement, TAggregation, TAncestorsAggregation>(initialAggregationImplementation, aggregateImplementation),
            new AdHocChildrenProvider<TElement>(getChildrenImplementation),
            new AdHocAncestorConverter<TElement, TAncestor>(convertToAncestorImplementation)
        )
    { }

    public TAncestorsAggregation InitialAncestorsAggregation => _aggregator3D.InitialAncestorsAggregation;

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _aggregator3D.AggregateAncestor(ancestorsAggregation, ancestor);

    public TAggregation InitialAggregation => _aggregator3D.InitialAggregation;

    public TAggregation Aggregate(TAncestorsAggregation ancestorsAggregation, TAggregation siblingsAggregation, TAggregation childrenAggregation, TElement element) =>
        _aggregator3D.Aggregate(ancestorsAggregation, siblingsAggregation, childrenAggregation, element);

    public IEnumerable<TElement> GetChildren(TElement source) => _childrenProvider.GetChildren(source);

    public TAncestor ConvertToAncestor(TElement element, int? elementIndex) =>
        _ancestorConverter.ConvertToAncestor(element, elementIndex);
}
