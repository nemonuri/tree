using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public readonly record struct IndexedIntNodeArrow(IntNode Tail, IntNode Head, int Index) : IArrow<IntNode, IntNode>;

public class IndexedIntNodeOutArrowSet : IOutArrowSet<IndexedIntNodeArrow, IntNode, IntNode>, IReadOnlyCollection<IndexedIntNodeArrow>
{
    public IndexedIntNodeOutArrowSet(IntNode commonTail, ImmutableList<IntNode> heads)
    {
        CommonTail = commonTail;
        Heads = heads;
    }

    public IndexedIntNodeOutArrowSet(IntNode commonTail) : this(commonTail, [.. commonTail.Children])
    { }

    public IntNode CommonTail { get; }

    public ImmutableList<IntNode> Heads { get; }

    public bool TryGetCommonTail([NotNullWhen(true)] out IntNode? commonTail) =>
        (commonTail = CommonTail) is not null;

    public IEnumerator<IndexedIntNodeArrow> GetEnumerator()
    {
        int index = 0;
        foreach (var head in Heads)
        {
            yield return new(CommonTail, head, index);
            index++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => Heads.Count;
}