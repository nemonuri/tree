using System.Runtime.CompilerServices;

namespace Nemonuri.Trees;

//[CollectionBuilder(typeof(IndexPathTheory), nameof(IndexPathTheory.Create))]
public interface IIndexPath : IReadOnlyList<int>, ISupportSlice<IIndexPath>
{
    IIndexPath Concat(IEnumerable<int> indexes);
    IIndexPath SetItem(int index, int value);
}
