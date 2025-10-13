using System.Collections.Immutable;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct AggregationSequenceUnion
{
    public AggregationSequenceUnion(ImmutableHashSet<AggregationSequence> internalAggregationSequenceUnion)
    {
        InternalAggregationSequenceUnion = internalAggregationSequenceUnion;
    }

    public ImmutableHashSet<AggregationSequence> InternalAggregationSequenceUnion { get; }

    public AggregationSequenceUnion Union(AggregationSequenceUnion union)
    {
        return new(InternalAggregationSequenceUnion.Union(union.InternalAggregationSequenceUnion));
    }
}


