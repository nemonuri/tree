using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ITree<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<ITree<TSource>, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<ITree<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                ITree<TResult> newTree = CreateBottomUp(r, c);
                return s.Add(newTree);
            }
        );

        ImmutableList<ITree<TResult>> aggregation = tree.Aggregate(treeAggregator);

        Guard.IsEqualTo(aggregation.Count, 1);
        return aggregation[0];
    }

    public static ITree<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(selector);

        return tree.Select(WarpParameterInTree(selector));
    }

#if false
    public static IRoseNode<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, IIndexPath, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<IRoseNode<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (a, s, c, e) =>
            {
                TResult r = selector(e, a);
                IRoseNode<TResult> roseNode = new RoseNode<TResult>(r, c);
                return s.Add(roseNode);
            }
        );

        ImmutableList<IRoseNode<TResult>> aggregation = tree.Aggregate(treeAggregator);

        Guard.IsEqualTo(aggregation.Count, 1);
        return aggregation[0];
    }

    public static IRoseNode<TResult> Select<TSource, TResult>
    (
        this IRoseNode<TSource> roseNode,
        Func<TSource, IIndexPath, TResult> selector
    )
    {
        Guard.IsNotNull(roseNode);
        Guard.IsNotNull(selector);

        return roseNode.ToTree().Select(SelectorImpl);

        TResult SelectorImpl(IRoseNode<TSource> roseNode, IIndexPath indexesFromRoot) => selector(roseNode.Value, indexesFromRoot);
    }
#endif
}
