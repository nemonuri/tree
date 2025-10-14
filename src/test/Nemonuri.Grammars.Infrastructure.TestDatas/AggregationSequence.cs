using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public readonly struct AggregationSequence : IEquatable<AggregationSequence>
{
    public AggregationSequence(ImmutableList<AggregationUnit> aggregationSequence)
    {
        InternalAggregationSequence = aggregationSequence;
    }

    public ImmutableList<AggregationUnit> InternalAggregationSequence { get; }

    public bool Equals(AggregationSequence other)
    {
        return
            InternalAggregationSequence.SequenceEqual(other.InternalAggregationSequence);
    }

    public override bool Equals(object? obj) => obj is AggregationSequence v && Equals(v);

    public override int GetHashCode()
    {
        return InternalAggregationSequence.Aggregate(new HashCode(), static (a, e) => { a.Add(e); return a; }, static h => h.ToHashCode());
    }

    public override string ToString()
    {
        return $"[{string.Join(',', InternalAggregationSequence)}]";
    }

    public AggregationSequence Add(AggregationUnit unit)
    {
        return new(InternalAggregationSequence.Add(unit));
    }

    public AggregationSequence SetLast(AggregationUnit unit)
    {
        return new(InternalAggregationSequence.SetItem(InternalAggregationSequence.Count - 1, unit));
    }

    public string DebuggerDisplay => $"[{string.Join(',', InternalAggregationSequence.Select(static a => a.DebuggerDisplay))}]";

    public string GetSliceDisplay<T>(IReadOnlyList<T> canon)
    {
        StringBuilder sb = new();
        foreach (var aggregationUnit in InternalAggregationSequence)
        {
            sb.Append(aggregationUnit.GetSliceDisplay(canon));
        }
        return sb.ToString();
    }
}


