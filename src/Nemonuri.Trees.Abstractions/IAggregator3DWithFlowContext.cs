
namespace Nemonuri.Trees;

public interface IAggregator3DWithFlowContext
<TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
    where TFlowContext : allows ref struct
#endif
{
    TFlowContext InitialFlowContext { get; }

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
        TAggregation childrenAggregation,
        TElement element,
        scoped ref TFlowContext flowContext
    );
}
