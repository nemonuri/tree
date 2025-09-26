namespace Nemonuri.Graphs.Infrastructure;

public interface ISuccessorGraph<TNode, THead, TArrow, TArrowSet>
    where TArrow : IArrow<TNode, THead>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectSuccessorArrows(TNode node);
}

public interface IDynamicSuccessorGraph<TNode, THead, THeadSet, TEffect>
    where THeadSet : IEnumerable<THead>
{
    TEffect AddDirectSuccessors(TNode node, THead successors);
}

public interface IPredecessorGraph<TTail, TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TTail, TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectPredecessorArrows(TNode node);
}

public interface IDynamicPredecessorGraph<TTail, TNode, TTailSet, TEffect>
    where TTailSet : IEnumerable<TTail>
{
    TEffect AddDirectPredecessors(TNode node, TTailSet predecessors);
}

public interface ISuccessorGraphNode<TNode, THead, TArrow, TArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : ISuccessorGraphNode<TNode, THead, TArrow, TArrowSet>
    where TArrow : IArrow<TNode, THead>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectSuccessorArrows();
}

public interface ISuccessorGraphNodeFactory<TContext, TTail, THead, THeadSet, TArrow, TArrowSet>
    where TTail : ISuccessorGraphNode<TTail, THead, TArrow, TArrowSet>
    where THeadSet : IEnumerable<THead>
    where TArrow : IArrow<TTail, THead>
    where TArrowSet : IEnumerable<TArrow>
{
    TTail Create(TContext context, THeadSet successors);
}

public interface IPredecessorGraphNode<TTail, TNode, TArrow, TArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : IPredecessorGraphNode<TTail, TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TTail, TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectPredecessorArrows();
}

public interface IPredecessorGraphNodeFactory<TContext, TTail, THead, TTailSet, TArrow, TArrowSet>
    where THead : IPredecessorGraphNode<TTail, THead, TArrow, TArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TArrow : IArrow<TTail, THead>
    where TArrowSet : IEnumerable<TArrow>
{
    THead Create(TContext context, TTailSet predecessors);
}

public interface IDigraphNode
<
    TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet
> :
    IPredecessorGraphNode<TTail, TNode, TInArrow, TInArrowSet>,
    ISuccessorGraphNode<TNode, THead, TOutArrow, TOutArrowSet>
    where TNode : IDigraphNode<TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IEnumerable<TInArrow>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IEnumerable<TOutArrow>
{
}

public interface IDigraphNodeFactory
<
    TContext, TNode, TTail, TTailSet, TInArrow, TInArrowSet, THead, THeadSet, TOutArrow, TOutArrowSet
>
    where TNode : IDigraphNode<TNode, TTail, TInArrow, TInArrowSet, THead, TOutArrow, TOutArrowSet>
    where TTailSet : IEnumerable<TTail>
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IEnumerable<TInArrow>
    where THeadSet : IEnumerable<THead>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IEnumerable<TOutArrow>
{
    TNode Create(TContext context, TTailSet predecessors, THeadSet successors);
}

