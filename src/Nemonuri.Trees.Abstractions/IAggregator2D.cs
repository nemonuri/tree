
namespace Nemonuri.Trees;

public interface IAggregator2D<TSource, TTarget>
{
    TTarget DefaultAggregated { get; }

    bool TryAggregate
    (
        TTarget siblingsAggregated,
        TTarget childrenAggregated,
        TSource source,
        [NotNullWhen(true)] out TTarget? aggregated
    );
}


