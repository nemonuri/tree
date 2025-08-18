

namespace Nemonuri.Trees;

public class RoseTreeNodeBasedRootOriginatedTreeNodeWithIndexSequenceAggregator<T> :
    IAggregator<TreeNodeWithIndex<RoseTreeNode<T>>, RootOriginatedTreeNodeWithIndexSequence<RoseTreeNode<T>>>
{
    private readonly RootOriginatedTreeNodeWithIndexSequenceAggregator<RoseTreeNode<T>> _internalSource;

    public RoseTreeNodeBasedRootOriginatedTreeNodeWithIndexSequenceAggregator()
    {
        _internalSource = new();
    }

    public RootOriginatedTreeNodeWithIndexSequence<RoseTreeNode<T>> DefaultAggregated => _internalSource.DefaultAggregated;

    public bool TryAggregate(RootOriginatedTreeNodeWithIndexSequence<RoseTreeNode<T>> siblingsAggregated, TreeNodeWithIndex<RoseTreeNode<T>> source, [NotNullWhen(true)] out RootOriginatedTreeNodeWithIndexSequence<RoseTreeNode<T>>? aggregated)
    {
        return _internalSource.TryAggregate(siblingsAggregated, source, out aggregated);
    }
}