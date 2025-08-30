
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// Represents the three-to-one aggregator that ancestor elements are null.
/// </summary>
/// <typeparam name="TAggregator2D">
/// The concrete type of <see cref="IAggregator2D{_,_}"/>
/// </typeparam>
/// <inheritdoc cref="IAggregator2D{_,_}" path="/typeparam"/>
/// <remarks>
/// This type wraps an instance of <see cref="IAggregator2D{_,_}"/> to provide implementation of <see cref="IAggregator3D{_,_,_,_}"/>
/// </remarks>
public readonly struct NullAncestorAggregator3D<TAggregator2D, TElement, TAggregation> :
    IAggregator3D<TElement, TAggregation, NullAggregation, NullAggregation>
    where TAggregator2D : IAggregator2D<TElement, TAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
#endif
{
    private readonly TAggregator2D _aggregator2D;

    /// <summary>
    /// Initializes an instance of <see cref="NullAncestorAggregator3D{_,_,_}"/> 
    /// that wraps an instance of <see cref="IAggregator2D{_,_}"/>
    /// </summary>
    /// <param name="aggregator2D">The <see cref="IAggregator2D{_,_}"/></param>
    public NullAncestorAggregator3D(TAggregator2D aggregator2D)
    {
        _aggregator2D = aggregator2D;
    }

    /// <inheritdoc cref="IAggregator3D{_,_,_,_}.InitialAncestorsAggregation"/>
    /// <inheritdoc cref="NullAggregator.InitialAggregation" path="/value"/>
    public NullAggregation InitialAncestorsAggregation => default;

    /// <inheritdoc cref="IAggregator3D{_,_,_,_}.InitialAncestorsAggregation"/>
    /// <returns>
    /// <inheritdoc cref="NullAncestorAggregator3D{_,_,_}.InitialAggregation" path="/value"/>
    /// </returns>
    public NullAggregation AggregateAncestor(NullAggregation ancestorsAggregation, NullAggregation ancestor) =>
        default;

    /// <inheritdoc cref="IAggregator3D{_,_,_,_}.InitialAggregation"/>
    public TAggregation InitialAggregation => _aggregator2D.InitialAggregation;

    /// <inheritdoc cref="IAggregator3D{_,_,_,_}.Aggregate(_,_,_,_)"/>
    public TAggregation Aggregate
    (
        NullAggregation ancestorsAggregation, TAggregation siblingsAggregation,
        TAggregation childrenAggregation, TElement element
    ) =>
    _aggregator2D.Aggregate(siblingsAggregation, childrenAggregation, element);
}