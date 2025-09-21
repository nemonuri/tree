using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests;

public class DynamicForest
<TForest, TForestKey, TForestKeyCollection,
 TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix, TForestPremise> :
    IForest
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix, TForestPremise>,
        TForestKey, TForestKeyCollection
    >
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
    where TForestSequenceCollection : IEnumerable<TForestSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestUnionCollection : IEnumerable<TForestUnion>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection>
    where TForestPremise : IForestPremise
    <TForest, TForestKey, TForestKeyCollection,
     TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix>
{
    private readonly TForestPremise _premise;

    private TForestUnion _internalForestUnion;
    private int _internalForestUnionVersion = 0;

    private TForestKeyCollection? _value;
    private int _valueVersion = 0;

    private TForestMatrix? _internalChildren;
    private int _internalChildrenVersion = 0;

    private IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix, TForestPremise>
    >?
    _castedChildren;
    private int _castedChildrenVersion = 0;

    public DynamicForest(TForestPremise premise, TForestUnion internalForestUnion)
    {
        Guard.IsNotNull(premise);
        Guard.IsNotNull(internalForestUnion);
        _premise = premise;
        _internalForestUnion = internalForestUnion;
        _internalForestUnionVersion++;
    }

    public TForestKeyCollection Value
    {
        get
        {
            //--- update `_value` if needed ---
            if (_internalForestUnionVersion > _valueVersion)
            {
                TForestKeyCollection l = _premise.CreateEmptyForestKeyCollection();
                foreach (var forest in _internalForestUnion)
                {
                    l = _premise.AggregateForestKeyCollection(l, forest.Value);
                }
                _value = l;
                _valueVersion = _internalForestUnionVersion;
            }
            //---|
            Guard.IsNotNull(_value);
            return _value;
        }
    }

    public IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix, TForestPremise>
    >
    Children
    {
        get
        {
            //--- update `_internalChildren` if needed ---
            if (_internalForestUnionVersion > _internalChildrenVersion)
            {
                TForestMatrix l = _premise.CreateEmptyForestMatrix();
                foreach (var forest in _internalForestUnion)
                {
                    l = _premise.AggregateForestMatrix(l, _premise.CastChildren(forest));
                }
                _internalChildren = l;
                _internalChildrenVersion = _internalForestUnionVersion;
            }
            //---|
            Guard.IsNotNull(_internalChildren);

            //--- update `_castedChildrenVersion` if needed ---
            if (_internalChildrenVersion > _castedChildrenVersion)
            {
                TForestUnion[] forestUnions = [.. _internalChildren.EnumerateColumns()];
                _castedChildren = forestUnions.Select
                (
                    forestUnion => new
                    DynamicForest
                    <TForest, TForestKey, TForestKeyCollection,
                     TForestSequence, TForestUnion, TForestSequenceCollection, TForestUnionCollection, TForestMatrix, TForestPremise>
                    (
                        _premise, forestUnion
                    )
                );
                _castedChildrenVersion = _internalChildrenVersion;
            }
            //---|
            Guard.IsNotNull(_castedChildren);

            return _castedChildren;
        }
    }
}

