
using System.Runtime.InteropServices;

namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the aggregated value which is null.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct NullAggregation : IEquatable<NullAggregation>
{
    /// <inheritdoc cref="System.Object.Equals(object?)" path="/summary"/>
    /// <returns>
    /// <see langword="true"/> if the specified object is <see cref="NullAggregation"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is NullAggregation;

    /// <inheritdoc cref="NullAggregation.Equals(object?)" path="/summary"/>
    /// <returns><see langword="true"/></returns>
    public bool Equals(NullAggregation other) => true;

    
    /// <inheritdoc cref="System.Object.GetHashCode()" path="/summary"/>
    /// <returns>0</returns>
    public override int GetHashCode() => 0;
}