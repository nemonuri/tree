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

    public IIndexPath Take(int count)
    {
        Guard.IsInRange(count, 0, Count + 1);

        var builder = ImmutableList<int>.Empty.ToBuilder();
        for (int i = 0; i < count; i++)
        {
            builder.Add(_indexes[i]);
        }

        return new IndexPath(builder.ToImmutable());
    }

    public IIndexPath Concat(IEnumerable<int> indexes)
    {
        return new IndexPath(_indexes.AddRange(indexes));
    }
}
