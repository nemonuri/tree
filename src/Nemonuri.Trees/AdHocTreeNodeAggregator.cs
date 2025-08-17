

namespace Nemonuri.Trees;

public class AdHocTreeNodeAggregator<TTreeNode, TTarget> :
    IAggregator2DWithContext<TTreeNode, TTarget, DefaultFromRootContextAggregator<TTreeNode>>
{
    public Func<TTarget> DefaultSeedProvider { get; }

    public TryAggregator2DWithContext<TTreeNode, TTarget, DefaultFromRootContextAggregator<TTreeNode>> TryAggregator { get; }

    public AdHocTreeNodeAggregator
    (
        Func<TTarget> defaultSeedProvider,
        TryAggregator2DWithContext<TTreeNode, TTarget, DefaultFromRootContextAggregator<TTreeNode>> tryAggregator
    )
    {
        Debug.Assert(defaultSeedProvider is not null);
        Debug.Assert(tryAggregator is not null);

        DefaultSeedProvider = defaultSeedProvider;
        TryAggregator = tryAggregator;
    }

    public AdHocTreeNodeAggregator
    (
        Func<TTarget> defaultSeedProvider,
        OptionalAggregator2DWithContext<TTreeNode, TTarget, DefaultFromRootContextAggregator<TTreeNode>> optionalAggregator
    )
    : this(defaultSeedProvider, optionalAggregator.ToTryAggregator2DWithContext())
    { }

    public TTarget DefaultAggregated => DefaultSeedProvider.Invoke();

    public bool TryAggregate
    (
        DefaultFromRootContextAggregator<TTreeNode> context,
        TTarget siblingsAggregated,
        TTarget childrenAggregated,
        TTreeNode source,
        [NotNullWhen(true)] out TTarget? aggregated
    ) =>
    TryAggregator.Invoke(context, siblingsAggregated, childrenAggregated, source, out aggregated);
}
