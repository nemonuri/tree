
namespace Nemonuri.Trees;

public class DefaultRoseTreeNodeBasedFromRootContextAggregator<T> :
    IAggregator<TreeNodeWithIndex<RoseTreeNode<T>>, DefaultFromRootContext<RoseTreeNode<T>>>
{
    private readonly DefaultFromRootContextAggregator<RoseTreeNode<T>> _internalSource;

    public DefaultRoseTreeNodeBasedFromRootContextAggregator()
    {
        _internalSource = new();
    }

    public DefaultFromRootContext<RoseTreeNode<T>> DefaultAggregated => _internalSource.DefaultAggregated;

    public bool TryAggregate
    (
        DefaultFromRootContext<RoseTreeNode<T>> siblingsAggregated,
        TreeNodeWithIndex<RoseTreeNode<T>> source,
        [NotNullWhen(true)] out DefaultFromRootContext<RoseTreeNode<T>>? aggregated
    ) =>
    _internalSource.TryAggregate(siblingsAggregated, source, out aggregated);
}