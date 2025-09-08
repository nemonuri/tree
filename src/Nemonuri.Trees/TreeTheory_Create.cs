namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ITree<TElement> CreateTopDown<TElement>
    (
        TElement value,
        IChildrenProvider<TElement> childrenProvider,
        ITopDownTreeFactory<TElement> childToTreeFactory,
        ITree<TElement>? parent
    )
    {
        return new TopDownTree<TElement>(value, childrenProvider, childToTreeFactory, parent);
    }

    public static ITree<TElement> CreateTopDown<TElement>
    (
        TElement value,
        IChildrenProvider<TElement> childrenProvider,
        ITree<TElement>? parent
    ) =>
    CreateTopDown(value, childrenProvider, TopDownTreeFactory<TElement>.Instance, parent);

    public static ITree<TElement> CreateLeaf<TElement>(TElement value)
    {
        return new LeafTree<TElement>(value);
    }

    public static IEnumerable<ITree<TElement>> CreateLeafs<TElement>(IEnumerable<TElement>? values)
    {
        if (values is null) { yield break; }
        foreach (var value in values)
        {
            yield return CreateLeaf(value);
        }
    }

    public static ITree<TElement> CreateBottomUp<TElement>
    (
        TElement value,
        IEnumerable<ITree<TElement>>? children,
        IParentTreeBinder<TElement> parentTreeBinder
    )
    {
        return new BottomUpTree<TElement>(value, children, parentTreeBinder);
    }

    public static ITree<TElement> CreateBottomUp<TElement>
    (
        TElement value,
        IEnumerable<ITree<TElement>>? children
    ) =>
    CreateBottomUp(value, children, ParentTreeBinder<TElement>.Instance);

    public static ITree<TElement> BindParent<TElement>
    (
        ITree<TElement> source,
        ITree<TElement>? parent
    )
    {
        ITree<TElement> ensuredSource =
        (
            source is ISupportInternalSource<ITree<TElement>> supportInternalSource &&
            supportInternalSource.GetInternalSource() is { } internalSource
        ) ? internalSource : source;

        return new ParentBoundTree<TElement>(ensuredSource, parent);
    }

    public static ITree<TElement> CreateLazyBottomUp<TElement>
    (
        ILazyTreeValueEvaluator<TElement> lazyValue,
        IEnumerable<ITree<TElement>>? children,
        IParentTreeBinder<TElement> parentTreeBinder
    )
    {
        return new LazyBottomUpTree<TElement>(lazyValue, children, parentTreeBinder);
    }
}