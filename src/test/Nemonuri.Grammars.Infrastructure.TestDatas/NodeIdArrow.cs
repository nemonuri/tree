using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class NodeIdArrow : IArrow<NodeId, NodeId>
{
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> Map { get; }
    public NodeArrowId NodeArrowId { get; }

    public NodeIdArrow(IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> map, NodeArrowId nodeArrowId)
    {
        Map = map;
        NodeArrowId = nodeArrowId;
    }


    public NodeId Tail => Map[NodeArrowId].Tail;

    public NodeId Head => Map[NodeArrowId].Head;
}


public delegate ScanResult<int, TExtra> Scanner<T, TExtra>(NodeArrowId arrowId, SequenceIdeal<T> ideal);

public record NodeArrowIdToNodeIdMapItem(NodeArrowId NodeArrowId, NodeId Tail, NodeId Head);

public record NodeArrowIdToScanPremiseMapItem<T, TExtra>(NodeArrowId NodeArrowId, IScanPremise<T, TExtra> Premise);

public class NodeIdOutArrowSet : IOutArrowSet<NodeIdArrow, NodeId, NodeId>
{
    public bool TryGetCommonTail([NotNullWhen(true)] out NodeId commonTail)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<NodeIdArrow> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
