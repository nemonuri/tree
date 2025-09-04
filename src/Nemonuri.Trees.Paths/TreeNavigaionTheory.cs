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

        var e = new TreeEnumerator<TSource>(root);
        while (e.MoveNext())
        {
            yield return selector(e.Current.Root, e.CurrentIndexPath!);
        }
    }

    private static T Identity<T>(T t, IReadOnlyList<int> indexes) => t;
    private static T IdentityForRoseNode<T>(IRoseNode<T> t, IReadOnlyList<int> indexes) => t.Value;

    public static bool TryGetItem<TElement>
    (
        this ITree<TElement> tree,
        IEnumerable<int> indexPath,
        [NotNullWhen(true)] out ITree<TElement>? result
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(indexPath);

        ITree<TElement> currentTree = tree;
        foreach (var index in indexPath)
        {
            if (currentTree.GetChildren().ElementAtOrDefault(index) is not { } nextTree)
            {
                result = default;
                return false;
            }
            currentTree = nextTree;
        }
        result = currentTree;
        return result is not null;
    }

    private static IndexPath GetFirstIndexPathAsDepthFirstPostOrder<TElement>(this ITree<TElement> root, out ITree<TElement> treeAtIndexPath)
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
        treeAtIndexPath = currentTree;

        Span<int> indexes = stackalloc int[depth];
        indexes.Fill(0);
        return new IndexPath(ImmutableList.Create<int>(indexes));
    }

    public static bool TryGetNextIndexPath<TElement>
    (
        this ITree<TElement> tree,
        IIndexPath? oldIndexPath,
        [NotNullWhen(true)] out IIndexPath? nextIndexPath,
        [NotNullWhen(true)] out ITree<TElement>? treeAtNextIndexPath
    )
    {
        Guard.IsNotNull(tree);

        if (oldIndexPath is null)
        {
            nextIndexPath = tree.GetFirstIndexPathAsDepthFirstPostOrder(out treeAtNextIndexPath);
            return true;
        }
        else if (oldIndexPath is [])
        {
            nextIndexPath = default;
            treeAtNextIndexPath = default;
            return false;
        }

        nextIndexPath = oldIndexPath;

        while (true)
        {
            Guard.HasSizeGreaterThan(nextIndexPath, 0);

            nextIndexPath = nextIndexPath.SetItem(^1, oldIndexPath[^1] + 1);
            if (tree.TryGetItem(nextIndexPath, out var tree1))
            {
                var toConcat = tree1.GetFirstIndexPathAsDepthFirstPostOrder(out treeAtNextIndexPath);
                if (toConcat.Count > 0)
                {
                    nextIndexPath = nextIndexPath.Concat(toConcat);
                }
                return true;
            }

            nextIndexPath = nextIndexPath[..^1];
            if (nextIndexPath is [])
            {
                treeAtNextIndexPath = tree;
                return true;
            }
        }
    }
}
