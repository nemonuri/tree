#if false

namespace Nemonuri.Trees;

public class IndexedTreeNodesFromRootAggregator<TTreeNode> :
    IAggregator<IndexedNode<TTreeNode>, IndexedTreeNodesFromRoot<TTreeNode>>
{
    public IndexedTreeNodesFromRootAggregator() { }

    public IndexedTreeNodesFromRoot<TTreeNode> InitialAggregation => IndexedTreeNodesFromRoot<TTreeNode>.Empty;

    public bool TryAggregate
    (
        IndexedTreeNodesFromRoot<TTreeNode> siblingsAggregated,
        IndexedNode<TTreeNode> source,
        [NotNullWhen(true)] out IndexedTreeNodesFromRoot<TTreeNode>? aggregated
    )
    {
        return siblingsAggregated.TryAppend(source, out aggregated);
    }
}

#endif