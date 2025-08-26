

namespace Nemonuri.Trees;

public class IndexedTreeNodesFromRootAggregator<TTreeNode> :
    IAggregator<IndexedTreeNode<TTreeNode>, IndexedTreeNodesFromRoot<TTreeNode>>
{
    public IndexedTreeNodesFromRootAggregator() { }

    public IndexedTreeNodesFromRoot<TTreeNode> DefaultAggregated => IndexedTreeNodesFromRoot<TTreeNode>.Empty;

    public bool TryAggregate
    (
        IndexedTreeNodesFromRoot<TTreeNode> siblingsAggregated,
        IndexedTreeNode<TTreeNode> source,
        [NotNullWhen(true)] out IndexedTreeNodesFromRoot<TTreeNode>? aggregated
    )
    {
        return siblingsAggregated.TryAppend(source, out aggregated);
    }
}
