
namespace Nemonuri.Trees.Abstractions;

public readonly struct NullContextualAggregator2D<TElement, TAggregation> :
    IContextualAggregator2D<TElement, TAggregation, NullAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    private readonly IAggregator2D<TElement, TAggregation> _internalAggregator2D;

    public NullContextualAggregator2D(IAggregator2D<TElement, TAggregation> internalAggregator2D)
    {
        Debug.Assert(internalAggregator2D is not null);

        _internalAggregator2D = internalAggregator2D;
    }

    public TAggregation InitialAggregation => _internalAggregator2D.InitialAggregation;

    public TAggregation Aggregate(NullAggregation context, TAggregation siblingsAggregation, TAggregation childrenAggregation, TElement element) =>
        _internalAggregator2D.Aggregate(siblingsAggregation, childrenAggregation, element);
}