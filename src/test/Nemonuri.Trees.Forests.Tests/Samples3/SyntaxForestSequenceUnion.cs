using System.Collections;
using System.Collections.Immutable;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

public class SyntaxForestSequenceUnion : IReadOnlyList<SyntaxForestSequence>, ISupportConsumableSourceVarientSet
{ 
    private readonly ImmutableList<SyntaxForestSequence> _internalList;
    private ConsumableSourceVarientSet? _consumableSourceVarients;

    public SyntaxForestSequenceUnion(ImmutableList<SyntaxForestSequence> internalList)
    {
        _internalList = internalList;
    }

    public ImmutableList<SyntaxForestSequence> InternalList => _internalList;

    public SyntaxForestSequence this[int index] => (_internalList)[index];

    public int Count => _internalList.Count;

    public IEnumerator<SyntaxForestSequence> GetEnumerator()
    {
        return (_internalList).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (_internalList).GetEnumerator();
    }

    public ConsumableSourceVarientSet ConsumableSourceVarientSet => _consumableSourceVarients ??= ConsumableSourceVarientSetTheory.Aggregate(_internalList);
}