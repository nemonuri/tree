using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public interface IIndexEnumerator<TContext, TIndex, TItem>
{
    TIndex GetInitialIndex(TContext context);

    bool TryGetItem(TContext context, TIndex index, [NotNullWhen(true)] out TItem? item);

    bool TryGetNextIndex(TContext context, TIndex index, [NotNullWhen(true)] out TIndex nextIndex);
}
