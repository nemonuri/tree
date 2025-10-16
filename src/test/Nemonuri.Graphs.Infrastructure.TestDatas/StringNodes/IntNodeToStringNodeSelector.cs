using System.Collections.Immutable;
using Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.StringNodes;

public readonly struct IntNodeToStringNodeSelector : IHomogeneousSuccessorAggregator
<
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>,
    ValueNull, ValueNull, ValueNull, ValueNull
>
{
    private readonly Func<int, string> _selector;

    public IntNodeToStringNodeSelector(Func<int, string> selector)
    {
        _selector = selector;
    }

    public ValueNull CreateInitialPrevious(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => default;

    public ValueNull AggregateOuterPrevious(ValueNull left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return default;
    }

    public ValueNull AggregateInnerPrevious(ValueNull left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return default;
    }

    public ImmutableList<StringNode> CreateInitialPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => [];

    public ImmutableList<StringNode> CreateFallbackPost(scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => CreateInitialPost(ref context);

    public ImmutableList<StringNode> AggregateInnerPost(ImmutableList<StringNode> left, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        //--- select value ---
        string selected = _selector(right.Snapshot.OutArrow.Head.Value);
        //---|

        //--- create node from children ---
        StringNode created = (selected, left.ToArray());
        //---|

        //--- append created node ---
        ImmutableList<StringNode> appended = right.Snapshot.Post.Add(created);
        //---|

        return appended;
    }

    public ImmutableList<StringNode> AggregateOuterPost(ImmutableList<StringNode> left, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> right, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        string selected = _selector(right.Snapshot.OuterNode.Value);
        StringNode created = (selected, left.ToArray());
        return [created];
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context) => outArrow;

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel == OuterPhaseLabel.InitialOuterPost;
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> phase, scoped ref AggregatorContext<ValueNull, ValueNull, ValueNull, ValueNull> context)
    {
        return phase.PhaseLabel is InnerPhaseLabel.InnerPost or InnerPhaseLabel.InnerMoment;
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
