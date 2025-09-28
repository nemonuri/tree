using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public record IntNode(int Value, IntNode[] Children)
{
    public static implicit operator IntNode(int v) => new(v, []);
    public static implicit operator IntNode((int, IntNode[]) v) => new(v.Item1, v.Item2);
}

public readonly record struct IntNodeArrow(IntNode Tail, IntNode Head) : IArrow<IntNode, IntNode>;

public class IntNodeOutArrowSet : IOutArrowSet<IntNodeArrow, IntNode, IntNode>
{
    public IntNodeOutArrowSet(IntNode commonTail, ImmutableList<IntNode> heads)
    {
        CommonTail = commonTail;
        Heads = heads;
    }

    public IntNodeOutArrowSet(IntNode commonTail) : this(commonTail, [..commonTail.Children])
    { }

    public IntNode CommonTail { get; }

    public ImmutableList<IntNode> Heads { get; }

    public bool TryGetCommonTail([NotNullWhen(true)] out IntNode? commonTail) =>
        (commonTail = CommonTail) is not null;

    public IEnumerator<IntNodeArrow> GetEnumerator()
    {
        foreach (var head in Heads)
        {
            yield return new(CommonTail, head);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
