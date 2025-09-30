using System.Diagnostics;
using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier1 : IHomogeneousSuccessorAggregator
<
    ValueNull, ValueNull, ValueNull, string,
    IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet
>
{
    private readonly StringBuilder _sb = new();
    public IntNodeParenthesizedStringfier1()
    {
    }

    public IndexedIntNodeArrow EmbedToInArrow(IndexedIntNodeArrow outArrow) => outArrow;

    public IndexedIntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);

    public ValueNull EmptyMutableSiblingContext => default;

    public ValueNull EmptyPreviousAggregation => default;

    public string EmptyPostAggregation => string.Empty;

    public ValueNull AggregateOuterPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> value)
    {
        _sb.Clear();
        if (value.Snapshot.OuterNode.Children.Length > 0)
        {
            _sb.Append('(');
        }
        _sb.Append(value.Snapshot.OuterNode.Value);

        Debug.WriteLine($"{nameof(AggregateOuterPrevious)} {_sb.ToString()}");
        return default;
    }

    public ValueNull AggregateInnerPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> value)
    {
        _sb.Append(' ');

        if (value.Snapshot.OutArrow.Index == 0)
        {
            _sb.Append('(');
        }

        if (value.Snapshot.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(value.Snapshot.OutArrow.Head.Value);

        Debug.WriteLine($"{nameof(AggregateInnerPrevious)} {_sb.ToString()}");
        return default;
    }

    public string AggregateInnerPost(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> value)
    {
        if (value.Snapshot.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append(')');
        }

        if
        (
            value.Snapshot.OutArrow.Index == value.Snapshot.OutArrowSet.Count - 1
        )
        {
            _sb.Append(')');
        }

        Debug.WriteLine($"{nameof(AggregateInnerPost)} {_sb.ToString()}");
        return EmptyPostAggregation;
    }

    public string AggregateOuterPost(scoped ref MutableContextRecord<ValueNull, ValueNull> mutableContext, string source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> value)
    {
        if (value.Snapshot.OuterNode.Children.Length > 0)
        {
            _sb.Append(')');
        }

        Debug.WriteLine($"{nameof(AggregateOuterPost)} {_sb.ToString()}");
        return value.PhaseLabel.IsInitial() ? _sb.ToString() : EmptyPostAggregation;
    }

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel.IsInitial();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> phaseSnapshot)
    {
        return true;
    }
}
