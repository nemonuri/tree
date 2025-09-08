using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static TAggregation Aggregate
    <TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        TTree tree,
        ITreeAggregator<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker
    )
        where TTree : ITree<TElement, TTree>
#if NET9_0_OR_GREATER
        , allows ref struct
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(tree is not null);

        return AggregatingTheory.Aggregate(treeWalker, TrivialChildrenProvider<TTree>.BoxedInstance, treeWalker, tree);
    }

    /// <inheritdoc 
    ///     cref="AggregatingTheory.Aggregate{_,_,_,_,_,_,_}(_,_,_,_)" 
    ///     path="/*[not(self::param)]"/>
    public static TAggregation Aggregate
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITree<TElement> tree,
        ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        return Aggregate(tree, treeWalker);
    }

}
