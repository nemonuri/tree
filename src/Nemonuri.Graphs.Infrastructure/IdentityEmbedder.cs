using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public readonly struct IdentityEmbedder<T>() : IEmbedder<T, T, ValueNull>
{
    public bool TryEmbed(T source, [NotNullWhen(true)] out T target, out ValueNull remainder)
    {
        return (target = source) is not null;
    }
}