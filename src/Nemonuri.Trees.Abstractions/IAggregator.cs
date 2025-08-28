
namespace Nemonuri.Trees;

/// <summary>
/// Defines type-specific methods for one-to-one aggregation.
/// </summary>
/// <typeparam name="TElement">The type of elements to be aggregated.</typeparam>
/// <typeparam name="TAggregation">The type of the aggregated value.</typeparam>
public interface IAggregator<TElement, TAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    /// <summary>
    /// Gets the initial aggregated value.
    /// </summary>
    TAggregation InitialAggregation { get; }

    /// <summary>
    /// Aggregate the spcified element with the previous aggregated value.
    /// </summary>
    /// <param name="aggregation">
    /// The previous aggregated value, if <paramref name="element"/> has preceding elements;
    /// otherwise, the value of <see cref="InitialAggregation"/>.
    /// </param>
    /// <param name="element">The element to be aggregated.</param>
    /// <returns>
    /// The next aggregated value.
    /// </returns>
    TAggregation Aggregate
    (
        TAggregation aggregation,
        TElement element
    );
}
