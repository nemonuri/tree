
namespace Nemonuri.Trees;

public class DefaultFromRootContextAggregator<TTreeNode> :
    IAggregator<TreeNodeWithIndex<TTreeNode>, DefaultFromRootContext<TTreeNode>>
{
    public DefaultFromRootContextAggregator() { }

    public DefaultFromRootContext<TTreeNode> DefaultAggregated => DefaultFromRootContext<TTreeNode>.Empty;

    public bool TryAggregate
    (
        DefaultFromRootContext<TTreeNode> siblingsAggregated,
        TreeNodeWithIndex<TTreeNode> source,
        [NotNullWhen(true)] out DefaultFromRootContext<TTreeNode>? aggregated
    )
    {
        return siblingsAggregated.TryAppend(source, out aggregated);
    }
}
