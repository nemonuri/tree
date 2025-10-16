using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;


public interface IGraph<TNode, TNodeSet>
    where TNodeSet : IEnumerable<TNode>
{
    TNodeSet GetAdjacentNodes(TNode node);
}

public interface IDigraph
<
    TTail, TNode, THead, TInArrow, TOutArrow, TInArrowSet, TOutArrowSet
> :
    IPredecessorGraph<TTail, TNode, TInArrow, TInArrowSet>,
    ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
}

public interface IDynamicDigraph
<
    TTail, TNode, THead, TTailSet, THeadSet, TEffect
> :
    IDynamicPredecessorGraph<TTail, TNode, TTailSet, TEffect>,
    IDynamicSuccessorGraph<TNode, THead, THeadSet, TEffect>
    where TTailSet : IEnumerable<TTail>
    where THeadSet : IEnumerable<THead>
{
}
