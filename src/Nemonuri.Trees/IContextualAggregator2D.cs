
namespace Nemonuri.Trees;

public interface IContextualAggregator2D
<TElement, TAggregation, TContext>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TContext : allows ref struct
#endif
{
    TAggregation InitialAggregation { get; }

    TAggregation Aggregate
    (
        TContext context,
        TAggregation siblingsAggregation,
        TAggregation childrenAggregation,
        TElement element
    );
}