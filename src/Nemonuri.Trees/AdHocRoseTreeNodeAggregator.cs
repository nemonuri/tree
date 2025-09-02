#if false

namespace Nemonuri.Trees;

public class AdHocRoseTreeNodeAggregator<T, TTarget> :
    IAggregator2DWithContext<RoseTreeNode<T>, TTarget, IndexedTreeNodesFromRoot<RoseTreeNode<T>>>
{
    private readonly AdHocTreeNodeAggregator<RoseTreeNode<T>, TTarget> _internalSource;

    public AdHocRoseTreeNodeAggregator
    (
        Func<TTarget> defaultSeedProvider,
        TryAggregator2DWithContext<RoseTreeNode<T>, TTarget, IndexedTreeNodesFromRoot<RoseTreeNode<T>>> tryAggregator
    )
    {
        _internalSource = new(defaultSeedProvider, tryAggregator);
    }

    public AdHocRoseTreeNodeAggregator
    (
        Func<TTarget> defaultSeedProvider,
        OptionalAggregator2DWithContext<RoseTreeNode<T>, TTarget, IndexedTreeNodesFromRoot<RoseTreeNode<T>>> optionalAggregator
    )
    {
        _internalSource = new(defaultSeedProvider, optionalAggregator);
    }

    public Func<TTarget> DefaultSeedProvider => _internalSource.DefaultSeedProvider;

    public TryAggregator2DWithContext<RoseTreeNode<T>, TTarget, IndexedTreeNodesFromRoot<RoseTreeNode<T>>> TryAggregator =>
        _internalSource.TryAggregator;

    public TTarget DefaultAggregated => _internalSource.DefaultAggregated;

    public bool TryAggregate
    (
        IndexedTreeNodesFromRoot<RoseTreeNode<T>> context,
        TTarget siblingsAggregated,
        TTarget childrenAggregated,
        RoseTreeNode<T> source,
        [NotNullWhen(true)] out TTarget? aggregated
    )
    {
        return _internalSource.TryAggregate(context, siblingsAggregated, childrenAggregated, source, out aggregated);
    }
}
#endif