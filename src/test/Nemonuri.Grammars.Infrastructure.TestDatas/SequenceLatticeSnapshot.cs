namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceLatticeSnapshot<T, TExtra>
{
    public SequenceLatticeSnapshot(SequenceLattice<T> sequence, TExtra extra)
    {
        Extra = extra;
        Sequence = sequence;
    }

    public TExtra Extra { get; }
    public SequenceLattice<T> Sequence { get; }
}
