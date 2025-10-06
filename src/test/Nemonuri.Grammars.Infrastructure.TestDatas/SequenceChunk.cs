namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public readonly struct SequenceChunk<T>
{
    public SequenceChunk(NodeId nodeId, IReadOnlyList<T> sequence)
    {
        NodeId = nodeId;
        Sequence = sequence;
    }

    public NodeId NodeId { get; }
    public IReadOnlyList<T> Sequence { get; }
}