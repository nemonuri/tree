namespace Nemonuri.Graphs.Infrastructure;

public interface IHomogeneousSuccessorAggregator
<
    TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet
> :
    ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    TMutableSiblingContext EmptyMutableSiblingContext { get; }

    TMutableDepthContext CloneMutableDepthContext(TMutableDepthContext depthContext);

    TPrevious EmptyPreviousAggregation { get; }

    TPrevious AggregateOuterPrevious
    (
        scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPrevious source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );

    TPrevious AggregateInnerPrevious
    (
        scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPrevious source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );


    TPost EmptyPostAggregation { get; }

    TPost AggregateInnerPost
    (
        scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPost source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );

    TPost AggregateOuterPost
    (
        scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPost source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );


    TInArrow EmbedToInArrow(TOutArrow outArrow);

    bool CanRunOuterPhase
    (
        scoped ref readonly MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> context,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot
    );
    bool CanRunInnerPhase
    (
        scoped ref readonly MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> context,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot
    );
}
