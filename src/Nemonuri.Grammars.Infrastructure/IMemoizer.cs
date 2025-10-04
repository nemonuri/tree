namespace Nemonuri.Grammars.Infrastructure;

public interface IMemoizer<TKey, TValue>
{
    bool TryGetMemoized(TKey key, [NotNullWhen(true)] out TValue? value);

    void Memoize(TKey key, TValue value);

    void ClearMemoized();
}
