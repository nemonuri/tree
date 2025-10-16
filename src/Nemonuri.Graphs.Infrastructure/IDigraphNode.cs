namespace Nemonuri.Graphs.Infrastructure;

public interface ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
    TOutArrowSet GetDirectSuccessorArrows(TNode node);
}

public interface IDynamicSuccessorGraph<TNode, THead, THeadSet, TEffect>
    where THeadSet : IEnumerable<THead>
{
    TEffect AddDirectSuccessors(TNode node, THead successors);
}

public interface IPredecessorGraph<TTail, TNode, TInArrow, TInArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
{
    TInArrowSet GetDirectPredecessorArrows(TNode node);
}

public interface IDynamicPredecessorGraph<TTail, TNode, TTailSet, TEffect>
    where TTailSet : IEnumerable<TTail>
{
    TEffect AddDirectPredecessors(TNode node, TTailSet predecessors);
}

public interface ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
    TOutArrowSet GetDirectSuccessorArrows();
}

public interface ISuccessorGraphNodeFactory<TNode, THead, TOutArrow, TOutArrowSet, THeadSet, TContext>
    where TNode : ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
    where THeadSet : IEnumerable<THead>
{
    TNode Create(THeadSet successors, TContext context);
}

public interface IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
{
    TInArrowSet GetDirectPredecessorArrows();
}

public interface IPredecessorGraphNodeFactory<TTail, TNode, TInArrow, TInArrowSet, TTailSet, TContext>
    where TNode : IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
{
    TNode Create(TTailSet predecessors, TContext context);
}

public interface IDigraphNode
<
    TTail, TNode, THead, TInArrow, TOutArrow, TInArrowSet, TOutArrowSet
> :
    IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>,
    ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet>
    where TNode : IDigraphNode<TTail, TNode, THead, TInArrow, TOutArrow, TInArrowSet, TOutArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
}

public interface IDigraphNodeFactory
<
    TTail, TNode, THead, TInArrow, TOutArrow, TInArrowSet, TOutArrowSet, TTailSet, THeadSet, TContext
>
    where TNode : IDigraphNode<TTail, TNode, THead, TInArrow, TOutArrow, TInArrowSet, TOutArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TTail, TNode, TInArrow>
    where THeadSet : IEnumerable<THead>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
    TNode Create(TTailSet predecessors, THeadSet successors, TContext context);
}

