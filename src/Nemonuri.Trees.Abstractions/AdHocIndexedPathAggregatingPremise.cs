
// TODO: Delete

#if false
namespace Nemonuri.Trees;

public class AdHocIndexedPathAggregatingPremise<TNode, TTarget> : IIndexedPathAggregatingPremise<TNode, TTarget>
{
    private readonly AdHocAggregatingPremise<IndexedPathWithNodePremise<TNode>, TTarget> _internalPremise;

    private AdHocIndexedPathAggregatingPremise(AdHocAggregatingPremise<IndexedPathWithNodePremise<TNode>, TTarget> internalPremise)
    {
        Debug.Assert(internalPremise is not null);

        _internalPremise = internalPremise;
    }

    public AdHocIndexedPathAggregatingPremise
    (
        Func<TTarget> defaultSeedProvider,
        TryAggregator2D<IndexedPathWithNodePremise<TNode>, TTarget> tryAggregator
    )
    : this(new(defaultSeedProvider, tryAggregator))
    { }

    public AdHocIndexedPathAggregatingPremise
    (
        Func<TTarget> defaultSeedProvider,
        OptionalAggregator2D<IndexedPathWithNodePremise<TNode>, TTarget> optionalAggregator
    )
    : this(new(defaultSeedProvider, optionalAggregator))
    { }

    public TTarget DefaultAggregated => _internalPremise.DefaultAggregated;

    public bool TryAggregate
    (
        TTarget siblingsSeed, TTarget childrenSeed, IndexedPathWithNodePremise<TNode> source, [NotNullWhen(true)] out TTarget? aggregated
    ) =>
    _internalPremise.TryAggregate(siblingsSeed, childrenSeed, source, out aggregated);
}
#endif