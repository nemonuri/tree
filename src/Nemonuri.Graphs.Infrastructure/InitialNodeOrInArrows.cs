using System.Diagnostics.CodeAnalysis;
using SumSharp;

namespace Nemonuri.Graphs.Infrastructure;

[UnionCase("Error")]
[UnionCase("InitialNode", nameof(TNode), UnionCaseStorage.Inline)]
[UnionCase("InArrows", nameof(TInArrowSet), UnionCaseStorage.Inline)]
public partial struct InitialNodeOrInArrows<TTail, TNode, TInArrow, TInArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
{
    public bool TryGetNode([NotNullWhen(true)] out TNode? node)
    {
        if (IsInitialNode)
        {
            return (node = AsInitialNode) is not null;
        }
        else if (IsInArrows)
        {
            return AsInArrows.TryGetCommonHead(out node);
        }
        else
        {
            node = default;
            return false;
        }
    }

    public TNode GetNode() => TryGetNode(out var node) ? node : throw new InvalidOperationException();
}