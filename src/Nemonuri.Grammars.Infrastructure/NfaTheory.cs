using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure;

public static class NfaTheory
{
    public static TPost AggregateHomogeneousSuccessors
    <
        TNfaPremise,
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet,
        TBound, TLogicalSet, TIdeal, TExtraScanResult
    >
    (
        TNfaPremise nfaPremise,
        scoped ref TMutableGraphContext mutableGraphContext,
        TIdealContext depthContext,
        TNode initialNode
    )
        where TNfaPremise : INfaPremise
        <
            TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet,
            TBound, TLogicalSet, TIdeal, TExtraScanResult
        >
        where TIdeal : IIdeal<TBound>
        where TInArrow : IArrow<TNode, TNode>
        where TOutArrow : IArrow<TNode, TNode>
        where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
        where TIdealContext : IIdealContext<TNode, TBound, TIdeal>
    {
        WrappedNfaAggregator
        <
            TNfaPremise,
            TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet,
            TBound, TLogicalSet, TIdeal, TExtraScanResult
        > w = new(nfaPremise);

        TPost post = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            WrappedNfaAggregator
            <
                TNfaPremise,
                TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
                TNode, TInArrow, TOutArrow, TOutArrowSet,
                TBound, TLogicalSet, TIdeal, TExtraScanResult
            >,
            TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet
        >
        (
            w, ref mutableGraphContext, depthContext, new InitialInfo<TNode>(initialNode)
        );

        return post;
    }
}