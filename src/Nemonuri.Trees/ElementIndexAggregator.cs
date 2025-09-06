#if false

namespace Nemonuri.Trees;

public readonly struct ElementIndexAggregator : IAggregator<int?, IIndexPath>
{
    public static readonly IAggregator<int?, IIndexPath> BoxedInstance = (ElementIndexAggregator)default;

    public ElementIndexAggregator() { }

    public IIndexPath InitialAggregation => [];

    public IIndexPath Aggregate(IIndexPath aggregation, int? element)
    {
        return element is int i ? aggregation.Concat([i]) : aggregation;
    }
}

#endif