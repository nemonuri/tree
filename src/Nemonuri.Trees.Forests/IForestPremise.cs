namespace Nemonuri.Trees.Forests;

public interface IForestPremise
<TForest, TForestKey, TForestKeyCollection,
 TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix>
    where TForest : IForest<TForest, TForestKey, TForestKeyCollection>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
#if NET9_0_OR_GREATER
    where TForestKey : allows ref struct
#endif
    where TForestKeyCollection : IEnumerable<TForestKey>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestSequence : IEnumerable<TForest>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestUnion : IEnumerable<TForest>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestSequenceUnion : IEnumerable<TForestSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestUnionSequence : IEnumerable<TForestUnion>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TForestMatrix CastChildren(TForest forest);

    TForestMatrix CreateEmptyForestMatrix();
    TForestMatrix AggregateForestMatrixAsSequenceUnion(TForestMatrix l, TForestSequenceUnion r);
    TForestMatrix AggregateForestMatrixAsUnionSequence(TForestMatrix l, TForestUnionSequence r);

    TForestKeyCollection CreateEmptyForestKeyCollection();
    TForestKeyCollection AggregateForestKeyCollection(TForestKeyCollection l, TForestKeyCollection r);

    TForestMatrix FilterByKey(TForestMatrix forestMatrix, TForestKey forestKey);
}
