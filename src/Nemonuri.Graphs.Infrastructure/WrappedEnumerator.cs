using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public readonly struct WrappedEnumerator<TEnumerator, TItem> : 
    IIndexEnumerator<ValueNull, SafeEnumerator<TEnumerator, TItem>, TItem>
    where TEnumerator : IEnumerator<TItem>
{
    private readonly TEnumerator _enumerator;

    public WrappedEnumerator(TEnumerator enumerator)
    {
        _enumerator = enumerator;
    }

    public SafeEnumerator<TEnumerator, TItem> GetInitialIndex(ValueNull context)
    {
        return new(_enumerator);
    }

    public bool TryGetItem(ValueNull context, SafeEnumerator<TEnumerator, TItem> index, [NotNullWhen(true)] out TItem? item)
    {
        if (index.CanGetCurrent)
        {
            item = index.Current;
            return item is not null;
        }
        else
        {
            item = default;
            return false;
        }
    }

    public bool TryGetNextIndex(ValueNull context, SafeEnumerator<TEnumerator, TItem> index, [NotNullWhen(true)] out SafeEnumerator<TEnumerator, TItem> nextIndex)
    {
        bool success = index.MoveNext();
        nextIndex = index;
        return success;
    }
}