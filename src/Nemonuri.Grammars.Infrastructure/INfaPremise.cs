namespace Nemonuri.Grammars.Infrastructure;

public interface INfaPremise
<
    TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet, TIdeal, TExtraScanResult
> :
    INafArrowPremise
    <
        TNode, TOutArrow, TBound, TIdeal, TExtraScanResult
    >,
    IIdealPremise
    <
        TBound, TLogicalSet, TIdeal
    >,
    IHomogeneousSuccessorAggregator
    <
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    where TIdeal : IIdeal<TBound>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
    where TIdealContext : IIdealContext<TNode, TBound, TIdeal>
{
    void SetScanResultArgument(ScanResult<TBound, TIdeal, TExtraScanResult> scanResult);

    ScanResult<TBound, TIdeal, TExtraScanResult> ScanResultArgument { get; }
}
