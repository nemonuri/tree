using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.StringNodes;

public record StringNode(string Value, StringNode[] Children)
{
    public static implicit operator StringNode(string v) => new(v, []);
    public static implicit operator StringNode((string, StringNode[]) v) => new(v.Item1, v.Item2);
}

public readonly record struct StringNodeArrow(StringNode Tail, StringNode Head) : IArrow<StringNode, StringNode>;

public class StringNodeOutArrowSet : IOutArrowSet<StringNodeArrow, StringNode, StringNode>
{
    public StringNodeOutArrowSet(StringNode commonTail, ImmutableList<StringNode> heads)
    {
        CommonTail = commonTail;
        Heads = heads;
    }

    public StringNodeOutArrowSet(StringNode commonTail) : this(commonTail, [.. commonTail.Children])
    { }

    public StringNode CommonTail { get; }

    public ImmutableList<StringNode> Heads { get; }

    public bool TryGetCommonTail([NotNullWhen(true)] out StringNode? commonTail) =>
        (commonTail = CommonTail) is not null;

    public IEnumerator<StringNodeArrow> GetEnumerator()
    {
        foreach (var head in Heads)
        {
            yield return new(CommonTail, head);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
