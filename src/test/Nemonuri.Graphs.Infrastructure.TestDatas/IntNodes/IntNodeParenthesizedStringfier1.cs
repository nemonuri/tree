using System.Diagnostics;
using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier1 : IHomogeneousSuccessorAggregator
<
    IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string,
    ValueNull, ValueNull, ValueNull, ValueNull
>
{
    private readonly StringBuilder _sb = new();
    public IntNodeParenthesizedStringfier1()
    {
    }

    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => default;

    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        _sb.Clear();
        if (right.Snapshot.OuterNode.Children.Length > 0)
        {
            _sb.Append('(');
        }
        _sb.Append(right.Snapshot.OuterNode.Value);

        Debug.WriteLine($"{nameof(AggregateOuterPrevious)} {_sb.ToString()}");
        return default;
    }

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        _sb.Append(' ');

        if (right.Snapshot.OutArrow.Index == 0)
        {
            _sb.Append('(');
        }

        if (right.Snapshot.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(right.Snapshot.OutArrow.Head.Value);

        Debug.WriteLine($"{nameof(AggregateInnerPrevious)} {_sb.ToString()}");
        return default;
    }

    public string CreateInitialPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => string.Empty;

    public string CreateFallbackPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => CreateInitialPost(ref context);

    public string AggregateInnerPost(string left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        if (right.Snapshot.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append(')');
        }

        if
        (
            right.Snapshot.OutArrow.Index == right.Snapshot.OutArrowSet.Count - 1
        )
        {
            _sb.Append(')');
        }

        Debug.WriteLine($"{nameof(AggregateInnerPost)} {_sb.ToString()}");
        return CreateFallbackPost(ref context);
    }

    public string AggregateOuterPost(string left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        if (right.Snapshot.OuterNode.Children.Length > 0)
        {
            _sb.Append(')');
        }

        Debug.WriteLine($"{nameof(AggregateOuterPost)} {_sb.ToString()}");
        return right.PhaseLabel.IsInitial() ? _sb.ToString() : CreateFallbackPost(ref context);
    }

    public IndexedIntNodeArrow EmbedToInArrow(IndexedIntNodeArrow outArrow, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IndexedIntNodeArrow, ValueNull, string>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel.IsInitial();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet, ValueNull, string>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return true;
    }

    public IndexedIntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => new(node);
    public ValueNull CreateInitialGraphScopeContext() => default;

    public ValueNull CreateInitialDescendantsScopeContext(scoped ref ValueNull graph) => default;

    public ValueNull CreateNextDescendantsScopeContext(scoped ref readonly ValueNull descendants, scoped ref readonly ValueNull children, scoped ref ValueNull graph) => default;

    public ValueNull CreateInitialChildrenScopeContext(scoped ref readonly ValueNull descendants, scoped ref ValueNull graph) => default;

    public ValueNull CreateInitialChildScopeContext(scoped ref readonly ValueNull children, scoped ref readonly ValueNull descendants, scoped ref ValueNull graph) => default;

    public void DisposeChildScopeContext(scoped ref ValueNull child, scoped ref ValueNull graph) { }

    public void DisposeChildrenScopeContext(scoped ref ValueNull children, scoped ref ValueNull graph) { }

    public void DisposeDescendantsScopeContext(scoped ref ValueNull descendants, scoped ref ValueNull graph) { }

    public void DisposeGraphScopeContext(scoped ref ValueNull graph) { }

    public AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> CreateInnerAggregatorContext(ref ValueNull childrenScopeContext, ref ValueNull descendantsScopeContext, ref ValueNull childScopeContext, ref ValueNull graphScopeContext)
    {
        return AggregatorContextTheory.CreateInnerAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref childScopeContext, ref graphScopeContext);
    }

    public AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> CreateOuterAggregatorContext(ref ValueNull childrenScopeContext, ref ValueNull descendantsScopeContext, ref ValueNull graphScopeContext)
    {
        return AggregatorContextTheory.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
    }

    public void DeconstructOuterAggregatorContext(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> aggregatorContext, out ValueNull childrenScopeContext, out ValueNull descendantsScopeContext, out ValueNull graphScopeContext)
    {
        AggregatorContextTheory.DeconstructOuterAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
    }

    public void DeconstructInnerAggregatorContext(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> aggregatorContext, out ValueNull childrenScopeContext, out ValueNull descendantsScopeContext, out ValueNull childScopeContext, out ValueNull graphScopeContext)
    {
        AggregatorContextTheory.DeconstructInnerAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out childScopeContext, out graphScopeContext);
    }

}
