using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{ 
    public static bool All<TTree>
    (
        this ITree<TTree> tree,
        Func<TTree, bool> predicate
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TTree, bool>
        (
            initialAggregationImplementation: static () => true,
            aggregateImplementation: (s, c, e) =>
            {
                if ((s && c) is false) { return false; }
                return predicate(e);
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static bool All<TValue, TTree>
    (
        this IRoseTree<TValue, TTree> tree,
        Func<TValue, bool> predicate
    )
        where TTree : IRoseTree<TValue, TTree>
    {
        return tree.All(WarpParameterInTree<TTree, TValue, bool>(predicate));
    }

    public static bool Any<TTree>
    (
        this ITree<TTree> tree,
        Func<TTree, bool> predicate
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TTree, bool>
        (
            initialAggregationImplementation: static () => false,
            aggregateImplementation: (s, c, e) =>
            {
                if ((s || c) is true) { return true; }
                return predicate(e);
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static bool Any<TValue, TTree>
    (
        this IRoseTree<TValue, TTree> tree,
        Func<TValue, bool> predicate
    )
        where TTree : IRoseTree<TValue, TTree>
    {
        return tree.Any(WarpParameterInTree<TTree, TValue, bool>(predicate));
    }
}