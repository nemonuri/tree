using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{ 
    public static bool All<TElement>
    (
        this ITree<TElement> tree,
        Func<ITree<TElement>, bool> predicate
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TElement, bool>
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

    public static bool All<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        return tree.All(WarpParameterInTree(predicate));
    }

    public static bool Any<TElement>
    (
        this ITree<TElement> tree,
        Func<ITree<TElement>, bool> predicate
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TElement, bool>
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

    public static bool Any<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        return tree.Any(WarpParameterInTree(predicate));
    }
}