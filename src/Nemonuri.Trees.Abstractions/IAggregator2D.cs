
namespace Nemonuri.Trees;

/// <summary>
/// Defines type-specific methods for two-to-one aggregation.
/// </summary>
/// <inheritdoc cref="IAggregator{_,_}" path="/typeparam" />
public interface IAggregator2D<TElement, TAggregation>
{
    /// <inheritdoc cref="IAggregator{_,_}.InitialAggregation" />
    TAggregation InitialAggregation { get; }

    /// <summary>
    /// Aggregate the spcified element with two previous aggregated values.
    /// </summary>
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
    TAggregation Aggregate
    (
        TAggregation siblingsAggregation,
        TAggregation childrenAggregation,
        TElement element
    );
}


