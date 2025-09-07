using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ImmutableList<ITree<TResult>> Where<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<ITree<TSource>, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<ITree<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                if (predicate(r))
                {
                    ITree<TResult> newTree = CreateBottomUp(r, c);
                    return s.Add(newTree);
                }
                else
                {
                    return s.AddRange(c);
                }
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static ImmutableList<ITree<TElement>> Where<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        return tree.Where(predicate, Identity);
    }

    public static ImmutableList<ITree<TResult>> Where<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<TSource, TResult> selector
    )
    {
        return tree.Where(predicate, WarpParameterInTree(selector));
    }
}