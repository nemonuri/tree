namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatingTheory
{
    public static TAggregation Aggregate
    <
        TMutableContext, TAggregation, TAggregationEmptyPremise, TSiblingsAggregation, TSuccessorsAggregation,
        TNode,
        TTail, TInArrow, TInArrowSet, TPredecessorGraph,
        THead, TOutArrow, TOutArrowSet, TSuccessorGraph,
        TAggregator, TSiblingsEmbedder, TSuccessorsEmbedder,
        TSuccessorVisitingPremise, TSiblingVisitingPremise
    >
    (
        scoped ref TMutableContext mutableContext,
        TAggregator aggregator,
        TAggregationEmptyPremise aggregatorPremise,
        InitialNodeOrInArrows<TTail, TNode, TInArrow, TInArrowSet> initialNodeOrInArrows
        

    )
        where TAggregationEmptyPremise : IEmptyPremise<TAggregation>
        where TPredecessorGraph : IPredecessorGraph<TTail, TNode, TInArrow, TInArrowSet>
        where TSuccessorGraph : ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
        where TInArrow : IArrow<TTail, TNode>
        where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
        where TOutArrow : IArrow<TNode, THead>
        where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, THead>
        where TAggregator : IMutableContextedAggregator<TMutableContext, TAggregation, AggregatingInput<TTail, TNode, TInArrow, TInArrowSet, TSiblingsAggregation, TSuccessorsAggregation>>
    {

    }


}

public readonly record struct AggregatingInput<TTail, TNode, TInArrow, TInArrowSet, TSiblingsAggregation, TSuccessorsAggregation>
(
    InitialNodeOrInArrows<TTail, TNode, TInArrow, TInArrowSet> InitialNodeOrInArrows,
    TSiblingsAggregation SiblingsAggregation,
    TSuccessorsAggregation SuccessorsAggregation
)
    where TInArrow : IArrow<TTail, TNode>
    where TInArrowSet : IInArrowSet<TInArrow, TTail, TNode>
;

public interface ISuccessorVisitingPremise<TContext>
{
    bool CanVisitSuccessor(TContext context);
}

public interface ISiblingVisitingPremise<TContext>
{
    bool CanVisitSibling(TContext context);
}
