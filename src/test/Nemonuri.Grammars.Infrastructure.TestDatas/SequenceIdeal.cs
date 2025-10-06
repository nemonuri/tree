namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceIdeal<T> : IIdeal<int>
{
    public SequenceIdeal(IReadOnlyList<T> sequence, int upperBound)
    {
        Sequence = sequence;
        UpperBound = upperBound;
    }

    public SequenceIdeal(IReadOnlyList<T> sequence) : this(sequence, 0)
    { }

    public IReadOnlyList<T> Sequence { get; }

    public int UpperBound { get; }
}
