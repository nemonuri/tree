using System.Collections.Immutable;
using Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.StringNodes;

public readonly struct IntNodeToStringNodeSelector : IHomogeneousSuccessorAggregator
<
    ValueNull, ValueNull, ValueNull, ValueNull, ImmutableList<StringNode>,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    private readonly Func<int, string> _selector;

    public IntNodeToStringNodeSelector(Func<int, string> selector)
    {
        _selector = selector;
    }

    public ValueNull EmptyMutableSiblingContext => default;

    public ValueNull EmptyPreviousAggregation => default;

    public ValueNull AggregateOuterPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> value)
    {
        return default;
    }

    public ValueNull AggregateInnerPrevious(scoped ref MutableContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, ValueNull source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> value)
    {
        return default;
    }

    public ImmutableList<StringNode> EmptyPostAggregation => [];

    public ImmutableList<StringNode> AggregateInnerPost(scoped ref MutableContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, ImmutableList<StringNode> source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> value)
    {
        //--- select value ---
        string selected = _selector(value.Snapshot.OutArrow.Head.Value);
        //---|

        //--- create node from children ---
        StringNode created = (selected, source.ToArray());
        //---|

        //--- append created node ---
        ImmutableList<StringNode> appended = value.Snapshot.PostAggregation.Add(created);
        //---|

        return appended;
    }

    public ImmutableList<StringNode> AggregateOuterPost(scoped ref MutableContextRecord<ValueNull, ValueNull, ValueNull> mutableContext, ImmutableList<StringNode> source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> value)
    {
        string selected = _selector(value.Snapshot.OuterNode.Value);
        StringNode created = (selected, source.ToArray());
        return [created];
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);

    public ValueNull CloneMutableDepthContext(ValueNull depthContext) => depthContext;

    public bool CanRunOuterPhase(scoped ref readonly MutableContextRecord<ValueNull, ValueNull, ValueNull> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<IntNode, IntNodeArrow, ValueNull, ImmutableList<StringNode>>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel == OuterPhaseLabel.InitialOuterPost;
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableContextRecord<ValueNull, ValueNull, ValueNull> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet, ValueNull, ImmutableList<StringNode>>> phaseSnapshot)
    {
        return phaseSnapshot.PhaseLabel is InnerPhaseLabel.InnerPost or InnerPhaseLabel.InnerMoment;
    }
} 
