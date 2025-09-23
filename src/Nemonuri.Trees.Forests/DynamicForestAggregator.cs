using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests;

public class DynamicForestAggregator
<
    TDynamicForest,
    TForest, TForestKey, TForestKeyCollection,
    TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
    TForestPremise,
    TConsumingSource, TResult, TConsumingSourceUnion,
    TAncestor, TAncestorsAggregation,
    TAncestorAggregator,
    TDynamicForestAggregatingPremise
> : IAggregator3D<TDynamicForest, TResult, TAncestor, TAncestorsAggregation>
    where TDynamicForest :
        IDynamicForest
        <TDynamicForest, TForest, TForestKey, TForestKeyCollection,
        TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
        TForestPremise>
    where TForest : IForest<TForest, TForestKey, TForestKeyCollection>
    where TForestKeyCollection : IEnumerable<TForestKey>
    where TForestSequence : IEnumerable<TForest>
    where TForestUnion : IEnumerable<TForest>
    where TForestSequenceUnion : IEnumerable<TForestSequence>
    where TForestUnionSequence : IEnumerable<TForestUnion>
    where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence>
    where TForestPremise : IForestPremise<TForest, TForestKey, TForestKeyCollection, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix>
    where TConsumingSourceUnion : IEnumerable<TConsumingSource>
    where TAncestorAggregator : IAggregator<TAncestor, TAncestorsAggregation>
    where TDynamicForestAggregatingPremise : IDynamicForestAggregatingPremise
    <
        TDynamicForest,
        TForest, TForestKey, TForestKeyCollection,
        TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
        TForestPremise,
        TConsumingSource, TResult, TConsumingSourceUnion,
        TAncestor, TAncestorsAggregation
    >
{
    private readonly TAncestorAggregator _ancestorAggregator;
    private readonly TDynamicForestAggregatingPremise _aggregatingPremise;
    private readonly TConsumingSource _initialConsumingCource;

    public DynamicForestAggregator(TAncestorAggregator ancestorAggregator, TDynamicForestAggregatingPremise aggregatingPremise, TConsumingSource initialConsumingCource)
    {
        Guard.IsNotNull(_ancestorAggregator);
        Guard.IsNotNull(_aggregatingPremise);
        Guard.IsNotNull(_initialConsumingCource);

        _ancestorAggregator = ancestorAggregator;
        _aggregatingPremise = aggregatingPremise;
        _initialConsumingCource = initialConsumingCource;
    }

    public TAncestorsAggregation InitialAncestorsAggregation => _ancestorAggregator.InitialAggregation;

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _ancestorAggregator.Aggregate(ancestorsAggregation, ancestor);

    public TResult InitialAggregation => _aggregatingPremise.CreateEmptyResult();

    public TResult Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TResult siblingsAggregation,
        TResult childrenAggregation,
        TDynamicForest element
    )
    {
        var consumedSources = _aggregatingPremise.GetConsumedSources(siblingsAggregation, _initialConsumingCource);
        var extraChildrenUnion = _aggregatingPremise.Consume(element.Value, consumedSources);
        var aggregation = _aggregatingPremise.Aggregate
        (
            ancestorsAggregation, siblingsAggregation, childrenAggregation,
            extraChildrenUnion, element
        );
        return aggregation;
    }
}

