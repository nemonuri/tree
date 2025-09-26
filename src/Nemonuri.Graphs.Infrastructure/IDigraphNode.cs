namespace Nemonuri.Graphs.Infrastructure;

public interface ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, THead>
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
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
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
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, THead>
{
    TOutArrowSet GetDirectSuccessorArrows();
}

public interface ISuccessorGraphNodeFactory<TContext, TNode, THead, THeadSet, TOutArrow, TArrTOutArrowSetwSet>
    where TNode : ISuccessorGraphNode<TNode, THead, TOutArrow, TArrTOutArrowSetwSet>
    where THeadSet : IEnumerable<THead>
    where TOutArrow : IArrow<TNode, THead>
    where TArrTOutArrowSetwSet : IOutArrowSet<TOutArrow, TNode, THead>
{
    TNode Create(TContext context, THeadSet successors);
}

public interface IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
{
    TInArrowSet GetDirectPredecessorArrows();
}

public interface IPredecessorGraphNodeFactory<TContext, TTail, TNode, TTailSet, TInArrow, TInArrowSet>
    where TNode : IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
{
    TNode Create(TContext context, TTailSet predecessors);
}

public interface IDigraphNode
<
    TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet
> :
    IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>,
    ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet>
    where TNode : IDigraphNode<TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, THead>
{
}

public interface IDigraphNodeFactory
<
    TContext, TNode, TTail, TTailSet, TInArrow, TInArrowSet, THead, THeadSet, TOutArrow, TOutArrowSet
>
    where TNode : IDigraphNode<TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
    where THeadSet : IEnumerable<THead>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, THead>
{
    TNode Create(TContext context, TTailSet predecessors, THeadSet successors);
}

