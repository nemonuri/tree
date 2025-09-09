
using System.Runtime.InteropServices;

namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the aggregated value which is null.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct NullValue : IEquatable<NullValue>
{
    /// <inheritdoc cref="System.Object.Equals(object?)" path="/summary"/>
    /// <returns>
    /// <see langword="true"/> if the specified object is <see cref="NullValue"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is NullValue;

    /// <inheritdoc cref="NullValue.Equals(object?)" path="/summary"/>
    /// <returns><see langword="true"/></returns>
    public bool Equals(NullValue other) => true;

    
    /// <inheritdoc cref="System.Object.GetHashCode()" path="/summary"/>
    /// <returns>0</returns>
    public override int GetHashCode() => 0;
}