
namespace Nemonuri.Trees;

using Abstractions;

public static partial class TreeTheory
{ 
    public static bool All<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TElement, bool>
        (
            initialAggregationImplementation: static () => true,
            aggregateImplementation: (s, c, e) =>
            {
                if ((s || c) is false) { return false; }
                return predicate(e);
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static bool All<TElement>
    (
        this IRoseNode<TElement> roseNode,
        Func<TElement, bool> predicate
    )
    {
        return roseNode.ToTree().All(PredicateImpl);

        bool PredicateImpl(IRoseNode<TElement> roseNode) => predicate(roseNode.Value);
    }

    public static bool Any<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
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
        this IRoseNode<TElement> roseNode,
        Func<TElement, bool> predicate
    )
    {
        return roseNode.ToTree().Any(PredicateImpl);

        bool PredicateImpl(IRoseNode<TElement> roseNode) => predicate(roseNode.Value);
    }
}