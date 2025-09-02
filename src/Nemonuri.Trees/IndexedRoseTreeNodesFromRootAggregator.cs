#if false

namespace Nemonuri.Trees;

public class IndexedRoseTreeNodesFromRootAggregator<T> :
    IAggregator<IndexedNode<RoseTreeNode<T>>, IndexedTreeNodesFromRoot<RoseTreeNode<T>>>
{
    private readonly IndexedTreeNodesFromRootAggregator<RoseTreeNode<T>> _internalSource;

    public IndexedRoseTreeNodesFromRootAggregator()
    {
        _internalSource = new();
    }

    public IndexedTreeNodesFromRoot<RoseTreeNode<T>> InitialAggregation => _internalSource.InitialAggregation;

    public bool TryAggregate
    (
        IndexedTreeNodesFromRoot<RoseTreeNode<T>> siblingsAggregated,
        IndexedNode<RoseTreeNode<T>> source,
        [NotNullWhen(true)] out IndexedTreeNodesFromRoot<RoseTreeNode<T>>? aggregated
    )
    {
        return _internalSource.TryAggregate(siblingsAggregated, source, out aggregated);
    }
}
#endif