#if false

using System.Collections;

namespace Nemonuri.Trees.Paths;

public class IndexedTreePath<TElement> : IIndexPath
{ 
    private readonly ITree<TElement>? _root;
    private readonly ImmutableList<ITree<TElement>> _decendents;
    private readonly ImmutableList<int> _indexes;

    private IndexedTreePath(ITree<TElement>? root, ImmutableList<ITree<TElement>> decendents, ImmutableList<int> indexes)
    {
        Debug.Assert(root is not null);
        Debug.Assert(indexes is not null);

        _root = root;
        _decendents = decendents;
        _indexes = indexes;

        Debug.Assert(_decendents.Count == _indexes.Count);
    }

    public IIndexPath Take(int count)
    {
        throw new NotImplementedException();
    }

    public IIndexPath Concat(IEnumerable<int> indexes)
    {
        throw new NotImplementedException();
    }

    public int this[int index] => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public IEnumerator<int> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

#endif