using System.Text;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier2 : IHomogeneousSuccessorAggregator
<
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string,
    int /* Sibling index */, ValueNull, ValueNull, ValueNull
>
{
    private readonly StringBuilder _sb = new();

    public IntNodeParenthesizedStringfier2()
    {
    }

    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context) => default;
    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        var node = right.Snapshot.OuterNode;

        //--- graph open ---
        if (right.PhaseLabel.IsInitial())
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

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        ref int siblingIndex = ref context.ChildrenScopeContext;

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

    public string CreateInitialPost(scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context) => string.Empty;

    public string CreateFallbackPost(scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context) => CreateInitialPost(ref context);

    public string AggregateInnerPost(string left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> right, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        throw new InvalidOperationException();
    }

    public string AggregateOuterPost(string left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> right, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        int siblingIndex = context.ChildrenScopeContext;

        //--- inner list close ---
        if (siblingIndex > 0)
        {
            _sb.Append(')');
        }
        //---|

        //--- outer node close ---
        var node = right.Snapshot.OuterNode;
        if (node.Children.Length > 0)
        {
            _sb.Append(')');
        }
        //---|

        return
        //--- graph close ---
            right.PhaseLabel.IsInitial() ? _sb.ToString() 
        //---|
            : CreateFallbackPost(ref context);
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, string>> phase, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        return true;
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, string>> phase, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel != InnerPhaseLabel.InnerPost;
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node, scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> context) => new(node);

    public ValueNull CreateInitialGraphScopeContext() => default;

    public ValueNull CreateInitialDescendantsScopeContext(scoped ref ValueNull graph) => default;

    public ValueNull CreateNextDescendantsScopeContext(scoped ref readonly ValueNull descendants, scoped ref readonly int children, scoped ref ValueNull graph) => default;

    public int CreateInitialChildrenScopeContext(scoped ref readonly ValueNull descendants, scoped ref ValueNull graph) => 0;

    public ValueNull CreateInitialChildScopeContext(scoped ref readonly int children, scoped ref readonly ValueNull descendants, scoped ref ValueNull graph) => default;

    public void DisposeChildScopeContext(scoped ref ValueNull child, scoped ref ValueNull graph) { }

    public void DisposeChildrenScopeContext(scoped ref int children, scoped ref ValueNull graph) { }

    public void DisposeDescendantsScopeContext(scoped ref ValueNull descendants, scoped ref ValueNull graph) { }

    public void DisposeGraphScopeContext(scoped ref ValueNull graph) { }

    public AggregatorContext<int, ValueNull, ValueNull, ValueNull> CreateInnerAggregatorContext(ref int childrenScopeContext, ref ValueNull descendantsScopeContext, ref ValueNull childScopeContext, ref ValueNull graphScopeContext)
    {
        return AggregatorContextTheory.CreateInnerAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref childScopeContext, ref graphScopeContext);
    }

    public AggregatorContext<int, ValueNull, ValueNull, ValueNull> CreateOuterAggregatorContext(ref int childrenScopeContext, ref ValueNull descendantsScopeContext, ref ValueNull graphScopeContext)
    {
        return AggregatorContextTheory.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
    }

    public void DeconstructOuterAggregatorContext(scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> aggregatorContext, out int childrenScopeContext, out ValueNull descendantsScopeContext, out ValueNull graphScopeContext)
    {
        AggregatorContextTheory.DeconstructOuterAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
    }

    public void DeconstructInnerAggregatorContext(scoped ref AggregatorContext<int, ValueNull, ValueNull, ValueNull> aggregatorContext, out int childrenScopeContext, out ValueNull descendantsScopeContext, out ValueNull childScopeContext, out ValueNull graphScopeContext)
    {
        AggregatorContextTheory.DeconstructInnerAggregatorContext(ref aggregatorContext, out childrenScopeContext, out descendantsScopeContext, out childScopeContext, out graphScopeContext);
    }
}
