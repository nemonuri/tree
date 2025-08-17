
namespace Nemonuri.Trees;

public interface IAggregator2DWithContext<TSource, TTarget, TContext>
{
    TTarget DefaultAggregated { get; }

    bool TryAggregate
    (
        TContext context,
        TTarget siblingsAggregated,
        TTarget childrenAggregated,
        TSource source,
        [NotNullWhen(true)] out TTarget? aggregated
    );
}
