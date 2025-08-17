
namespace Nemonuri.Trees;

public interface IAggregator<TSource, TTarget>
{
    TTarget DefaultAggregated { get; }
    
    bool TryAggregate
    (
        TTarget siblingsAggregated,
        TSource source,
        [NotNullWhen(true)] out TTarget? aggregated
    );
}
