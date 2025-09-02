
namespace Nemonuri.Trees;

public readonly struct ElementIndexAggregator : IAggregator<int?, ImmutableList<int>>
{
    public static readonly IAggregator<int?, ImmutableList<int>> BoxedInstance = (ElementIndexAggregator)default;

    public ElementIndexAggregator() { }

    public ImmutableList<int> InitialAggregation => [];

    public ImmutableList<int> Aggregate(ImmutableList<int> aggregation, int? element)
    {
        return element is int i ? aggregation.Add(i) : aggregation;
    }
}