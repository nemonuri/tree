using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SequenceIdealContext<T, TNode> : IIdealContext<TNode, int, SequenceLattice<T>>
    where TNode : notnull
{
    private ImmutableDictionary<TNode, int> _memo;

    public SequenceIdealContext(ImmutableDictionary<TNode, int> memo, SequenceLattice<T> ideal)
    {
        _memo = memo;
        CurrentIdeal = ideal;
    }

    public SequenceLattice<T> CurrentIdeal { get; set; }

    public bool TryGetMemoized(TNode key, [NotNullWhen(true)] out int value)
    {
        return _memo.TryGetValue(key, out value);
    }

    public void Memoize(TNode key, int value)
    {
        if (_memo.ContainsKey(key))
        {
            _memo = _memo.SetItem(key, value);
        }
        else
        {
            _memo = _memo.Add(key, value);
        }
    }

    public void ClearMemoized()
    {
        _memo = _memo.Clear();
    }

    public SequenceIdealContext<T, TNode> Clone() => new(_memo, CurrentIdeal);
}