namespace Nemonuri.Trees.Paths;

public static class IndexPathTheory
{
    public static IIndexPath Create(ReadOnlySpan<int> indexes) => new IndexPath(ImmutableList.Create(indexes));
}