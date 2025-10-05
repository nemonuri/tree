namespace Nemonuri.Graphs.Infrastructure;

public interface IHomogeneousSuccessorAggregator
<
    TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet
> :
    ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    TMutableSiblingContext EmptyMutableSiblingContext { get; }

    TMutableInnerSiblingContext EmptyMutableInnerSiblingContext { get; }

    TMutableDepthContext CloneMutableDepthContext(TMutableDepthContext depthContext);

    TPrevious EmptyPreviousAggregation { get; }

    TPrevious AggregateOuterPrevious
    (
        scoped ref MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPrevious source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );

    TPrevious AggregateInnerPrevious
    (
        scoped ref MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext> mutableContext, TPrevious source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );


    TPost EmptyPostAggregation { get; }

    TPost AggregateInnerPost
    (
        scoped ref MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext> mutableContext, TPost source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );

    TPost AggregateOuterPost
    (
        scoped ref MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext, TPost source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );


    TInArrow EmbedToInArrow(TOutArrow outArrow);

    bool CanRunOuterPhase
    (
        scoped ref readonly MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> context,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot
    );
    
    bool CanRunInnerPhase
    (
        scoped ref readonly MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext> context,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot
    );
}
