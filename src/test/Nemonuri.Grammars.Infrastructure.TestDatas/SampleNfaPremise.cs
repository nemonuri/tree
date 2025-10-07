using System.Collections.Immutable;
using CommunityToolkit.Diagnostics;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SampleNfaPremise<T, TExtra> : INfaPremise
<
    ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>,
    NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet,
    int, IReadOnlyList<T>, SequenceLattice<T>, TExtra
>
{
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> NodeMap { get; }
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, TExtra>> ScanMap { get; }
    private readonly SequenceLatticePremise<T> _idealPremise;

    public SampleNfaPremise
    (
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> nodeMap,
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, TExtra>> scanMap
    )
    {
        Guard.IsNotNull(nodeMap);
        Guard.IsNotNull(scanMap);

        NodeMap = nodeMap;
        ScanMap = scanMap;
        _idealPremise = new();
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

    public SequenceIdealContext<T, NodeId> CloneMutableDepthContext(SequenceIdealContext<T, NodeId> depthContext) => depthContext.Clone();

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> EmptyPreviousAggregation => [];

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> AggregateOuterPrevious(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> mutableContext, ImmutableList<SequenceLatticeSnapshot<T, TExtra>> source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> value)
    {
        return source;
    }

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> AggregateInnerPrevious(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> mutableContext, ImmutableList<SequenceLatticeSnapshot<T, TExtra>> source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> value)
    {
        ScanResult<int, TExtra> scanResult = ScanResultArgument;
        var ideal = mutableContext.MutableDepthContext.CurrentIdeal;

        if (!scanResult.IsSuccess) { return source; }

        if (!WellFoundedRelationTheory.TryCreateLesserIdeal<SequenceLatticePremise<T>, int, IReadOnlyList<T>, SequenceLattice<T>>(_idealPremise, ideal, scanResult.UpperBound, out var lesserIdeal))
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

        mutableContext.MutableDepthContext.CurrentIdeal = lesserIdeal;
        SequenceLatticeSnapshot<T, TExtra> snapshot = new(sublattice, scanResult.Extra);
        return source.Add(snapshot);
    }

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> EmptyPostAggregation => [];

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> AggregateInnerPost(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> mutableContext, ImmutableList<SequenceLatticeSnapshot<T, TExtra>> source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> value)
    {
        return source;
    }

    public ImmutableList<SequenceLatticeSnapshot<T, TExtra>> AggregateOuterPost(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> mutableContext, ImmutableList<SequenceLatticeSnapshot<T, TExtra>> source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> value)
    {
        return source;
    }

    public NodeIdArrow EmbedToInArrow(NodeIdArrow outArrow) => outArrow;

    public bool CanRunOuterPhase(scoped ref readonly MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> phaseSnapshot)
    {
        return true;
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>, ImmutableList<SequenceLatticeSnapshot<T, TExtra>>>> phaseSnapshot)
    {
        return true;
    }

    public NodeIdOutArrowSet GetDirectSuccessorArrows(NodeId node)
    {
        throw new NotImplementedException();
    }


}
