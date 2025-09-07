
namespace Nemonuri.Trees;

/// <summary>The default implementation of <see cref="ITreeAggregator{_,_,_,_}"/></summary>
/// <inheritdoc cref="ITreeAggregator{_,_,_,_}" path="/typeparam"/>
internal class TreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation>
{
    private readonly IAggregator3D<ITree<TElement>, TAggregation, TAncestor, TAncestorsAggregation> _aggregator3D;
    private readonly IAncestorConverter<ITree<TElement>, TAncestor> _ancestorConverter;

    public TreeAggregator
    (
        IAggregator3D<ITree<TElement>, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IAncestorConverter<ITree<TElement>, TAncestor> ancestorConverter
    )
    {
        Guard.IsNotNull(aggregator3D);
        Guard.IsNotNull(ancestorConverter);

        _aggregator3D = aggregator3D;
        _ancestorConverter = ancestorConverter;
    }

    public TAncestorsAggregation InitialAncestorsAggregation => _aggregator3D.InitialAncestorsAggregation;

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _aggregator3D.AggregateAncestor(ancestorsAggregation, ancestor);

    public TAggregation InitialAggregation => _aggregator3D.InitialAggregation;

    public TAggregation Aggregate(TAncestorsAggregation ancestorsAggregation, TAggregation siblingsAggregation, TAggregation childrenAggregation, ITree<TElement> element) =>
        _aggregator3D.Aggregate(ancestorsAggregation, siblingsAggregation, childrenAggregation, element);

    public TAncestor ConvertToAncestor(ITree<TElement> element, int? elementIndex) =>
        _ancestorConverter.ConvertToAncestor(element, elementIndex);
}
