namespace Nemonuri.Graphs.Infrastructure;

public struct SafeEnumerator<TEnumerator, TItem>
    where TEnumerator : IEnumerator<TItem>
{
    private readonly TEnumerator _enumerator;
    private bool _moved;
    private bool _completed;

    public SafeEnumerator(TEnumerator enumerator)
    {
        _enumerator = enumerator;
        _moved = false;
        _completed = false;
    }

    public bool MoveNext()
    {
        _moved = true;
        if (_completed) { return false; }

        bool success = _enumerator.MoveNext();
        if (!success) { _completed = true; }
        return success;
    }

    public readonly bool Moved => _moved;

    public readonly bool Completed => _completed;

    public readonly bool CanGetCurrent => _moved && !_completed;

    public readonly TItem Current => _enumerator.Current;
}
