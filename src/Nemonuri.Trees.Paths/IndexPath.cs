using System.Collections;

namespace Nemonuri.Trees.Paths;

public class IndexPath : IIndexPath
{
    private readonly ImmutableList<int> _indexes;

    public IndexPath(IEnumerable<int> indexes)
    {
        if (indexes is ImmutableList<int> immlist)
        {
            _indexes = immlist;
        }
        else
        {
            _indexes = [.. indexes];
        }
    }

    public int this[int index] => _indexes[index];

    public int Count => _indexes.Count;

    public IEnumerator<int> GetEnumerator()
    {
        return _indexes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _indexes.GetEnumerator();
    }

    public IIndexPath Slice(int start, int length)
    {
        Guard.IsInRange(start, 0, Count + 1);
        Guard.IsGreaterThanOrEqualTo(length, 0);
        Guard.IsLessThanOrEqualTo(start + length, Count);

        var builder = ImmutableList<int>.Empty.ToBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Add(_indexes[start + i]);
        }

        return new IndexPath(builder.ToImmutable());
    }

    public IIndexPath SetItem(int index, int value)
    {
        Guard.IsInRange(index, 0, Count);

        return new IndexPath(_indexes.SetItem(index, value));
    }

    public IIndexPath Concat(IEnumerable<int> indexes)
    {
        return new IndexPath(_indexes.AddRange(indexes));
    }
}
