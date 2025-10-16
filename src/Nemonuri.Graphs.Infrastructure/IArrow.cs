using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public interface IArrow<TTail, THead>
{
    TTail Tail { get; }
    THead Head { get; }
}

public interface IInArrowSet<TTail, THead, TArrow> : IEnumerable<TArrow>
    where TArrow : IArrow<TTail, THead>
{
    bool TryGetCommonHead([NotNullWhen(true)] out THead? commonHead);
}

public interface IOutArrowSet<TTail, THead, TArrow> : IEnumerable<TArrow>
    where TArrow : IArrow<TTail, THead>
{
    bool TryGetCommonTail([NotNullWhen(true)] out TTail? commonTail);
}
