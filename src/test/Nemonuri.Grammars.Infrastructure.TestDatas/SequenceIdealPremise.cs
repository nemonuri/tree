using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SequenceIdealPremise<T>() : IIdealPremise<int, IReadOnlyList<T>, SequenceIdeal<T>>
{
    public SequenceIdeal<T> CreateIdeal(IReadOnlyList<T> set, int upperBound)
    {
        return new(set, upperBound);
    }

    public IReadOnlyList<T> CastToSet(SequenceIdeal<T> ideal) => ideal.Sequence;

    public bool IsMinimalElement(IReadOnlyList<T> set, int item)
    {
        return set.Count == item;
    }

    public bool IsEmpty(IReadOnlyList<T> set)
    {
        return false;
    }

    public bool IsMember(IReadOnlyList<T> set, int item)
    {
        return (item >= 0) && (item <= set.Count);
    }

    public bool IsLesserThan(int less, int greater) => less > greater;
}

public class NodeIdArrow : IArrow<NodeId, NodeId>
{
    public NodeId Tail => throw new NotImplementedException();

    public NodeId Head => throw new NotImplementedException();
}


public class NafNodeIdArrowPremise<T>() : INafArrowPremise<NodeId, NodeIdArrow, int, SequenceIdeal<T>>
{
    public bool TryScan(NodeIdArrow arrow, SequenceIdeal<T> ideal, [NotNullWhen(true)] out int scannedUpperBound)
    {
        throw new NotImplementedException();
    }
}

