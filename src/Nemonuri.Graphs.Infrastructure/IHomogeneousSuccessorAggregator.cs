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

    TPrevious AggregatePreviousToInitialNode(scoped ref TMutableContext mutableContext, TPrevious source, TNode value);

    TPrevious AggregatePrevious(scoped ref TMutableContext mutableContext, TPrevious source,
                                PhaseSnapshot<TNode, TInArrow, TOutArrow, TPrevious, TPost> value);


    TPost EmptyPostAggregation { get; }

    TPost AggregatePostToInitialNode(scoped ref TMutableContext mutableContext, TPost source, TNode value);

    TPost AggregatePost(scoped ref TMutableContext mutableContext, TPost source,
                        PhaseSnapshot<TNode, TInArrow, TOutArrow, TPrevious, TPost> value);


    TInArrow EmbedToInArrow(TOutArrow outArrow);

    bool CanRunPhase(PhaseSnapshot<TNode, TInArrow, TOutArrow, TPrevious, TPost> snapshot, AggregatingPhase phase);
}
