
using System.Collections;

namespace Nemonuri.Trees.Paths;

public class TreeIterator<TSource, TResult> : IEnumerator<TResult>
{
    public TreeIterator(ITree<TSource> root)
    {
        Root = root;
    }

    public ITree<TSource> Root { get; }
    public IIndexPath? CurrentIndexPath { get; }

    public TResult Current => throw new NotImplementedException();

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    object IEnumerator.Current => Current!;

    public void Dispose()
    {
    }
}