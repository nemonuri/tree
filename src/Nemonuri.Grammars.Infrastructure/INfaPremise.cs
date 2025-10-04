namespace Nemonuri.Grammars.Infrastructure;

public interface INfaPremise
<
    TMutableGraphContext, TMutableSiblingContext, TIdealContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet, TIdeal
> :
    INafArrowPremise
    <
        TNode, TOutArrow, TBound, TIdeal
    >,
    IIdealPremise
    <
        TBound, TLogicalSet, TIdeal
    >,
    IHomogeneousSuccessorAggregator
    <
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    where TIdeal : IIdeal<TBound>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
    where TIdealContext : IIdealContext<TNode, TBound, TIdeal>
{

}
