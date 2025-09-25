using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;


public interface IGraph<TNode, TNodeSet>
    where TNodeSet : IEnumerable<TNode>
{
    TNodeSet GetAdjacentNodes(TNode node);
}

public interface IArrow<T>
{
    T Tail { get; }
    T Head { get; }
}

public interface IArrowToRelativePathMap<TNode, TArrow, TRelativePath>
    where TArrow : IArrow<TNode>
{
    TRelativePath GetRelativePath(TArrow arrow);
}

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
    TEffect AddDirectPredecessors(TNode node);
}

public interface IDigraph<TNode, TArrow, TArrowSet> :
    ISuccessorGraph<TNode, TArrow, TArrowSet>,
    IPredecessorGraph<TNode, TArrow, TArrowSet>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
{
}

public interface IDynamicDigraph<TNode, TNodeSet, TArrow, TArrowSet, TEffect> :
    ISuccessorGraph<TNode, TArrow, TArrowSet>,
    IPredecessorGraph<TNode, TArrow, TArrowSet>,
    IDynamicSuccessorGraph<TNode, TNodeSet, TEffect>,
    IDynamicPredecessorGraph<TNode, TNodeSet, TEffect>
    where TArrow : IArrow<TNode>
    where TArrowSet : IEnumerable<TArrow>
    where TNodeSet : IEnumerable<TNode>
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