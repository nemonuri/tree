using System.Diagnostics;
using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeStringfier : IHomogeneousSuccessorAggregator
<
    ValueNull, ValueNull, ValueNull, ValueNull, ValueNull, string,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    // [Note] 오직 '부작용' 만으로 동작한다.

    private readonly StringBuilder _sb = new();
    public IntNodeStringfier() { }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;


    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);

    public ValueNull EmptyMutableSiblingContext => default;
    
    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull EmptyMutableInnerSiblingContext => default;

    public ValueNull AggregateOuterPrevious(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        if (value.PhaseLabel.IsInitial())
        {
            _sb.Clear();
        }
        else
        {
            _sb.Append(',');
        }

        _sb.Append(value.Snapshot.OuterNode.Value);
        Debug.WriteLine($"{nameof(AggregateOuterPrevious)} {_sb.ToString()}");
        return default;
    }

    public ValueNull AggregateInnerPrevious(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        throw new InvalidOperationException();
    }

    public string EmptyPostAggregation => string.Empty;

    public string AggregateInnerPost(scoped ref MutableInnerContextRecord<ValueNull, ValueNull, ValueNull, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        throw new InvalidOperationException();
    }

    public string AggregateOuterPost(scoped ref MutableOuterContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        return _sb.ToString();
    }

    public ValueNull CloneMutableDepthContext(ValueNull depthContext) => depthContext;

    public bool CanRunOuterPhase(scoped ref readonly MutableOuterContextRecord<ValueNull, ValueNull, ValueNull> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsPrevious() || phaseSnapshot.PhaseLabel == OuterPhaseLabel.InitialOuterPost;
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableInnerContextRecord<ValueNull, ValueNull, ValueNull, ValueNull> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel == InnerPhaseLabel.InnerMoment;
    }
}
