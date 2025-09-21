namespace Nemonuri.Trees.Forests;

public interface IDynamicForest
<TDynamicForest,
 TForest, TForestKey, TForestKeyCollection,
 TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
 TForestPremise> :
    IForest<TDynamicForest, TForestKey, TForestKeyCollection>
where TDynamicForest :
    IDynamicForest
    <TDynamicForest, TForest, TForestKey, TForestKeyCollection,
     TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
     TForestPremise>
where TForest : IForest<TForest, TForestKey, TForestKeyCollection>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
#if NET9_0_OR_GREATER
where TForestKey : allows ref struct
#endif
where TForestKeyCollection : IEnumerable<TForestKey>
where TForestSequence : IEnumerable<TForest>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
where TForestUnion : IEnumerable<TForest>
where TForestSequenceUnion : IEnumerable<TForestSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
where TForestUnionSequence : IEnumerable<TForestUnion>
where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence>
where TForestPremise : IForestPremise<TForest, TForestKey, TForestKeyCollection, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix>
{
    TForestPremise Premise { get; }
    TForestUnion ForestUnion { get; set; }
    IEnumerable<TDynamicForest> ExtraChildren { get; set; }
}
