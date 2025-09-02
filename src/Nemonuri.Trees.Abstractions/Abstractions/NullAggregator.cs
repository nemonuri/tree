
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the minimal one-to-one aggregator that does nothing.
/// </summary>
public readonly struct NullAggregator :
    IAggregator<NullAggregation, NullAggregation>
{
    /// <summary>
    /// The boxed instance of <see langword="default"/> <see cref="NullAggregator"/>
    /// </summary>
    public readonly static IAggregator<NullAggregation, NullAggregation> BoxedInstance = (NullAggregator)default;

    /// <inheritdoc cref="IAggregator{_,_}.InitialAggregation"/>
    /// <value>
    /// The <see langword="default"/> value of <see cref="NullAggregation"/>
    /// </value>
    public NullAggregation InitialAggregation => default;

    /// <inheritdoc cref="IAggregator{_,_}.Aggregate(_,_)" path="/summary"/>
    /// <returns>
    /// <inheritdoc cref="NullAggregator.InitialAggregation" path="/value"/>
    /// </returns>
    public NullAggregation Aggregate(NullAggregation aggregation, NullAggregation element) =>
        default;
}
