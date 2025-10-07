using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

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
