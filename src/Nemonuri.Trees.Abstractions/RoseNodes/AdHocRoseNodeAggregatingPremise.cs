
// TODO: Delete

#if false
namespace Nemonuri.Trees.RoseNodes;

public class AdHocRoseNodeAggregatingPremise<T, TTarget>
    : IAggregator2D<IndexedPathWithNodePremise<RoseNode<T>>, TTarget>
{
    private readonly AdHocAggregatingPremise<IndexedPathWithNodePremise<RoseNode<T>>, TTarget> _internalPremise;

    private AdHocRoseNodeAggregatingPremise(AdHocAggregatingPremise<IndexedPathWithNodePremise<RoseNode<T>>, TTarget> internalPremise)
    {
        Debug.Assert(internalPremise is not null);

        _internalPremise = internalPremise;
    }

    public AdHocRoseNodeAggregatingPremise
    (
        Func<TTarget> defaultSeedProvider,
        TryAggregator2D<IndexedPathWithNodePremise<RoseNode<T>>, TTarget> tryAggregator
    )
    : this(new(defaultSeedProvider, tryAggregator))
    { }

    public AdHocRoseNodeAggregatingPremise
    (
        Func<TTarget> defaultSeedProvider,
        OptionalAggregator2D<IndexedPathWithNodePremise<RoseNode<T>>, TTarget> optionalAggregator
    )
    : this(new(defaultSeedProvider, optionalAggregator))
    { }

    public TTarget DefaultAggregated => _internalPremise.DefaultAggregated;

    public bool TryAggregate
    (
        TTarget siblingsSeed, TTarget childrenSeed, IndexedPathWithNodePremise<RoseNode<T>> source,
        [NotNullWhen(true)] out TTarget? aggregated
    ) =>
    _internalPremise.TryAggregate(siblingsSeed, childrenSeed, source, out aggregated);
}
#endif