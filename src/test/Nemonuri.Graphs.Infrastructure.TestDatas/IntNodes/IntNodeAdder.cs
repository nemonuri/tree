using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public readonly struct IntNodeAdder() : IHomogeneousSuccessorAggregator
<
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int,
    ValueNull, ValueNull, ValueNull, ValueNull
>
{
    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return default;
    }

    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        throw new InvalidOperationException();
    }

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        throw new NotImplementedException();
    }

    public int CreateInitialPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return 0;
    }

    public int CreateFallbackPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        throw new NotImplementedException();
    }

    public int AggregateInnerPost(int left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {left} + {right.Snapshot.Post}");
        return left + right.Snapshot.Post;
    }

    public int AggregateOuterPost(int left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        Debug.WriteLine($"{nameof(AggregateInnerPost)} {left} + {right.Snapshot.OuterNode.Value}");
        return left + right.Snapshot.OuterNode.Value;
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return outArrow;
    }

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, int>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel.IsPost();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, int>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel.IsPostOrMoment();
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return new(node);
    }

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
