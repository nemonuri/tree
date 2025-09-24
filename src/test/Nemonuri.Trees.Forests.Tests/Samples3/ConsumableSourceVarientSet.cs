using System.Collections;
using System.Collections.Immutable;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

public class ConsumableSourceVarientSet : IReadOnlySet<ConsumableSourceVarient>, IReadOnlyList<ConsumableSourceVarient>
{
    private readonly ImmutableSortedSet<ConsumableSourceVarient> _internalSortedSet;

    public ImmutableSortedSet<ConsumableSourceVarient> InternalSortedSet => _internalSortedSet;

    public ConsumableSourceVarientSet(ImmutableSortedSet<ConsumableSourceVarient> internalSortedSet)
    {
        _internalSortedSet = internalSortedSet;
    }

    public bool Contains(ConsumableSourceVarient item)
    {
        return _internalSortedSet.Contains(item);
    }

    public bool IsProperSubsetOf(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.IsProperSubsetOf(other);
    }

    public bool IsProperSupersetOf(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.IsProperSupersetOf(other);
    }

    public bool IsSubsetOf(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.IsSubsetOf(other);
    }

    public bool IsSupersetOf(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.IsSupersetOf(other);
    }

    public bool Overlaps(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.Overlaps(other);
    }

    public bool SetEquals(IEnumerable<ConsumableSourceVarient> other)
    {
        return _internalSortedSet.SetEquals(other);
    }

    public int Count => _internalSortedSet.Count;

    public IEnumerator<ConsumableSourceVarient> GetEnumerator()
    {
        return (_internalSortedSet).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (_internalSortedSet).GetEnumerator();
    }

    public ConsumableSourceVarient this[int index] => (_internalSortedSet)[index];

    public ConsumableSourceVarientSet Union(IEnumerable<ConsumableSourceVarient> other)
    {
        var v1 = _internalSortedSet.Union(other);
        return new(v1);
    }
}

public static class ConsumableSourceVarientSetTheory
{
    public static ConsumableSourceVarientSet Aggregate(IEnumerable<ISupportConsumableSourceVarientSet> supporters)
    {
        return supporters.Aggregate
        (
            new ConsumableSourceVarientSet([]),
            static (a, e) => a.Union(e.ConsumableSourceVarientSet)
        );
    }
}