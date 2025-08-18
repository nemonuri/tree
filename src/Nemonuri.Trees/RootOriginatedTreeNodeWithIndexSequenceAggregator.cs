
namespace Nemonuri.Trees;

public class RootOriginatedTreeNodeWithIndexSequenceAggregator<TTreeNode> :
    IAggregator<TreeNodeWithIndex<TTreeNode>, RootOriginatedTreeNodeWithIndexSequence<TTreeNode>>
{
    public RootOriginatedTreeNodeWithIndexSequenceAggregator() { }

    public RootOriginatedTreeNodeWithIndexSequence<TTreeNode> DefaultAggregated => RootOriginatedTreeNodeWithIndexSequence<TTreeNode>.Empty;

    public bool TryAggregate
    (
        RootOriginatedTreeNodeWithIndexSequence<TTreeNode> siblingsAggregated,
        TreeNodeWithIndex<TTreeNode> source,
        [NotNullWhen(true)] out RootOriginatedTreeNodeWithIndexSequence<TTreeNode>? aggregated
    )
    {
        return siblingsAggregated.TryAppend(source, out aggregated);
    }
}
