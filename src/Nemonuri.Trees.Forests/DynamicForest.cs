using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests;

public class DynamicForest
<TForest, TForestKey, TForestKeyCollection,
 TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
 TForestPremise> :
    IDynamicForest
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
         TForestPremise>,
        TForest, TForestKey, TForestKeyCollection,
        TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
        TForestPremise
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
    where TForestSequenceUnion : IEnumerable<TForestSequence>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TForestUnionSequence : IEnumerable<TForestUnion>
    where TForestMatrix : IEnumerableMatrix<TForest, TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence>
    where TForestPremise : IForestPremise
    <TForest, TForestKey, TForestKeyCollection,
     TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix>
{
    private readonly TForestPremise _premise;

    private TForestUnion _forestUnion;
    private int _forestUnionVersion = 0;

    public IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
         TForestPremise>
    > _extraChildren;
    private int _extraChildrenVersion = 0;

    private TForestKeyCollection? _value;
    private int _valueVersion = 0;

    private TForestMatrix? _childrenFromForestUnion;
    private int _childrenFromForestUnionVersion = 0;

    private IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
         TForestPremise>
    >?
    _castedChildrenFromForestUnion;
    private int _castedChildrenFromForestUnionVersion = 0;

    private IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
         TForestPremise>
    >?
    _aggregatedChildren;
    private (int ForestUnion, int ExtraChildren) _aggregatedChildrenVersion = (0,0);


    public DynamicForest
    (
        TForestPremise premise,
        TForestUnion forestUnion,
        IEnumerable
        <
            DynamicForest
            <TForest, TForestKey, TForestKeyCollection,
            TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
            TForestPremise>
        > extraChildren
    )
    {
        Guard.IsNotNull(premise);

        _premise = premise;
        ForestUnion = forestUnion;
        ExtraChildren = extraChildren;
    }

    //public DynamicForest(TForestPremise premise, TForestUnion forestUnion)

    public TForestPremise Premise => _premise;

    public TForestUnion ForestUnion
    {
        get => _forestUnion;

        [MemberNotNull(nameof(_forestUnion))]
        set
        {
            Guard.IsNotNull(value);
            _forestUnion = value;
            _forestUnionVersion++;
        }
    }

    public IEnumerable
    <
        DynamicForest
        <TForest, TForestKey, TForestKeyCollection,
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix,
         TForestPremise>
    > ExtraChildren
    {
        get => _extraChildren;

        [MemberNotNull(nameof(_extraChildren))]
        set
        {
            Guard.IsNotNull(value);
            _extraChildren = value;
            _extraChildrenVersion++;
        }
    }

    public TForestKeyCollection Value
    {
        get
        {
            //--- update `_value` if needed ---
            if (_forestUnionVersion > _valueVersion)
            {
                TForestKeyCollection l = _premise.CreateEmptyForestKeyCollection();
                foreach (var forest in _forestUnion)
                {
                    l = _premise.AggregateForestKeyCollection(l, forest.Value);
                }
                _value = l;
                _valueVersion = _forestUnionVersion;
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
         TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix, TForestPremise>
    >
    Children
    {
        get
        {
            //--- update `_internalChildren` if needed ---
            if (_forestUnionVersion > _childrenFromForestUnionVersion)
            {
                TForestMatrix l = _premise.CreateEmptyForestMatrix();
                foreach (var forest in _forestUnion)
                {
                    l = _premise.AggregateForestMatrixAsSequenceUnion(l, _premise.CastChildren(forest).EnumerateRows());
                }
                _childrenFromForestUnion = l;
                _childrenFromForestUnionVersion = _forestUnionVersion;
            }
            //---|
            Guard.IsNotNull(_childrenFromForestUnion);

            //--- update `_castedChildrenVersion` if needed ---
            if (_childrenFromForestUnionVersion > _castedChildrenFromForestUnionVersion)
            {
                _castedChildrenFromForestUnion = _childrenFromForestUnion.EnumerateColumns().Select
                (
                    forestUnion => new
                    DynamicForest
                    <TForest, TForestKey, TForestKeyCollection,
                     TForestSequence, TForestUnion, TForestSequenceUnion, TForestUnionSequence, TForestMatrix, TForestPremise>
                    (
                        _premise, forestUnion, []
                    )
                );
                _castedChildrenFromForestUnionVersion = _childrenFromForestUnionVersion;
            }
            //---|
            Guard.IsNotNull(_castedChildrenFromForestUnion);

            //--- update `_aggregatedChildren` if needed ---
            {
                var version = (_castedChildrenFromForestUnionVersion, _extraChildrenVersion);
                if (_aggregatedChildrenVersion != version)
                {
                    _aggregatedChildren = _castedChildrenFromForestUnion.Concat(_extraChildren);
                    _aggregatedChildrenVersion = version;
                }
            }
            //---|
            Guard.IsNotNull(_aggregatedChildren);

            return _aggregatedChildren;
        }
    }
}

