
// TODO: Delete

#if false
namespace Nemonuri.Trees;

public class AdHocAggregatingPremise<TSource, TTarget> : IAggregator2D<TSource, TTarget>
{
    public AdHocAggregatingPremise(Func<TTarget> defaultSeedProvider, TryAggregator2D<TSource, TTarget> tryAggregator)
    {
        Debug.Assert(defaultSeedProvider is not null);
        Debug.Assert(tryAggregator is not null);

        DefaultSeedProvider = defaultSeedProvider;
        TryAggregator = tryAggregator;
    }

    public AdHocAggregatingPremise
    (
        Func<TTarget> defaultSeedProvider,
        OptionalAggregator2D<TSource, TTarget> optionalAggregator
    )
    : this(defaultSeedProvider, optionalAggregator.ToTryAggregator())
    { }

    public Func<TTarget> DefaultSeedProvider { get; }

    public TryAggregator2D<TSource, TTarget> TryAggregator { get; }

    public TTarget DefaultAggregated => DefaultSeedProvider.Invoke();

    public bool TryAggregate(TTarget siblingsSeed, TTarget childrenSeed, TSource source, [NotNullWhen(true)] out TTarget? aggregated) =>
        TryAggregator.Invoke(siblingsSeed, childrenSeed, source, out aggregated);
}
#endif