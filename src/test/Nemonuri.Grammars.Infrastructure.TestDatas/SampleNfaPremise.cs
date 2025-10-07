using System.Collections.Immutable;
using CommunityToolkit.Diagnostics;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SampleNfaPremise<T, TExtra> : INfaPremise
<
    ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>,
    NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet,
    int, IReadOnlyList<T>, SequenceIdeal<T>, TExtra
>
{
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> NodeMap { get; }
    public IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, TExtra>> ScanMap { get; }
    private readonly SequenceIdealPremise<T> _idealPremise;

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

    public ScanResult<int, TExtra> Scan(NodeIdArrow arrow, SequenceIdeal<T> ideal)
    {
        return ScanMap[arrow.NodeArrowId].Premise.Scan(arrow.NodeArrowId, ideal);
    }

    public SequenceIdeal<T> CreateIdeal(IReadOnlyList<T> set, int upperBound) => _idealPremise.CreateIdeal(set, upperBound);

    public IReadOnlyList<T> CastToSet(SequenceIdeal<T> ideal) => _idealPremise.CastToSet(ideal);

    public bool IsMinimalElement(IReadOnlyList<T> set, int item) => _idealPremise.IsMinimalElement(set, item);

    public bool IsEmpty(IReadOnlyList<T> set) => _idealPremise.IsEmpty(set);

    public bool IsMember(IReadOnlyList<T> set, int item) => _idealPremise.IsMember(set, item);

    public bool IsLesserThan(int less, int greater) => _idealPremise.IsLesserThan(less, greater);

    public ValueNull EmptyMutableSiblingContext => default;

    public ValueNull EmptyMutableInnerSiblingContext => default;

    public SequenceIdealContext<T, NodeId> CloneMutableDepthContext(SequenceIdealContext<T, NodeId> depthContext) => depthContext.Clone();

    public ImmutableList<SequenceChunk<T>> EmptyPreviousAggregation => [];

    public ImmutableList<SequenceChunk<T>> AggregateOuterPrevious(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> mutableContext, ImmutableList<SequenceChunk<T>> source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> value)
    {
        return source;
    }

    public ImmutableList<SequenceChunk<T>> AggregateInnerPrevious(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> mutableContext, ImmutableList<SequenceChunk<T>> source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> value)
    {
        ScanResult<int, TExtra> scanResult = ScanResultArgument;
        var ideal = mutableContext.MutableDepthContext.CurrentIdeal;
        //scanResult.UpperBound

        /*
        if (!WellFoundedRelationTheory.TryCreateLesserIdeal<SequenceIdealPremise<T>, int, IReadOnlyList<T>, SequenceIdeal<T>>(_idealPremise, ideal, scanResult.UpperBound, out var lesserIdeal))
        {
            return source;
        }
        */
        
    }

    public ImmutableList<SequenceChunk<T>> EmptyPostAggregation => throw new NotImplementedException();

    public ImmutableList<SequenceChunk<T>> AggregateInnerPost(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> mutableContext, ImmutableList<SequenceChunk<T>> source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> value)
    {
        throw new NotImplementedException();
    }

    public ImmutableList<SequenceChunk<T>> AggregateOuterPost(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> mutableContext, ImmutableList<SequenceChunk<T>> source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> value)
    {
        throw new NotImplementedException();
    }

    public NodeIdArrow EmbedToInArrow(NodeIdArrow outArrow)
    {
        throw new NotImplementedException();
    }

    public bool CanRunOuterPhase(scoped ref readonly MutableOuterContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<NodeId, NodeIdArrow, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableInnerContextRecord<ValueNull, ValueNull, SequenceIdealContext<T, NodeId>, ValueNull> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public NodeIdOutArrowSet GetDirectSuccessorArrows(NodeId node)
    {
        throw new NotImplementedException();
    }
}
