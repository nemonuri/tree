using Nemonuri.Trees.Abstractions;
using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static TResult[] ToArray<TSourceTree, TResult>
    (
        this ITree<TSourceTree> tree,
        Func<TResult, bool> predicate,
        Func<TSourceTree, TResult> selector
    )
        where TSourceTree : ITree<TSourceTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);
        Guard.IsNotNull(selector);

        ArrayBuilder<TResult> arrayBuilder = default;

        var treeAggregator = TreeAggregatorTheory.Create<TSourceTree, NullValue>
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

    public static TResult[] ToArray<TSourceValue, TSourceTree, TResult>
    (
        this IRoseTree<TSourceValue, TSourceTree> tree,
        Func<TResult, bool> predicate,
        Func<TSourceValue, TResult> selector
    )
        where TSourceTree : IRoseTree<TSourceValue, TSourceTree>
    {
        return tree.ToArray
        (
            predicate,
            WarpParameterInTree<TSourceTree, TSourceValue, TResult>(selector)
        );
    }

    public static TResult[] ToArray<TSourceTree, TResult>
    (
        this ITree<TSourceTree> tree,
        Func<TSourceTree, TResult> selector
    )
        where TSourceTree : ITree<TSourceTree>
    {
        return tree.ToArray(Tautology, selector);
    }

    public static TResult[] ToArray<TSourceValue, TSourceTree, TResult>
    (
        this IRoseTree<TSourceValue, TSourceTree> tree,
        Func<TSourceValue, TResult> selector
    )
        where TSourceTree : IRoseTree<TSourceValue, TSourceTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        return tree.ToArray(WarpParameterInTree<TSourceTree, TSourceValue, TResult>(selector));
    }

    public static TValue[] ToArray<TValue, TTree>
    (
        this IRoseTree<TValue, TTree> tree
    )
        where TTree : IRoseTree<TValue, TTree>
    {
        return tree.ToArray(Identity);
    }

    public static int Count<TTree>
    (
        this ITree<TTree> tree
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(tree);

        int count = 0;

        var treeAggregator = TreeAggregatorTheory.Create<TTree, NullValue>
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