
#if false
namespace Nemonuri.Trees;

public static class TreeNodeAggregatingTheory
{
    public static bool TryAggregateAsRoot<TTreeNode, TTarget, TContextFromRoot>
    (
        IAggregator<IndexedTreeNode<TTreeNode>, TContextFromRoot> contextFromRootAggregator,
        IAggregator2DWithContext<TTreeNode, TTarget, TContextFromRoot> treeNodeAggregator,
        IChildrenProvider<TTreeNode> childrenProvider,
        TTreeNode treeNode,
        [NotNullWhen(true)] out TTarget? aggregated
    )
    {
        Debug.Assert(contextFromRootAggregator is not null);
        Debug.Assert(treeNodeAggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(treeNode is not null);

        if
        (
            !contextFromRootAggregator.TryAggregate
            (
                contextFromRootAggregator.InitialAggregation,
                new IndexedTreeNode<TTreeNode>(treeNode),
                out TContextFromRoot? contextFromRoot
            )
        )
        { goto Fail; }

        if
        (
            !TryAggregateChildren
            (
                contextFromRootAggregator,
                treeNodeAggregator,
                childrenProvider,
                contextFromRoot,
                treeNode,
                out TTarget? childrenAggregated
            )
        )
        { goto Fail; }

        if
        (
            !treeNodeAggregator.TryAggregate
            (
                contextFromRoot,
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

    public static bool TryAggregateChildren<TTreeNode, TTarget, TContextFromRoot>
    (
        IAggregator<IndexedTreeNode<TTreeNode>, TContextFromRoot> contextFromRootAggregator,
        IAggregator2DWithContext<TTreeNode, TTarget, TContextFromRoot> treeNodeAggregator,
        IChildrenProvider<TTreeNode> childrenProvider,
        TContextFromRoot contextFromRoot,
        TTreeNode treeNode,
        [NotNullWhen(true)] out TTarget? childrenAggregated
    )
    {
        Debug.Assert(contextFromRootAggregator is not null);
        Debug.Assert(treeNodeAggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(contextFromRoot is not null);
        Debug.Assert(treeNode is not null);

        childrenAggregated = treeNodeAggregator.DefaultAggregated;

        int childIndex = 0;
        foreach (var child in childrenProvider.GetChildren(treeNode))
        {
            if
            (
                !contextFromRootAggregator.TryAggregate
                (
                    contextFromRoot,
                    new IndexedTreeNode<TTreeNode>(child, childIndex),
                    out TContextFromRoot? childContextFromRoot
                )
            )
            { goto Fail; }

            if
            (
                !TryAggregateChildren
                (
                    contextFromRootAggregator,
                    treeNodeAggregator,
                    childrenProvider,
                    childContextFromRoot,
                    child,
                    out TTarget? grandChildrenAggregated
                )
            )
            { goto Fail; }

            if
            (
                !treeNodeAggregator.TryAggregate
                (
                    childContextFromRoot,
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
#endif