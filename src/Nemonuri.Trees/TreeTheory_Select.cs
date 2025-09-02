using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{

    public static IRoseNode<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<IRoseNode<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
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
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(roseNode);
        Guard.IsNotNull(selector);

        return roseNode.ToTree().Select(SelectorImpl);

        TResult SelectorImpl(IRoseNode<TSource> roseNode) => selector(roseNode.Value);
    }

    public static IRoseNode<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, IReadOnlyList<int>, TResult> selector
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
        Func<TSource, IReadOnlyList<int>, TResult> selector
    )
    {
        Guard.IsNotNull(roseNode);
        Guard.IsNotNull(selector);

        return roseNode.ToTree().Select(SelectorImpl);

        TResult SelectorImpl(IRoseNode<TSource> roseNode, IReadOnlyList<int> indexesFromRoot) => selector(roseNode.Value, indexesFromRoot);
    }
}
