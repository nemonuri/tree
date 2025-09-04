using System.Runtime.CompilerServices;

namespace Nemonuri.Trees.Paths;

[CollectionBuilder(typeof(IndexPathTheory), nameof(IndexPathTheory.Create))]
public interface IIndexPath : IReadOnlyList<int>
{
    IIndexPath Slice(int start, int length);
    IIndexPath Concat(IEnumerable<int> indexes);
    IIndexPath SetItem(int index, int value);
}
