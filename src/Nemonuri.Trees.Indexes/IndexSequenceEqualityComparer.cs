
namespace Nemonuri.Trees.Indexes;

public class IndexPathEqualityComparer : IEqualityComparer<IIndexPath>
{
    public IndexPathEqualityComparer() { }

    public bool Equals(IIndexPath? x, IIndexPath? y) =>
        IndexPathTheory.AreEqual(x, y);

    public int GetHashCode([DisallowNull] IIndexPath obj) =>
        IndexPathTheory.CalculateHashCode(obj);
}

