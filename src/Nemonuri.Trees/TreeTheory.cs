using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    /// <inheritdoc 
    ///     cref="AggregatingTheory.Aggregate{_,_,_,_,_,_,_}(_,_,_,_)" 
    ///     path="/*[not(self::param)]"/>
    public static TAggregation Aggregate
    <TTree, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITree<TTree> tree,
        ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation> treeAggregator
    )
        where TTree : ITree<TTree>
#if NET9_0_OR_GREATER
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(tree is not null);

        return AggregatingTheory.Aggregate(treeAggregator, TrivialChildrenProvider<TTree>.BoxedInstance, treeAggregator, (TTree)tree);
    }

    public static IEnumerable<TTree> CreateBoundChildren<TTree, TBinder>
    (
        IEnumerable<TTree> unboundChildren,
        TBinder binder
    )
        where TTree : IBoundableTree<TTree, TBinder>
        where TBinder : ITree<TBinder>
    {
        return unboundChildren.Select(child => child.BindParent(binder));
    }

}
