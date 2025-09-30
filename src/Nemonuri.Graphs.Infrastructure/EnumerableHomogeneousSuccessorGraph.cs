using System.Collections;

namespace Nemonuri.Graphs.Infrastructure;

public class EnumerableHomogeneousSuccessorGraph<TGraph, TNode, TOutArrow, TOutArrowSet> :
    IEnumerable<TNode>
    where TGraph : ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    private readonly TGraph _graph;
    private readonly TNode _initialNode;

    public EnumerableHomogeneousSuccessorGraph(TGraph graph, TNode initialNode)
    {
        _graph = graph;
        _initialNode = initialNode;
    }

    public IEnumerator<TNode> GetEnumerator()
    {
        return new HomogeneousSuccessorGraphEnumerator<TGraph, TNode, TOutArrow, TOutArrowSet>(_graph, _initialNode);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
