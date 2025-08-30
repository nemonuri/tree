
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// An ad-hoc implementation of <see cref="IAggregator2D{_,_}"/>
/// </summary>
/// <inheritdoc cref="IAggregator2D{_,_}" path="/typeparam" />
public readonly struct AdHocAggregator2D<TElement, TAggregation> :
    IAggregator2D<TElement, TAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    private readonly Func<TAggregation> _initialAggregationImplementation;
    private readonly Func<TAggregation, TAggregation, TElement, TAggregation> _aggregateImplementation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdHocAggregator2D{_,_}"/> structure with the specified implementations.
    /// </summary>
    /// <param name="initialAggregationImplementation">
    /// The implementation of <see cref="IAggregator2D{_,_}.InitialAggregation"/>
    /// </param>
    /// <param name="aggregateImplementation">
    /// The implementation of <see cref="IAggregator2D{_,_}.Aggregate(_,_,_)"/>
    /// </param>
    public AdHocAggregator2D
    (
        Func<TAggregation> initialAggregationImplementation,
        Func<TAggregation, TAggregation, TElement, TAggregation> aggregateImplementation
    )
    {
        _initialAggregationImplementation = initialAggregationImplementation;
        _aggregateImplementation = aggregateImplementation;
    }

    /// <inheritdoc cref="IAggregator2D{_,_}.InitialAggregation" />
    public TAggregation InitialAggregation => _initialAggregationImplementation();

    /// <inheritdoc cref="IAggregator2D{_,_}.Aggregate(_,_,_)" />
    public TAggregation Aggregate(TAggregation siblingsAggregation, TAggregation childrenAggregation, TElement element) =>
        _aggregateImplementation(siblingsAggregation, childrenAggregation, element);
}