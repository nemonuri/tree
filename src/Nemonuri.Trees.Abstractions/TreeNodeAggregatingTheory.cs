
namespace Nemonuri.Trees;

public static class TreeNodeAggregatingTheory
{
    public static bool TryAggregateAsRoot<TTreeNode, TTarget, TContext>
    (
        IAggregator<TreeNodeWithIndex<TTreeNode>, TContext> rootOriginatedContextAggregator,
        IAggregator2DWithContext<TTreeNode, TTarget, TContext> treeNodeAggregator,
        IChildrenProvider<TTreeNode> childrenProvider,
        TTreeNode treeNode,
        [NotNullWhen(true)] out TTarget? aggregated
    )
    {
        Debug.Assert(rootOriginatedContextAggregator is not null);
        Debug.Assert(treeNodeAggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(treeNode is not null);

        if
        (
            !rootOriginatedContextAggregator.TryAggregate
            (
                rootOriginatedContextAggregator.DefaultAggregated,
                new TreeNodeWithIndex<TTreeNode>(treeNode),
                out TContext? context
            )
        )
        { goto Fail; }

        if
        (
            !TryAggregateChildren
            (
                rootOriginatedContextAggregator,
                treeNodeAggregator,
                childrenProvider,
                context,
                treeNode,
                out TTarget? childrenAggregated
            )
        )
        { goto Fail; }

        if
        (
            !treeNodeAggregator.TryAggregate
            (
                context,
                treeNodeAggregator.DefaultAggregated,
                childrenAggregated,
                treeNode,
                out aggregated
            )
        )
        { goto Fail; }

        return true;

    Fail:
        aggregated = default;
        return false;
    }

    public static bool TryAggregateChildren<TTreeNode, TTarget, TContext>
    (
        IAggregator<TreeNodeWithIndex<TTreeNode>, TContext> rootOriginatedContextAggregator,
        IAggregator2DWithContext<TTreeNode, TTarget, TContext> treeNodeAggregator,
        IChildrenProvider<TTreeNode> childrenProvider,
        TContext context,
        TTreeNode treeNode,
        [NotNullWhen(true)] out TTarget? childrenAggregated
    )
    {
        Debug.Assert(rootOriginatedContextAggregator is not null);
        Debug.Assert(treeNodeAggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(context is not null);
        Debug.Assert(treeNode is not null);

        childrenAggregated = treeNodeAggregator.DefaultAggregated;

        int childIndex = 0;
        foreach (var child in childrenProvider.GetChildren(treeNode))
        {
            if
            (
                !rootOriginatedContextAggregator.TryAggregate
                (
                    context,
                    new TreeNodeWithIndex<TTreeNode>(child, childIndex),
                    out TContext? childContext
                )
            )
            { goto Fail; }

            if
            (
                !TryAggregateChildren
                (
                    rootOriginatedContextAggregator,
                    treeNodeAggregator,
                    childrenProvider,
                    childContext,
                    child,
                    out TTarget? grandChildrenAggregated
                )
            )
            { goto Fail; }

            if
            (
                !treeNodeAggregator.TryAggregate
                (
                    childContext,
                    childrenAggregated,
                    grandChildrenAggregated,
                    child,
                    out childrenAggregated
                )
            )
            { goto Fail; }

            childIndex++;
        }

        return childrenAggregated is not null;

    Fail:
        childrenAggregated = default;
        return false;
    }
}