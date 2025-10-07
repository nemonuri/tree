using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceIdealPremise<T>() : IIdealPremise<int, IReadOnlyList<T>, SequenceIdeal<T>>
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


