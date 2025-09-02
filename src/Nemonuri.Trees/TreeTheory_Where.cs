namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ImmutableList<IRoseNode<TResult>> Where<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TResult, bool> predicate,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<IRoseNode<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                if (predicate(r))
                {
                    IRoseNode<TResult> roseNode = new RoseNode<TResult>(r, c);
                    return s.Add(roseNode);
                }
                else
                {
                    return s.AddRange(c);
                }
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static ImmutableList<IRoseNode<TElement>> Where<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        return tree.Where(predicate, Identity);
    }

    public static ImmutableList<IRoseNode<TResult>> Where<TSource, TResult>
    (
        this IRoseNode<TSource> roseNode,
        Func<TResult, bool> predicate,
        Func<TSource, TResult> selector
    )
    {
        return roseNode.ToTree().Where(predicate, SelectorImpl);

        TResult SelectorImpl(IRoseNode<TSource> roseNode) => selector(roseNode.Value);
    }

    public static ImmutableList<IRoseNode<TElement>> Where<TElement>
    (
        this IRoseNode<TElement> roseNode,
        Func<TElement, bool> predicate
    )
    {
        return roseNode.Where(predicate, Identity);
    }
}