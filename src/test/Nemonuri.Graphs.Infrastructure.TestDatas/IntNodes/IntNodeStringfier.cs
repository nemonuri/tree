using System.Diagnostics;
using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeStringfier : IHomogeneousSuccessorAggregator
<
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string,
    ValueNull, ValueNull, ValueNull, ValueNull
>
{
    // [Note] 오직 '부작용' 만으로 동작한다.

    private readonly StringBuilder _sb = new();
    public IntNodeStringfier() { }

    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return default;
    }

    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        if (right.PhaseLabel.IsInitial())
        {
            _sb.Clear();
        }
        else
        {
            _sb.Append(',');
        }

        _sb.Append(right.Snapshot.OuterNode.Value);
        Debug.WriteLine($"{nameof(AggregateOuterPrevious)} {_sb.ToString()}");
        return default;
    }

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        throw new NotImplementedException();
    }

    public string CreateInitialPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => string.Empty;

    public string CreateFallbackPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => string.Empty;

    public string AggregateInnerPost(string left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        throw new NotImplementedException();
    }

    public string AggregateOuterPost(string left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return _sb.ToString();
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel.IsPrevious() || phase.PhaseLabel == OuterPhaseLabel.InitialOuterPost;
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel == InnerPhaseLabel.InnerMoment;
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => new(node);

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
