namespace Nemonuri.Graphs.Infrastructure;

public interface IHomogeneousSuccessorAggregator
<
    TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
    TChildren, TDescendants, TChild, TGraph
> :
    IAggregatorContextPremise<TChildren, TDescendants, TChild, TGraph>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TNode, TNode, TOutArrow>
{
    TPrevious CreateInitialPrevious(scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context);

    TPrevious AggregateOuterPrevious
    (
        TPrevious left,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> right,
        scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context
    );

    TPrevious AggregateInnerPrevious
    (
        TPrevious left,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> right,
        scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> context
    );


    TPost CreateInitialPost(scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context);

    TPost CreateFallbackPost(scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> context);

    TPost AggregateInnerPost
    (
        TPost left,
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> right,
        scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> context
    );

    TPost AggregateOuterPost
    (
        TPost left,
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> right,
        scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context
    );


    TInArrow EmbedToInArrow(TOutArrow outArrow, scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> context);

    bool CanRunOuterPhase
    (
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phase,
        scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context
    );

    bool CanRunInnerPhase
    (
        LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phase,
        scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> context
    );
    
    TOutArrowSet GetDirectSuccessorArrows(TNode node, scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> context);
}
