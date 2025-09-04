
namespace Nemonuri.Trees.Paths;

public static class IndexPathTheory
{
    public static IIndexPath Create(ReadOnlySpan<int> indexes) => new IndexPath(ImmutableList.Create(indexes));

    public static IIndexPath SetItem(this IIndexPath indexPath, Index index, int value)
    {
        return !index.IsFromEnd ?
            indexPath.SetItem(index.Value, value) :
            indexPath.SetItem(indexPath.Count - index.Value, value);
    }
}