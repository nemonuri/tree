
namespace Nemonuri.Trees;

// Note:
// - IAggregator4D 가 맞는 네이밍이야? 뭔가 이상한데?
// - IAggregator3D 를, IAggregator2DWithAncestor 로 바꿔야 하는 거 아닌가...?

public interface IAggregator4D
<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
    TAncestorsAggregation InitialAncestorsAggregation { get; }

    TAncestorsAggregation AggregateAncestor
    (
        TAncestorsAggregation ancestorsAggregation,
        TAncestor ancestor
    );

    TAggregation InitialAggregation { get; }

    TAggregation Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregation siblingsAggregation,
        TAggregation childrenFromAxis0Aggregation,
        TAggregation childrenFromAxis1Aggregation,
        TElement element
    );
}