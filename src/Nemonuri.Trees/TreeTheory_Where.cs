using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ImmutableList<IBinderRoseTree<TResult>> Where<TSource, TResult>
    (
        this IBinderRoseTree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<IBinderRoseTree<TSource>, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<IBinderRoseTree<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                if (predicate(r))
                {
                    IBinderRoseTree<TResult> newTree = CreateBranch(r, c);
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

    public static ImmutableList<IBinderRoseTree<TElement>> Where<TElement>
    (
        this IBinderRoseTree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        return tree.Where(predicate, Identity);
    }

    public static ImmutableList<IBinderRoseTree<TResult>> Where<TSource, TResult>
    (
        this IBinderRoseTree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<TSource, TResult> selector
    )
    {
        return tree.Where(predicate, WarpParameterInTree(selector));
    }
}