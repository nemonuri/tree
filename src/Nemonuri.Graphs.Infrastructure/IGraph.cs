using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;


public interface IGraph<TNode, TNodeSet>
    where TNodeSet : IEnumerable<TNode>
{
    TNodeSet GetAdjacentNodes(TNode node);
}

public interface IDigraph
<
    TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet
> :
    IPredecessorGraph<TTail, TNode, TInArrow, TInArrowSet>,
    ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IEnumerable<TInArrow>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IEnumerable<TOutArrow>
{
}

public interface IDynamicDigraph
<
    TContext, TNode, TTail, TTailSet, THead, THeadSet, TEffect
> :
    IDynamicPredecessorGraph<TTail, TNode, TTailSet, TEffect>,
    IDynamicSuccessorGraph<TNode, THead, THeadSet, TEffect>
    where TTailSet : IEnumerable<TTail>
    where THeadSet : IEnumerable<THead>
{
}

public interface IVisitor<TNode, TNodeInfo, TEffect>
{
    TNode? CurrentNode { get; }
    TNodeInfo? CurrentNodeInfo { get; }

    [MemberNotNullWhen(true, nameof(CurrentNode))]
    bool MoveForward(out TEffect? effect);

    [MemberNotNullWhen(true, nameof(CurrentNode))]
    bool MoveBackward(out TEffect? effect);
}
