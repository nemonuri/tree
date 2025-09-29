namespace Nemonuri.Graphs.Infrastructure;

public interface IHomogeneousSuccessorAggregator
<
    TMutableContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet
> :
    ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    TPrevious EmptyPreviousAggregation { get; }

    TPrevious AggregateOuterPrevious
    (
        scoped ref TMutableContext mutableContext, TPrevious source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );

    TPrevious AggregateInnerPrevious
    (
        scoped ref TMutableContext mutableContext, TPrevious source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );


    TPost EmptyPostAggregation { get; }

    TPost AggregateInnerPost
    (
        scoped ref TMutableContext mutableContext, TPost source,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value
    );

    TPost AggregateOuterPost
    (
        scoped ref TMutableContext mutableContext, TPost source,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value
    );


    TInArrow EmbedToInArrow(TOutArrow outArrow);

    bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot);
    bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot);
}
