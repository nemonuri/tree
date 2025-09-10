
using System.Collections;

namespace Nemonuri.Trees.Paths;

public struct TreeEnumerator<TTree> : IEnumerator<TTree>
    where TTree : ITree<TTree>
{
    private TTree? _current;
    private IIndexPath? _currentIndexPath;
    private bool _isCompleted;

    public TreeEnumerator(TTree root)
    {
        Guard.IsNotNull(root);

        Root = root;
        Reset();
    }

    public TTree Root { get; }
    public IIndexPath? CurrentIndexPath => _currentIndexPath;

    public TTree Current => _current!;

    public bool MoveNext()
    {
        if (_isCompleted)
        {
            _currentIndexPath = default;
            _current = default;
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
        _currentIndexPath = default;
        _current = default;
        _isCompleted = false;
    }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}