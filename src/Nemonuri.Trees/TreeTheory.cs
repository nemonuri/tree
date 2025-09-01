using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    /// <inheritdoc 
    ///     cref="AggregatingTheory.Aggregate{_,_,_,_,_,_,_}(_,_,_,_)" 
    ///     path="/*[not(self::param)]"/>
    public static TAggregation Aggregate
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker,
        TElement element
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(treeWalker is not null);
        Debug.Assert(element is not null);

        return AggregatingTheory.Aggregate
        (
            treeWalker, treeWalker, treeWalker,
            element
        );
    }

    /// <inheritdoc 
    ///     cref="AggregatingTheory.Aggregate{_,_,_,_,_,_,_}(_,_,_,_)" 
    ///     path="/*[not(self::param)]"/>
    public static TAggregation Aggregate
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation> tree
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(tree is not null);

        return tree.TreeWalker.Aggregate(tree.Root);
    }

    public static IEnumerable<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>> GetChildren
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation> tree
    )
    {
        if (tree is ISupportChildren<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>> supportChildren)
        {
            return supportChildren.Children;
        }

        return tree.TreeWalker.GetChildren(tree.Root).Select
        (
            a => new Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
            (
                a, tree.TreeWalker
            )
        );
    }

    public static bool All<TElement>
    (
        TElement root,
        Func<TElement, bool> predicate,
        IChildrenProvider<TElement> childrenProvider
    )
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(predicate);
        Guard.IsNotNull(childrenProvider);

        var treeWalker = TreeWalkerTheory.Create
        (
            new AdHocAggregator2D<TElement, bool>
            (
                initialAggregationImplementation: static () => true,
                aggregateImplementation: (s, c, e) =>
                {
                    if ((s || c) is false) { return false; }
                    return predicate(root);
                }
            ),
            childrenProvider
        );

        return treeWalker.Aggregate(root);
    }

    public static bool All<TElement>(TElement root, Func<TElement, bool> predicate)
        where TElement : ISupportChildren<TElement>
    {
        return All(root, predicate, (TrivialChildrenProvider<TElement>)default);
    }

    public static bool Any<TElement>
    (
        TElement root,
        Func<TElement, bool> predicate,
        IChildrenProvider<TElement> childrenProvider
    )
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(predicate);
        Guard.IsNotNull(childrenProvider);

        var treeWalker = TreeWalkerTheory.Create
        (
            new AdHocAggregator2D<TElement, bool>
            (
                initialAggregationImplementation: static () => false,
                aggregateImplementation: (s, c, e) =>
                {
                    if ((s || c) is true) { return true; }
                    return predicate(root);
                }
            ),
            childrenProvider
        );

        return treeWalker.Aggregate(root);
    }

    public static bool Any<TElement>(TElement root, Func<TElement, bool> predicate)
        where TElement : ISupportChildren<TElement>
    {
        return Any(root, predicate, (TrivialChildrenProvider<TElement>)default);
    }

    


#if REF_TREE
    public static IEnumerable<TTree> GetChildren
    <
        TTree,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        this TTree tree,
        Func<TElement, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation>, TTree> treeFactory
    )
        where TTree : ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
                        , allows ref struct
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(treeFactory is not null);

        return new ResultOfGetChildren<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation>
                    (tree.TreeWalker.GetChildren(tree.Root), tree.TreeWalker, treeFactory);
    }
#endif
}
