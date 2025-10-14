using CommunityToolkit.Diagnostics;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SampleNfaPremise<T, TExtra, TAggregationSequence, TAggregationSequenceUnion> : INfaPremise
<
    ValueNull, ValueNull, TAggregationSequence, ValueNull, ValueNull, TAggregationSequenceUnion,
    NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet,
    int, IReadOnlyList<T>, SequenceLattice<T>, SequenceIdealContext<T, NodeId>, TExtra
>
{
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> NodeMap { get; }
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, TExtra>> ScanMap { get; }
    private readonly SequenceLatticePremise<T> _idealPremise;
    private readonly Dictionary<NodeId, NodeIdOutArrowSet> _nodeIdCache;
    private readonly IInitialSourceGivenAggregator<TAggregationSequence, SequenceLatticeSnapshot<T, TExtra>> _sequenceAggregator;
    private readonly IInitialSourceGivenAggregator<TAggregationSequenceUnion, TAggregationSequenceUnion> _sequenceUnionAggregator;
    private readonly Func<TAggregationSequence, TAggregationSequence> _sequenceCloner;
    private readonly Func<IReadOnlyList<T>, TAggregationSequence, TAggregationSequenceUnion> _sequenceToSequenceUnionCaster;

    public SampleNfaPremise
    (
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> nodeMap,
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, TExtra>> scanMap,
        IInitialSourceGivenAggregator<TAggregationSequence, SequenceLatticeSnapshot<T, TExtra>> sequenceAggregator,
        IInitialSourceGivenAggregator<TAggregationSequenceUnion, TAggregationSequenceUnion> sequenceUnionAggregator,
        Func<TAggregationSequence, TAggregationSequence> sequenceCloner,
        Func<IReadOnlyList<T>, TAggregationSequence, TAggregationSequenceUnion> sequenceToSequenceUnionCaster
    )
    {
        Guard.IsNotNull(nodeMap);
        Guard.IsNotNull(scanMap);
        Guard.IsNotNull(sequenceAggregator);
        Guard.IsNotNull(sequenceUnionAggregator);
        Guard.IsNotNull(sequenceCloner);
        Guard.IsNotNull(sequenceToSequenceUnionCaster);

        NodeMap = nodeMap;
        ScanMap = scanMap;
        _idealPremise = new();
        _nodeIdCache = new();
        _sequenceAggregator = sequenceAggregator;
        _sequenceUnionAggregator = sequenceUnionAggregator;
        _sequenceCloner = sequenceCloner;
        _sequenceToSequenceUnionCaster = sequenceToSequenceUnionCaster;
    }

    public void SetScanResultArgument(ScanResult<int, TExtra> scanResult)
    {
        ScanResultArgument = scanResult;
    }

    public ScanResult<int, TExtra> ScanResultArgument { get; private set; }

    public ScanResult<int, TExtra> Scan(NodeIdArrow arrow, SequenceLattice<T> ideal)
    {
        return ScanMap[arrow.NodeArrowId].Premise.Scan(arrow.NodeArrowId, ideal);
    }

    public SequenceLattice<T> CreateIdeal(IReadOnlyList<T> set, int upperBound) => _idealPremise.CreateIdeal(set, upperBound);

    public bool IsMinimalElement(IReadOnlyList<T> set, int item) => _idealPremise.IsMinimalElement(set, item);

    public bool IsEmpty(IReadOnlyList<T> set) => _idealPremise.IsEmpty(set);

    public bool IsMember(IReadOnlyList<T> set, int item) => _idealPremise.IsMember(set, item);

    public bool IsLesserOrEqualThan(int less, int greater) => _idealPremise.IsLesserOrEqualThan(less, greater);

    public bool AreEqual(int left, int right) => _idealPremise.AreEqual(left, right);

    public IReadOnlyList<T> GetCanonicalSuperset(SequenceLattice<T> subset) => _idealPremise.GetCanonicalSuperset(subset);

    public ValueNull EmptyMutableSiblingContext => default;

    public ValueNull EmptyMutableInnerSiblingContext => default;

    public ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>> CloneMutableDepthContext(ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>> depthContext)
    {
        return new(_sequenceCloner(depthContext.Value), depthContext.IdealContext.Clone());
    }

    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull AggregateOuterPrevious
    (
        scoped ref MutableOuterContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>> mutableContext,
        ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ValueNull, TAggregationSequenceUnion>> value
    )
    {
        return source;
    }

    public ValueNull AggregateInnerPrevious
    (
        scoped ref MutableInnerContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>, ValueNull> mutableContext,
        ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ValueNull, TAggregationSequenceUnion>> value
    )
    {
        ScanResult<int, TExtra> scanResult = ScanResultArgument;
        var ideal = mutableContext.MutableDepthContext.IdealContext.CurrentIdeal;

        if (!scanResult.IsSuccess) { return source; }

        if (!WellFoundedRelationTheory.TryCreateLesserIdeal<SequenceLatticePremise<T>, int, IReadOnlyList<T>, SequenceLattice<T>>(_idealPremise, ideal, scanResult.UpperBound, false, out var lesserIdeal))
        {
            return source;
        }

        if
        (
            !LatticeTheory.TryCreateSublattice<SequenceLatticePremise<T>, int, IReadOnlyList<T>, SequenceLattice<T>>
            (
                _idealPremise, ideal, lesserIdeal.LeastUpperBound, ideal.LeastUpperBound, out SequenceLattice<T> sublattice
            )
        )
        {
            return source;
        }

        mutableContext.MutableDepthContext.IdealContext.CurrentIdeal = lesserIdeal;

        SequenceLatticeSnapshot<T, TExtra> snapshot = new(sublattice, scanResult.Extra);
        mutableContext.MutableDepthContext.Value = _sequenceAggregator.Aggregate(mutableContext.MutableDepthContext.Value, snapshot);
        return default;
    }

    public TAggregationSequenceUnion EmptyPostAggregation => _sequenceUnionAggregator.InitialSource;

    public TAggregationSequenceUnion AggregateInnerPost
    (
        scoped ref MutableInnerContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>, ValueNull> mutableContext,
        TAggregationSequenceUnion source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ValueNull, TAggregationSequenceUnion>> value
    )
    {
        TAggregationSequenceUnion v1 = _sequenceToSequenceUnionCaster(mutableContext.MutableDepthContext.IdealContext.CurrentIdeal.Canon, mutableContext.MutableDepthContext.Value);
        TAggregationSequenceUnion v2 = _sequenceUnionAggregator.Aggregate(value.Snapshot.PostAggregation, source);
        TAggregationSequenceUnion v3 = _sequenceUnionAggregator.Aggregate(v2, v1);

        return v3;
    }

    public TAggregationSequenceUnion AggregateOuterPost(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>> mutableContext, TAggregationSequenceUnion source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ValueNull, TAggregationSequenceUnion>> value)
    {
        return source;
    }

    public NodeIdArrow EmbedToInArrow(NodeIdArrow outArrow) => outArrow;

    public bool CanRunOuterPhase(scoped ref readonly MutableOuterContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ValueNull, TAggregationSequenceUnion>> phaseSnapshot)
    {
        return true;
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableInnerContextRecord<ValueNull, ValueNull, ValueWithIdealContext<TAggregationSequence, SequenceIdealContext<T, NodeId>>, ValueNull> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ValueNull, TAggregationSequenceUnion>> phaseSnapshot)
    {
        return true;
    }


    public NodeIdOutArrowSet GetDirectSuccessorArrows(NodeId node)
    {
        if (!_nodeIdCache.TryGetValue(node, out var result))
        {
            result = new(NodeMap, node);
            _nodeIdCache.Add(node, result);
        }
        
        return result;
    }

}
