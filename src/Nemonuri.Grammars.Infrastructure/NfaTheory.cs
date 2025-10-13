using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure;

public static class NfaTheory
{
    public static TPost AggregateHomogeneousSuccessors
    <
        TNfaPremise,
        TMutableGraphContext, TMutableSiblingContext, TExtraMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet,
        TBound, TLogicalSet, TIdeal, TIdealContext, TExtraScanResult
    >
    (
        TNfaPremise nfaPremise,
        scoped ref TMutableGraphContext mutableGraphContext,
        TExtraMutableDepthContext extraMutableDepthContext,
        TIdealContext idealContext,
        TNode initialNode
    )
        where TNfaPremise : INfaPremise
        <
            TMutableGraphContext, TMutableSiblingContext, TExtraMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet,
            TBound, TLogicalSet, TIdeal, TIdealContext, TExtraScanResult
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
            TMutableGraphContext, TMutableSiblingContext, TExtraMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet,
            TBound, TLogicalSet, TIdeal, TIdealContext, TExtraScanResult
        > w = new(nfaPremise);

        TPost post = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            WrappedNfaAggregator
            <
                TNfaPremise,
                TMutableGraphContext, TMutableSiblingContext, TExtraMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
                TNode, TInArrow, TOutArrow, TOutArrowSet,
                TBound, TLogicalSet, TIdeal, TIdealContext, TExtraScanResult
            >,
            TMutableGraphContext, TMutableSiblingContext, ValueWithIdealContext<TExtraMutableDepthContext, TIdealContext>, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet
        >
        (
            w, ref mutableGraphContext, new(extraMutableDepthContext,idealContext), new InitialInfo<TNode>(initialNode)
        );

        return post;
    }
}