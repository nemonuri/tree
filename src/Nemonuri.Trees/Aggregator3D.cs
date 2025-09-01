
namespace Nemonuri.Trees;

/// <inheritdoc cref="IAggregator3D{_,_,_,_}" path="/typeparam" />
public class Aggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
{
    private readonly IAggregator<TAncestor, TAncestorsAggregation> _ancestorAggregator;
    private readonly IContextualAggregator2D<TElement, TAggregation, TAncestorsAggregation> _elementAggregator;

    public Aggregator3D
    (
        IAggregator<TAncestor, TAncestorsAggregation> ancestorAggregator,
        IContextualAggregator2D<TElement, TAggregation, TAncestorsAggregation> elementAggregator
    )
    {
        Guard.IsNotNull(ancestorAggregator);
        Guard.IsNotNull(elementAggregator);

        _ancestorAggregator = ancestorAggregator;
        _elementAggregator = elementAggregator;
    }

    public TAncestorsAggregation InitialAncestorsAggregation => _ancestorAggregator.InitialAggregation;

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _ancestorAggregator.Aggregate(ancestorsAggregation, ancestor);

    public TAggregation InitialAggregation => _elementAggregator.InitialAggregation;

    public TAggregation Aggregate(TAncestorsAggregation ancestorsAggregation, TAggregation siblingsAggregation, TAggregation childrenAggregation, TElement element) =>
        _elementAggregator.Aggregate(ancestorsAggregation, siblingsAggregation, childrenAggregation, element);
}
