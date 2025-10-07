using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceLatticePremise<T>() : ILatticePremise<int, IReadOnlyList<T>, SequenceLattice<T>>
{
    public SequenceLattice<T> CreateIdeal(IReadOnlyList<T> set, int upperBound)
    {
        return new(set, upperBound);
    }

    public SequenceLattice<T> CreateLattice(IReadOnlyList<T> set, int greatestLowerBound, int leastUpperBound)
    {
        return new(set, greatestLowerBound, leastUpperBound);
    }

    public IReadOnlyList<T> GetCanonicalSuperset(SequenceLattice<T> ideal) => ideal.Canon;

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

    public bool IsLesserOrEqualThan(int left, int right) => left >= right;

    public bool AreEqual(int left, int right) => left == right;
}


