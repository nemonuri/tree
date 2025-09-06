
namespace Nemonuri.Trees.Indexes;

public static class IndexPathFactoryTheory
{
    public static IIndexPath CreateFromImmutableList
    (
        this IIndexPathFactory indexPathFactory,
        ImmutableList<int> indexes
    )
    {
        Guard.IsNotNull(indexPathFactory);
        Guard.IsNotNull(indexes);

        return indexPathFactory.Create(indexes);
    }
}