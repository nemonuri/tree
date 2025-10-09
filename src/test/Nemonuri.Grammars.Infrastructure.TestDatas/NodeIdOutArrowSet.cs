using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class NodeIdOutArrowSet : IOutArrowSet<NodeIdArrow, NodeId, NodeId>
{
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> Map { get; }

    public NodeId CommonTail { get; }

    private IEnumerable<NodeIdArrow>? _internalNodeIdArrows = null;

    internal IEnumerable<NodeIdArrow> InternalNodeIdArrows => _internalNodeIdArrows ??=
        Map.Where(kv => kv.Value.Tail == CommonTail)
            .Select(kv => new NodeIdArrow(Map, kv.Key));

    public NodeIdOutArrowSet
    (
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> map,
        NodeId commonTail
    )
    {
        Map = map;
        CommonTail = commonTail;
    }

    public bool TryGetCommonTail([NotNullWhen(true)] out NodeId commonTail)
    {
        commonTail = CommonTail; return true;
    }

    public IEnumerator<NodeIdArrow> GetEnumerator() => InternalNodeIdArrows.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
