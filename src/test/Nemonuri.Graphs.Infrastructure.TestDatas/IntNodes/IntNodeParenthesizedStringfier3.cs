using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier3 : IHomogeneousSuccessorAggregator
<
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string,
    IntNodeParenthesizedStringfier3.ChildrenScopeContext,
    ValueNull, ValueNull,
    IntNodeParenthesizedStringfier3.GraphScopeContext
>
{
    public readonly record struct ChildrenScopeContext(int TotalChildCount, int WrittenChildCount);

    public readonly record struct GraphScopeContext(StringBuilder StringBuilder);    

    public IntNodeParenthesizedStringfier3() { }

    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context) => default;

    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        context.ChildrenScopeContext = context.ChildrenScopeContext with { TotalChildCount = right.Snapshot.OuterNode.Children.Length };

        if (context.ChildrenScopeContext.TotalChildCount > 0)
        {
            context.GraphScopeContext.StringBuilder.Append('(');
        }

        context.GraphScopeContext.StringBuilder.Append(right.Snapshot.OuterNode.Value);
        return default;
    }

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        var childrenContext = context.ChildrenScopeContext;

        if (!(childrenContext.TotalChildCount > 0)) { return default; }

        var graphContext = context.GraphScopeContext;
        if (childrenContext.WrittenChildCount == 0)
        {
            graphContext.StringBuilder.Append(" (");
        }
        else if (childrenContext.WrittenChildCount > 0)
        {
            graphContext.StringBuilder.Append(' ');
        }

        return default;
    }

    public string CreateInitialPost(scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context) => string.Empty;

    public string CreateFallbackPost(scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context) => string.Empty;

    public string AggregateInnerPost(string left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        var childrenContext = context.ChildrenScopeContext;

        if (!(childrenContext.TotalChildCount > 0)) { goto Return; }

        childrenContext = childrenContext with { WrittenChildCount = childrenContext.WrittenChildCount + 1 };
        if (childrenContext.WrittenChildCount == childrenContext.TotalChildCount)
        {
            context.GraphScopeContext.StringBuilder.Append(')');
        }

        context.ChildrenScopeContext = childrenContext;

    Return:
        return CreateFallbackPost(ref context);
    }

    public string AggregateOuterPost(string left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        if (context.ChildrenScopeContext.TotalChildCount > 0)
        {
            context.GraphScopeContext.StringBuilder.Append(')');
        }

        return right.PhaseLabel.IsInitial() ? context.GraphScopeContext.StringBuilder.ToString() : CreateInitialPost(ref context);
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phase, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        return true;
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phase, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context)
    {
        return true;
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node, scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> context) => new(node);

    public GraphScopeContext CreateInitialGraphScopeContext() => new(new());

    public ValueNull CreateInitialDescendantsScopeContext(scoped ref GraphScopeContext graph) => default;

    public ValueNull CreateNextDescendantsScopeContext(scoped ref readonly ValueNull descendants, scoped ref readonly ChildrenScopeContext children, scoped ref GraphScopeContext graph) => default;

    public ChildrenScopeContext CreateInitialChildrenScopeContext(scoped ref readonly ValueNull descendants, scoped ref GraphScopeContext graph) => new(0, 0);

    public ValueNull CreateInitialChildScopeContext(scoped ref readonly ChildrenScopeContext children, scoped ref readonly ValueNull descendants, scoped ref GraphScopeContext graph) => default;

    public void DisposeChildScopeContext(scoped ref ValueNull child, scoped ref GraphScopeContext graph) { }

    public void DisposeChildrenScopeContext(scoped ref ChildrenScopeContext children, scoped ref GraphScopeContext graph) { }

    public void DisposeDescendantsScopeContext(scoped ref ValueNull descendants, scoped ref GraphScopeContext graph) { }

    public void DisposeGraphScopeContext(scoped ref GraphScopeContext graph) { }

    public AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> CreateInnerAggregatorContext(ref ChildrenScopeContext childrenScopeContext, ref ValueNull descendantsScopeContext, ref ValueNull childScopeContext, ref GraphScopeContext graphScopeContext)
    {
        return AggregatorContextTheory.CreateInnerAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref childScopeContext, ref graphScopeContext);
    }

    public AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> CreateOuterAggregatorContext(ref ChildrenScopeContext childrenScopeContext, ref ValueNull descendantsScopeContext, ref GraphScopeContext graphScopeContext)
    {
        return AggregatorContextTheory.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
    }

    public void DeconstructOuterAggregatorContext(scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> aggregatorContext, out ChildrenScopeContext childrenScopeContext, out ValueNull descendantsScopeContext, out GraphScopeContext graphScopeContext)
    {
        AggregatorContextTheory.DeconstructOuterAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
    }

    public void DeconstructInnerAggregatorContext(scoped ref AggregatorContext<ChildrenScopeContext, ValueNull, ValueNull, GraphScopeContext> aggregatorContext, out ChildrenScopeContext childrenScopeContext, out ValueNull descendantsScopeContext, out ValueNull childScopeContext, out GraphScopeContext graphScopeContext)
    {
        AggregatorContextTheory.DeconstructInnerAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out childScopeContext, out graphScopeContext);
    }
}