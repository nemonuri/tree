namespace Nemonuri.Trees.Forests;

public interface IDynamicForestAggregatingPremise
<
    TDynamicForest,
    TForest, TForestKey, TForestKeyCollection,
    TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
    TForestPremise,
    TConsumingSource, TResult, TConsumingSourceUnion,
    TAncestor, TAncestorsAggregation
>
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
{
    int CompareConsumingSource(TConsumingSource l, TConsumingSource r);

    TConsumingSourceUnion GetConsumedSources(TResult aggregation, TConsumingSource initialConsumingCource);

    TResult Consume(TForestKeyCollection keys, TConsumingSourceUnion consumedSources);

    TResult CreateEmptyResult();

    TResult Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TResult siblingsUnion,
        TResult childrenUnion,
        TResult extraChildrenUnion,
        TDynamicForest element
    );

}
