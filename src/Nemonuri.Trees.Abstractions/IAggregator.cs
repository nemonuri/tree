
namespace Nemonuri.Trees;

/// <summary>
/// Defines type-specific methods for aggregation.
/// </summary>
/// <typeparam name="TSource">The type of objects to be aggregated.</typeparam>
/// <typeparam name="TAggregation">The type of aggregated values.</typeparam>
public interface IAggregator<TSource, TAggregation>
#if NET9_0_OR_GREATER
    where TSource : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    /// <summary>
    /// Gets the initial aggregated value.
    /// </summary>
    TAggregation InitialAggregation { get; }

    /// <summary>
    /// Tries to aggregate the spcified object with the previous aggregated value.
    /// </summary>
    /// <param name="oldAggregation">The previous aggregated value.</param>
    /// <param name="source">The object to be aggregated.</param>
    /// <param name="aggregation">
    /// When this method returns, contains the aggregated result value, if the operation succeeded;
    /// otherwise, the <see langword="default"/> value for the type of the <paramref name="aggregation"/> parameter.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the operation succeeded; otherwise, <see langword="false"/>.
    /// </returns>
    bool TryAggregate
    (
        TAggregation oldAggregation,
        TSource source,
        [NotNullWhen(true)] out TAggregation? aggregation
    );
}
