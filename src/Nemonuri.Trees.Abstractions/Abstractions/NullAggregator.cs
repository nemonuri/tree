
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the minimal one-to-one aggregator that does nothing.
/// </summary>
public readonly struct NullAggregator :
    IAggregator<NullValue, NullValue>
{
    /// <summary>
    /// The boxed instance of <see langword="default"/> <see cref="NullAggregator"/>
    /// </summary>
    public readonly static IAggregator<NullValue, NullValue> BoxedInstance = (NullAggregator)default;

    /// <inheritdoc cref="IAggregator{_,_}.InitialAggregation"/>
    /// <value>
    /// The <see langword="default"/> value of <see cref="NullValue"/>
    /// </value>
    public NullValue InitialAggregation => default;

    /// <inheritdoc cref="IAggregator{_,_}.Aggregate(_,_)" path="/summary"/>
    /// <returns>
    /// <inheritdoc cref="NullAggregator.InitialAggregation" path="/value"/>
    /// </returns>
    public NullValue Aggregate(NullValue aggregation, NullValue element) =>
        default;
}
