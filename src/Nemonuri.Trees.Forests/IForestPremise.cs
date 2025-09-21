namespace Nemonuri.Trees.Forests;

public interface IForestPremise
<TForest, TForestKey, TForestKeyCollection,
 TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix>
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
    where TForestSequenceCollection : IEnumerable<TForestSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestUnionCollection : IEnumerable<TForestUnion>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TForestMatrix CastChildren(TForest forest);

    TForestMatrix FilterByKey(TForestMatrix forestMatrix, TForestKey forestKey);

    TForestMatrix CreateEmptyForestMatrix();
    TForestMatrix AggregateForestMatrix(TForestMatrix l, TForestMatrix r);

    TForestKeyCollection CreateEmptyForestKeyCollection();
    TForestKeyCollection AggregateForestKeyCollection(TForestKeyCollection l, TForestKeyCollection r);
}
