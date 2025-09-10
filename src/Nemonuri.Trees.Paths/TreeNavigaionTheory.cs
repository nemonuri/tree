namespace Nemonuri.Trees.Paths;

public static class TreeNavigaionTheory
{
    public static IEnumerable<TTree> ToEnumerable<TTree>
    (
        TTree root
    )
        where TTree : ITree<TTree>
    {
        return ToEnumerable(root, Identity);
    }

    public static IEnumerable<TResultValue> ToEnumerable<TSourceTree, TResultValue>
    (
        TSourceTree root,
        Func<TSourceTree, IIndexPath, TResultValue> selector
    )
        where TSourceTree : ITree<TSourceTree>
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(selector);

        var e = new TreeEnumerator<TSourceTree>(root);
        while (e.MoveNext())
        {
            yield return selector(e.Current, e.CurrentIndexPath!);
        }
    }

    private static T Identity<T>(T t, IReadOnlyList<int> indexes) => t;
    //private static T IdentityForRoseNode<T>(IRoseNode<T> t, IReadOnlyList<int> indexes) => t.Value;

    public static bool TryGetItem<TTree>
    (
        this ITree<TTree> tree,
        IEnumerable<int> indexPath,
        [NotNullWhen(true)] out TTree? result
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(indexPath);

        TTree currentTree = (TTree)tree;
        foreach (var index in indexPath)
        {
            if (currentTree.Children.ElementAtOrDefault(index) is not { } nextTree)
            {
                result = default;
                return false;
            }
            currentTree = nextTree;
        }
        result = currentTree;
        return result is not null;
    }

    private static IndexPath GetFirstIndexPathAsDepthFirstPostOrder<TTree>(TTree root, [NotNull] out TTree? treeAtIndexPath)
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(root);

        TTree currentTree = root;
        int depth = 0;

        while
        (
            currentTree.Children is { } children &&
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

    public static bool TryGetNextIndexPath<TTree>
    (
        this ITree<TTree> tree,
        IIndexPath? oldIndexPath,
        [NotNullWhen(true)] out IIndexPath? nextIndexPath,
        [NotNullWhen(true)] out TTree? treeAtNextIndexPath
    )
        where TTree : ITree<TTree>
    {
        Guard.IsNotNull(tree);
        TTree ensuredTree = (TTree)tree;

        if (oldIndexPath is null)
        {
            nextIndexPath = GetFirstIndexPathAsDepthFirstPostOrder(ensuredTree, out treeAtNextIndexPath);
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

            nextIndexPath = nextIndexPath.SetItem(^1, nextIndexPath[^1] + 1);
            if (tree.TryGetItem(nextIndexPath, out var tree1))
            {
                var toConcat = GetFirstIndexPathAsDepthFirstPostOrder(tree1, out treeAtNextIndexPath);
                if (toConcat.Count > 0)
                {
                    nextIndexPath = nextIndexPath.Concat(toConcat);
                }
                return true;
            }

            nextIndexPath = nextIndexPath[..^1];
            return tree.TryGetItem(nextIndexPath, out treeAtNextIndexPath);
        }
    }
}
