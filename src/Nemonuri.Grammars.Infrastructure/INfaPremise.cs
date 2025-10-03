namespace Nemonuri.Grammars.Infrastructure;

public interface INfaPremise
<
    TMutableGraphContext, TMutableSiblingContext, TIdeal, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet
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
        TMutableGraphContext, TMutableSiblingContext, TIdeal, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >,
    IMemoizer<TNode, TIdeal>
    where TIdeal : IIdeal<TBound>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{ 

}
