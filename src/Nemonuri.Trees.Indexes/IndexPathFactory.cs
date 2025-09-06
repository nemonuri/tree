
namespace Nemonuri.Trees.Indexes;

public class IndexPathFactory : IIndexPathFactory
{
    public readonly static IndexPathFactory Instance = new();

    public IndexPathFactory() { }

    public IIndexPath Create(IEnumerable<int> indexes)
    {
        return new IndexPath(indexes);
    }
}