namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static IGeneralBinderRoseTree<TValue> CreateTopDown<TValue>
    (
        TValue value,
        IChildrenProvider<TValue> childrenProvider
    )
    {
        return new TopDownBinderRoseTree<TValue>(value, childrenProvider, null);
    }


    public static IGeneralBottomUpRoseTree<TValue> CreateBranch<TValue>
    (
        TValue value,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>> children
    )
    {
        return new GeneralBottomUpRoseTree<TValue>(value, children, null, null);
    }

    public static IGeneralBottomUpRoseTree<TValue> CreateLeaf<TValue>(TValue value)
    {
        return new GeneralBottomUpRoseTree<TValue>(value, [], null, null);
    }

    public static IEnumerable<IGeneralBottomUpRoseTree<TValue>> CreateLeafs<TValue>(IEnumerable<TValue>? values)
    {
        if (values is null) { yield break; }
        foreach (var value in values)
        {
            yield return CreateLeaf(value);
        }
    }
}