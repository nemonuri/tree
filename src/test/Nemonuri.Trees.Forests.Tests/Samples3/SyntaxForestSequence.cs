using System.Collections;
using System.Collections.Immutable;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

public class SyntaxForestSequence : IReadOnlyList<SyntaxForest>, ISupportConsumableSourceVarientSet
{
    private readonly ImmutableList<SyntaxForest> _internalList;
    private ConsumableSourceVarientSet? _consumableSourceVarients;

    public SyntaxForestSequence(ImmutableList<SyntaxForest> internalList)
    {
        _internalList = internalList;
    }

    public ImmutableList<SyntaxForest> InternalList => _internalList;

    public SyntaxForest this[int index] => _internalList[index];

    public int Count => _internalList.Count;

    public IEnumerator<SyntaxForest> GetEnumerator()
    {
        return _internalList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _internalList.GetEnumerator();
    }

    public ConsumableSourceVarientSet ConsumableSourceVarientSet => _consumableSourceVarients ??= ConsumableSourceVarientSetTheory.Aggregate(_internalList);
}
