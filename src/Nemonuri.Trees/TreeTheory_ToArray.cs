using Nemonuri.Trees.Abstractions;
using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static TResult[] ToArray<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<ITree<TSource>, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);
        Guard.IsNotNull(selector);

        ArrayBuilder<TResult> arrayBuilder = default;

        var treeAggregator = TreeAggregatorTheory.Create<TSource, NullAggregation>
        (
            initialAggregationImplementation: static () => default,
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                if (predicate(r))
                { 
                    arrayBuilder.Add(selector(e));
                }
                return default;
            }
        );

        _ = tree.Aggregate(treeAggregator);

        return arrayBuilder.ToArray();
    }

    public static TResult[] ToArray<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<TSource, TResult> selector
    )
    {
        return tree.ToArray(predicate, WarpParameterInTree(selector));
    }

    public static TResult[] ToArray<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<ITree<TSource>, TResult> selector
    )
    {
        return tree.ToArray(Tautology, selector);
    }

    public static TResult[] ToArray<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        return tree.ToArray(WarpParameterInTree(selector));
    }

    public static TElement[] ToArray<TElement>
    (
        this ITree<TElement> tree
    )
    {
        return tree.ToArray((Func<TElement, TElement>)Identity);
    }

    public static int Count<TElement>
    (
        this ITree<TElement> tree
    )
    {
        Guard.IsNotNull(tree);

        int count = 0;

        var treeAggregator = TreeAggregatorTheory.Create<TElement, NullAggregation>
        (
            initialAggregationImplementation: static () => default,
            aggregateImplementation: (s, c, e) =>
            {
                count += 1;
                return default;
            }
        );

        _ = tree.Aggregate(treeAggregator);

        return count;
    }
}