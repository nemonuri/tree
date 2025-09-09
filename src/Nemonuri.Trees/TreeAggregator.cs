
namespace Nemonuri.Trees;

/// <summary>The default implementation of <see cref="ITreeAggregator{_,_,_,_}"/></summary>
/// <inheritdoc cref="ITreeAggregator{_,_,_,_}" path="/typeparam"/>
internal class TreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation> :
    ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation>
    where TTree : ITree<TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif    
{
    private readonly IAggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation> _aggregator3D;
    private readonly IAncestorConverter<TTree, TAncestor> _ancestorConverter;

    public TreeAggregator
    (
        IAggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IAncestorConverter<TTree, TAncestor> ancestorConverter
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

    public TAggregation Aggregate(TAncestorsAggregation ancestorsAggregation, TAggregation siblingsAggregation, TAggregation childrenAggregation, TTree element) =>
        _aggregator3D.Aggregate(ancestorsAggregation, siblingsAggregation, childrenAggregation, element);

    public TAncestor ConvertToAncestor(TTree element, int? elementIndex) => _ancestorConverter.ConvertToAncestor(element, elementIndex);
}
