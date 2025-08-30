
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the minimal ancestor converter that does nothing.
/// </summary>
/// <inheritdoc cref="IAggregator3D{_,_,_,_}" path="/typeparam[@name='TElement']" />
public readonly struct NullAncestorConverter<TElement> :
    IAncestorConverter<TElement, NullAggregation, NullAggregation>
{
    /// <inheritdoc 
    ///     cref="IAncestorConverter{_,_,_}.ConvertToAncestor(_, int?)"
    ///     path="//*[not(self::returns)]"/>
    /// <returns><inheritdoc cref="NullAggregator.InitialAggregation" path="/value"/></returns>
    public NullAggregation ConvertToAncestor(TElement element, int? elementIndex) =>
        default;
}

