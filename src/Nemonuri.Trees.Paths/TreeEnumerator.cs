
using System.Collections;

namespace Nemonuri.Trees.Paths;

public struct TreeEnumerator<TElement> : IEnumerator<ITree<TElement>>
{
    private ITree<TElement>? _current;
    private IIndexPath? _currentIndexPath;
    private bool _isCompleted;

    public TreeEnumerator(ITree<TElement> root)
    {
        Guard.IsNotNull(root);

        Root = root;
        Reset();
    }

    public ITree<TElement> Root { get; }
    public IIndexPath? CurrentIndexPath => _currentIndexPath;

    public ITree<TElement> Current => _current!;

    public bool MoveNext()
    {
        if (_isCompleted)
        {
            _currentIndexPath = null;
            _current = null;
            return false;
        }

        if (Root.TryGetNextIndexPath(_currentIndexPath, out _currentIndexPath, out _current))
        {
            return true;
        }
        else
        {
            _isCompleted = true;
            return false;
        }
    }

    public void Reset()
    {
        _currentIndexPath = null;
        _current = null;
        _isCompleted = false;
    }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}