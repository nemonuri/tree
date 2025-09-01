namespace Nemonuri.Trees;

public static class TreeTheory
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
}
