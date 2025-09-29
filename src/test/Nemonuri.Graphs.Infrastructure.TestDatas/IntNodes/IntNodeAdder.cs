using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public readonly struct IntNodeAdder() : IHomogeneousSuccessorAggregator
<
    NullValue, NullValue, int,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    public NullValue EmptyPreviousAggregation => default;

    public NullValue AggregateOuterPrevious(scoped ref NullValue mutableContext, NullValue source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, NullValue, int>> value)
    {
        throw new InvalidOperationException();
        //return default;
    }

    public NullValue AggregateInnerPrevious(scoped ref NullValue mutableContext, NullValue source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, NullValue, int>> value)
    {
        throw new InvalidOperationException();
        //return default;
    }

    public int EmptyPostAggregation => 0;

    public int AggregateInnerPost(scoped ref NullValue mutableContext, int source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, NullValue, int>> value)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {source} + {value.Snapshot.PostAggregation}");
        return source + value.Snapshot.PostAggregation;
    }

    public int AggregateOuterPost(scoped ref NullValue mutableContext, int source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, NullValue, int>> value)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {source} + {value.Snapshot.OuterNode.Value}");
        return source + value.Snapshot.OuterNode.Value;
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, NullValue, int>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsPost();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, NullValue, int>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsPostOrMoment();
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);
}