namespace Nemonuri.Graphs.Infrastructure;

public interface ISuccessorGraph<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectSuccessorArrows(TNode node);
}

public interface IDynamicSuccessorGraph<TNode, TNodeSet, TEffect>
    where TNodeSet : IEnumerable<TNode>
{
    TEffect AddDirectSuccessors(TNode node, TNodeSet successors);
}

public interface IPredecessorGraph<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectPredecessorArrows(TNode node);
}

public interface IDynamicPredecessorGraph<TNode, TNodeSet, TEffect>
    where TNodeSet : IEnumerable<TNode>
{
    TEffect AddDirectPredecessors(TNode node, TNodeSet predecessors);
}

public interface ISuccessorGraphNode<TNode, TArrow, TArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : ISuccessorGraphNode<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectSuccessorArrows();
}

public interface ISuccessorGraphNodeFactory<TContext, TNode, TNodeSet, TArrow, TArrowSet>
    where TNode : ISuccessorGraphNode<TNode, TArrow, TArrowSet>
    where TNodeSet : IEnumerable<TNode>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TNode Create(TContext context, TNodeSet successors);
}

public interface IPredecessorGraphNode<TNode, TArrow, TArrowSet> :
    ISupportFixedPoint<TNode>
    where TNode : IPredecessorGraphNode<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TArrowSet GetDirectPredecessorArrows();
}

public interface IPredecessorGraphNodeFactory<TContext, TNode, TNodeSet, TArrow, TArrowSet>
    where TNode : IPredecessorGraphNode<TNode, TArrow, TArrowSet>
    where TNodeSet : IEnumerable<TNode>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
    TNode Create(TContext context, TNodeSet predecessors);
}

public interface IDigraphNode<TNode, TArrow, TArrowSet> :
    ISuccessorGraphNode<TNode, TArrow, TArrowSet>,
    IPredecessorGraphNode<TNode, TArrow, TArrowSet>
    where TNode : IDigraphNode<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
}

public interface IDigraphNodeFactory<TContext, TNode, TNodeSet, TArrow, TArrowSet>
    where TNode : IDigraphNode<TNode, TArrow, TArrowSet>
    where TNodeSet : IEnumerable<TNode>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{ 
    TNode Create(TContext context, TNodeSet predecessors, TNodeSet successors);
}

