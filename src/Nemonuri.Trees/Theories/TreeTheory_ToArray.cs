namespace Nemonuri.Trees.Theories;

using Abstractions;

public static partial class TreeTheory
{
    public static TResult[] ToArray<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        ArrayBuilder<TResult> arrayBuilder = default;

        var treeAggregator = TreeAggregatorTheory.Create<TSource, NullAggregation>
        (
            initialAggregationImplementation: static () => default,
            aggregateImplementation: (s, c, e) =>
            {
                arrayBuilder.Add(selector(e));
                return default;
            }
        );

        _ = tree.Aggregate(treeAggregator);

        return arrayBuilder.ToArray();
    }

    public static TResult[] ToArray<TSource, TResult>
    (
        this IRoseNode<TSource> roseNode,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(roseNode);
        Guard.IsNotNull(selector);

        return roseNode.ToTree().ToArray(SelectorImpl);

        TResult SelectorImpl(IRoseNode<TSource> roseNode) => selector(roseNode.Value);
    }

    public static TElement[] ToArray<TElement>
    (
        this ITree<TElement> tree
    )
    {
        return tree.ToArray(Identity);
    }

    public static TElement[] ToArray<TElement>
    (
        this IRoseNode<TElement> roseNode
    )
    {
        return roseNode.ToArray(Identity);
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

    public static int Count<TElement>
    (
        this IRoseNode<TElement> roseNode
    )
    {
        return roseNode.ToTree().Count();
    }
}