using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public interface IContextedEnumerator<T, TContext>
{
    [MemberNotNullWhen(true, nameof(Current))]
    bool MoveNext(TContext context);

    T? Current { get; }
}