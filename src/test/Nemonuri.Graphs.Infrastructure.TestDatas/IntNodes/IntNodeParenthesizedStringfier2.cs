#if false

using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier2 : IHomogeneousSuccessorAggregator
<
    ValueNull, ValueNull, string,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    private readonly StringBuilder _sb = new();
    private bool _outerPreviousAggregated = false;

    public IntNodeParenthesizedStringfier2()
    {
    }

    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull AggregateOuterPrevious(scoped ref ValueNull mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        var node = value.Snapshot.OuterNode;

        //value.Snapshot.InitialOrRecursiveInfo.AsRecursiveInfo.

        if (value.PhaseLabel.IsInitial())
        {
            _sb.ToString();
            _outerPreviousAggregated = false;
        }
    }

    public ValueNull AggregateInnerPrevious(scoped ref ValueNull mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        throw new NotImplementedException();
    }

    public string EmptyPostAggregation => string.Empty;

    public string AggregateInnerPost(scoped ref ValueNull mutableContext, string source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        throw new NotImplementedException();
    }

    public string AggregateOuterPost(scoped ref ValueNull mutableContext, string source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        throw new NotImplementedException();
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow)
    {
        throw new NotImplementedException();
    }

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node)
    {
        throw new NotImplementedException();
    }
}

#endif