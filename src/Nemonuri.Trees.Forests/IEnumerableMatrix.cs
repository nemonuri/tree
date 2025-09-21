namespace Nemonuri.Trees.Forests;

public interface IEnumerableMatrix<T, TRow, TColumn, TRowCollection, TColumnCollection>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    where TRow : IEnumerable<T>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TColumn : IEnumerable<T>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TRowCollection : IEnumerable<TRow>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TColumnCollection : IEnumerable<TColumn>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TRowCollection EnumerateRows();
    TColumnCollection EnumerateColumns();
}
