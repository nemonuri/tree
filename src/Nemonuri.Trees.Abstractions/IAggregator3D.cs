
namespace Nemonuri.Trees;

/// <summary>
/// Defines type-specific methods for three-to-one aggregation.
/// </summary>
/// <typeparam name="TElement">
/// <inheritdoc cref="IAggregator{_,_}" path="/typeparam[@name='TElement']" />
/// </typeparam>
/// <typeparam name="TAggregation">
/// <inheritdoc cref="IAggregator{_,_}" path="/typeparam[@name='TAggregation']" />
/// </typeparam>
/// <typeparam name="TAncestorsAggregation">
/// The type of the aggregated value from ancestors.
/// </typeparam>
public interface IAggregator3D
<
    TElement,
    TAggregation,
    TAncestorsAggregation
>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
    /// <summary>
    /// Gets the initial aggregated value from ancestors.
    /// </summary>
    TAncestorsAggregation InitialAncestorsAggregation { get; }

    /// <inheritdoc cref="IAggregator{_,_}.InitialAggregation" />
    TAggregation InitialAggregation { get; }

    /// <summary>
    /// Aggregate the spcified element with three previous aggregated values.
    /// </summary>
    /// <param name="ancestorsAggregation">
    /// The previous aggregated value from ancestors,
    /// if <paramref name="element"/> has ancestor elements;
    /// otherwise, the value of <see cref="InitialAncestorsAggregation"/>.
    /// </param>
    /// <param name="siblingsAggregation">
    /// The previous aggregated value from preceding siblings, 
    /// if <paramref name="element"/> has preceding siblings elements;
    /// otherwise, the value of <see cref="InitialAggregation"/>.
    /// </param>
    /// <param name="childrenAggregation">
    /// The previous aggregated value from children,
    /// if <paramref name="element"/> has children elements;
    /// otherwise, the value of <see cref="InitialAggregation"/>.
    /// </param>
    /// <param name="element">
    /// <inheritdoc cref="IAggregator{_,_}.Aggregate(_,_)" path="/param[@name='element']" />
    /// </param>
    /// <inheritdoc cref="IAggregator{_,_}.Aggregate(_,_)" path="/returns" />
    bool Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregation siblingsAggregation,
        TAggregation childrenAggregation,
        TElement element
    );
}