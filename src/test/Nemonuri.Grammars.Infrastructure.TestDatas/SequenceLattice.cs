namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceLattice<T> : ILattice<int>
{
    public SequenceLattice(IReadOnlyList<T> canon, int lowerBound, int upperBound)
    {
        Canon = canon;
        GreatestLowerBound = lowerBound;
        LeastUpperBound = upperBound;
    }

    public SequenceLattice(IReadOnlyList<T> canon, int upperBound) : this(canon, canon.Count, upperBound)
    { }

    public SequenceLattice(IReadOnlyList<T> canon) : this(canon, 0)
    { }

    public IReadOnlyList<T> Canon { get; }

    public int LeastUpperBound { get; }

    public int GreatestLowerBound { get; }
}
