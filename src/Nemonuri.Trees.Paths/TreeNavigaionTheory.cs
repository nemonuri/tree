namespace Nemonuri.Trees.Paths;

public static class TreeNavigaionTheory
{
    public static IEnumerable<TElement> ToEnumerable<TElement>
    (
        ITree<TElement> root
    )
    {
        return ToEnumerable(root, Identity);
    }

    public static IEnumerable<TElement> ToEnumerable<TElement>
    (
        IRoseNode<TElement> roseNode
    )
    {
        return ToEnumerable(roseNode.ToTree(), IdentityForRoseNode);
    }

    public static IEnumerable<TResult> ToEnumerable<TSource, TResult>
    (
        ITree<TSource> root,
        Func<TSource, IReadOnlyList<int>, TResult> selector
    )
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(selector);

        throw new NotImplementedException();
    }

    private static T Identity<T>(T t, IReadOnlyList<int> indexes) => t;
    private static T IdentityForRoseNode<T>(IRoseNode<T> t, IReadOnlyList<int> indexes) => t.Value;

    public static IIndexPath GetFirstIndexPathAsDepthFirstPostOrder<TElement>(this ITree<TElement> root)
    {
        Guard.IsNotNull(root);

        ITree<TElement> currentTree = root;
        int depth = 0;

        while
        (
            currentTree.GetChildren() is { } children &&
            children.Any()
        )
        {
            depth++;
            currentTree = children.First();
        }

        Span<int> indexes = stackalloc int[depth];
        indexes.Fill(0);
        return new IndexPath(ImmutableList.Create<int>(indexes));
    }
}
