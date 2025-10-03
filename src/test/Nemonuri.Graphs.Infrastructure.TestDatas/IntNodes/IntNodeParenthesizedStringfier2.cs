using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier2 : IHomogeneousSuccessorAggregator
<
    ValueNull, int /* Sibling index */, ValueNull, ValueNull, string,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    private readonly StringBuilder _sb = new();

    public IntNodeParenthesizedStringfier2()
    {
    }

    public int EmptyMutableSiblingContext => 0;

    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull AggregateOuterPrevious(scoped ref MutableContextRecord<ValueNull, int, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        var node = value.Snapshot.OuterNode;

        //--- graph open ---
        if (value.PhaseLabel.IsInitial())
        {
            _sb.Clear();
        }
        //---|

        //--- outer node open ---
        if (node.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(node.Value);
        //---|

        return default;
    }

    public ValueNull AggregateInnerPrevious(scoped ref MutableContextRecord<ValueNull, int, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        ref int siblingIndex = ref mutableContext.MutableSiblingContext;

        //--- inner list open ---
        _sb.Append(' ');
        if (siblingIndex == 0)
        {
            _sb.Append('(');
        }
        //---|
        
        siblingIndex++;

        return default;
    }

    public string EmptyPostAggregation => string.Empty;

    public string AggregateInnerPost(scoped ref MutableContextRecord<ValueNull, int, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> value)
    {
        throw new InvalidOperationException();
    }

    public string AggregateOuterPost(scoped ref MutableContextRecord<ValueNull, int, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> value)
    {
        int siblingIndex = mutableContext.MutableSiblingContext;

        //--- inner list close ---
        if (siblingIndex > 0)
        {
            _sb.Append(')');
        }
        //---|

        //--- outer node close ---
        var node = value.Snapshot.OuterNode;
        if (node.Children.Length > 0)
        {
            _sb.Append(')');
        }
        //---|

        return
        //--- graph close ---
            value.PhaseLabel.IsInitial() ? _sb.ToString() 
        //---|
            : EmptyPostAggregation;
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phaseSnapshot)
    {
        return true;
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel != InnerPhaseLabel.InnerPost;
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);

    public ValueNull CloneMutableDepthContext(ValueNull depthContext) => depthContext;
}
