
namespace Nemonuri.Trees.Abstractions;

/// <summary>An ad-hoc implementation of <see cref="IAggregator{_,_}"/></summary>
/// <inheritdoc cref="IAggregator{_,_}" path="/typeparam" />
public readonly struct AdHocAggregator<TElement, TAggregation> : IAggregator<TElement, TAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    private readonly Func<TAggregation> _initialAggregationImplementation;
    private readonly Func<TAggregation, TElement, TAggregation> _aggregateImplementation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdHocAggregator{_,_}"/> structure with the specified implementations.
    /// </summary>
    /// <param name="initialAggregationImplementation">
    /// The implementation of <see cref="IAggregator{_,_}.InitialAggregation"/>
    /// </param>
    /// <param name="aggregateImplementation">
    /// The implementation of <see cref="IAggregator{_,_}.Aggregate(_, _)"/>
    /// </param>
    public AdHocAggregator(Func<TAggregation> initialAggregationImplementation, Func<TAggregation, TElement, TAggregation> aggregateImplementation)
    {
        _initialAggregationImplementation = initialAggregationImplementation;
        _aggregateImplementation = aggregateImplementation;
    }

    /// <inheritdoc cref="IAggregator{_,_}.InitialAggregation"/>
    public TAggregation InitialAggregation => _initialAggregationImplementation();

    /// <inheritdoc cref="IAggregator{_,_}.Aggregate(_,_)"/>
    public TAggregation Aggregate(TAggregation aggregation, TElement element) => _aggregateImplementation(aggregation, element);
}