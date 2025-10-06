
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Grammars.Infrastructure;

internal readonly struct WrappedNfaAggregator
<
    TNfaPremise,
    TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet, TIdeal, TExtraScanResult
> :
    IHomogeneousSuccessorAggregator
    <
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
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
    private readonly TNfaPremise _nfa;

    public WrappedNfaAggregator(TNfaPremise nfa)
    {
        Guard.IsNotNull(nfa);
        _nfa = nfa;
    }

    public TMutableSiblingContext EmptyMutableSiblingContext => _nfa.EmptyMutableSiblingContext;

    public ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult> EmptyMutableInnerSiblingContext => new(_nfa.EmptyMutableInnerSiblingContext, default);

    public TIdealContext CloneMutableDepthContext(TIdealContext depthContext) => _nfa.CloneMutableDepthContext(depthContext);

    public TPrevious EmptyPreviousAggregation => _nfa.EmptyPreviousAggregation;


    public TPrevious AggregateOuterPrevious(scoped ref MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPrevious source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        return _nfa.AggregateOuterPrevious(ref mutableContext, source, value);
    }

    public TPrevious AggregateInnerPrevious(scoped ref MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>> mutableContext, TPrevious source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        if (!mutableContext.MutableInnerSiblingContext.ScanResult.IsSuccess) { return source; }

        MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext> embeddedContext =
            new(mutableContext.MutableOuterContext, ref mutableContext.MutableInnerSiblingContext.WrappedValue);

        _nfa.SetScanResultArgument(mutableContext.MutableInnerSiblingContext.ScanResult);
        return _nfa.AggregateInnerPrevious(ref embeddedContext, source, value);
    }

    public TPost EmptyPostAggregation => _nfa.EmptyPostAggregation;

    public TPost AggregateInnerPost(scoped ref MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>> mutableContext, TPost source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        if (!mutableContext.MutableInnerSiblingContext.ScanResult.IsSuccess) { return source; }

        MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext> embeddedContext =
            new(mutableContext.MutableOuterContext, ref mutableContext.MutableInnerSiblingContext.WrappedValue);

        _nfa.SetScanResultArgument(mutableContext.MutableInnerSiblingContext.ScanResult);
        return _nfa.AggregateInnerPost(ref embeddedContext, source, value);
    }

    public TPost AggregateOuterPost(scoped ref MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPost source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        return _nfa.AggregateOuterPost(ref mutableContext, source, value);
    }

    public TInArrow EmbedToInArrow(TOutArrow outArrow) => _nfa.EmbedToInArrow(outArrow);

    public bool CanRunOuterPhase(scoped ref readonly MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot)
    {
        return _nfa.CanRunOuterPhase(in context, phaseSnapshot);
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot)
    {
        if (context.MutableInnerSiblingContext.ScanResult.Status == ScanResultStatus.ScanFail)
        {
            return false;
        }

        MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, TMutableInnerSiblingContext> embeddedContext =
            new(context.MutableOuterContext, ref context.MutableInnerSiblingContext.WrappedValue);

        if (!_nfa.CanRunInnerPhase(in embeddedContext, phaseSnapshot))
        {
            return false;
        }

        //--- Ideal test - current, scanned, memoized ---
        if (!context.MutableInnerSiblingContext.ScanResult.IsSuccess)
        {
            var scanResult = ScanAndCheckCanEnter(in context, in phaseSnapshot);
            if (scanResult.Status is not ScanResultStatus.ScanSuccess and not ScanResultStatus.ScanFail)
            {
                scanResult = scanResult with { Status = ScanResultStatus.ScanFail };
            }

            context.MutableInnerSiblingContext.ScanResult = scanResult;
            if (scanResult.IsSuccess)
            {
                context.MutableDepthContext.Memoize(phaseSnapshot.Snapshot.OutArrow.Head, scanResult.UpperBound);
            }
            return scanResult.IsSuccess;
        }
        //---|

        return true;
    }

    public TOutArrowSet GetDirectSuccessorArrows(TNode node) => _nfa.GetDirectSuccessorArrows(node);

    private ScanResult<TBound, TExtraScanResult> ScanAndCheckCanEnter
    (
        scoped ref readonly MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext, ValueWithScanResult<TMutableInnerSiblingContext, TBound, TIdeal, TExtraScanResult>> context,
        ref readonly LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot
    )
    {
        ref readonly TIdealContext idealContext = ref context.MutableDepthContext;
        TIdeal currentIdeal = idealContext.CurrentIdeal;

        ScanResult<TBound, TExtraScanResult> scanResult = _nfa.Scan(phaseSnapshot.Snapshot.OutArrow, currentIdeal);
        if (!scanResult.IsSuccess) { goto Fail; }

        if (!_nfa.IsMember(_nfa.CastToSet(currentIdeal), scanResult.UpperBound)) { goto Fail; }

        if (idealContext.TryGetMemoized(phaseSnapshot.Snapshot.OutArrow.Head, out var memoizedUpperBound))
        {
            if (!_nfa.IsLesserThan(scanResult.UpperBound, memoizedUpperBound))
            {
                goto Fail;
            }
        }

        return scanResult;

    Fail:
        return new(ScanResultStatus.ScanFail, default, default);
    }


}
