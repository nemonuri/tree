using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static IGeneralBinderRoseTree<TResultValue> Select<TSourceTree, TResultValue>
    (
        this ITree<TSourceTree> tree,
        Func<TSourceTree, TResultValue> selector
    )
        where TSourceTree : ITree<TSourceTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        var treeAggregator = TreeAggregatorTheory.Create<TSourceTree, ImmutableList<IGeneralBottomUpRoseTree<TResultValue>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResultValue r = selector(e);
                IGeneralBottomUpRoseTree<TResultValue> newTree = CreateBranch(r, c);
                return s.Add(newTree);
            }
        );

        var aggregation = tree.Aggregate(treeAggregator);

        Guard.IsEqualTo(aggregation.Count, 1);
        return aggregation[0];
    }

    public static IGeneralBinderRoseTree<TResultValue> Select<TSourceValue, TSourceTree, TResultValue>
    (
        this IRoseTree<TSourceValue, TSourceTree> tree,
        Func<TSourceValue, TResultValue> selector
    )
        where TSourceTree : IRoseTree<TSourceValue, TSourceTree>
    {
        Guard.IsNotNull(selector);

        return tree.Select(WarpParameterInTree<TSourceTree, TSourceValue, TResultValue>(selector));
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
