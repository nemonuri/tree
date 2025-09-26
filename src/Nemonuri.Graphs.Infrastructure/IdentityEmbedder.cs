using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public readonly struct IdentityEmbedder<T>() : IEmbedder<T, T, NullValue>
{
    public bool TryEmbed(T source, [NotNullWhen(true)] out T target, out NullValue remainder)
    {
        return (target = source) is not null;
    }
}