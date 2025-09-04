using System.Runtime.CompilerServices;

namespace Nemonuri.Trees.Paths;

[CollectionBuilder(typeof(IndexPathTheory), nameof(IndexPathTheory.Create))]
public interface IIndexPath : IReadOnlyList<int>
{
    IIndexPath Take(int count);
    IIndexPath Concat(IEnumerable<int> indexes);
}
