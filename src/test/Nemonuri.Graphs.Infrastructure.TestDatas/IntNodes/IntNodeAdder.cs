using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public readonly struct IntNodeAdder() : IHomogeneousSuccessorAggregator
<
    ValueNull, ValueNull, ValueNull, int,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    public ValueNull EmptyMutableSiblingContext => default;

    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull AggregateOuterPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> value)
    {
        throw new InvalidOperationException();
        //return default;
    }

    public ValueNull AggregateInnerPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> value)
    {
        throw new InvalidOperationException();
        //return default;
    }

    public int EmptyPostAggregation => 0;

    public int AggregateInnerPost(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, int source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> value)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {source} + {value.Snapshot.PostAggregation}");
        return source + value.Snapshot.PostAggregation;
    }

    public int AggregateOuterPost(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, int source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> value)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {source} + {value.Snapshot.OuterNode.Value}");
        return source + value.Snapshot.OuterNode.Value;
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsPost();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsPostOrMoment();
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);
}
