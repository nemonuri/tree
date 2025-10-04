
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Grammars.Infrastructure;

internal class WrappedNfaAggregator
<
    TNfaPremise,
    TMutableGraphContext, TMutableSiblingContext, TIdealContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet, TIdeal
> :
    IHomogeneousSuccessorAggregator
    <
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    where TNfaPremise : INfaPremise
    <
        TMutableGraphContext, TMutableSiblingContext, TIdealContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet,
        TBound, TLogicalSet, TIdeal
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

    public TIdealContext CloneMutableDepthContext(TIdealContext depthContext) => _nfa.CloneMutableDepthContext(depthContext);

    public TPrevious EmptyPreviousAggregation => _nfa.EmptyPreviousAggregation;


    public TPrevious AggregateOuterPrevious(scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPrevious source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TPrevious AggregateInnerPrevious(scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPrevious source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TPost EmptyPostAggregation => throw new NotImplementedException();

    public TPost AggregateInnerPost(scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPost source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TPost AggregateOuterPost(scoped ref MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> mutableContext, TPost source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TInArrow EmbedToInArrow(TOutArrow outArrow)
    {
        throw new NotImplementedException();
    }

    public bool CanRunOuterPhase(scoped ref readonly MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> context, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public bool CanRunInnerPhase(scoped ref readonly MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TIdealContext> context, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot)
    {
        if (!_nfa.CanRunInnerPhase(in context, phaseSnapshot))
        {
            return false;
        }

        //--- Ideal test - current, scanned, memoized ---
        ref readonly TIdealContext idealContext = ref context.MutableDepthContext;
        TIdeal currentIdeal = idealContext.CurrentIdeal;

        if (!_nfa.TryScan(phaseSnapshot.Snapshot.OutArrow, currentIdeal, out var scannedUpperBound)) { return false; }

        if (!_nfa.IsMember(_nfa.CastToSet(currentIdeal), scannedUpperBound)) { return false; }

        if (idealContext.TryGetMemoized(phaseSnapshot.Snapshot.OutArrow.Head, out var memoizedUpperBound))
        {
            if (!_nfa.IsLesserThan(scannedUpperBound, memoizedUpperBound))
            {
                return false;
            }
        }
        //---|

        return true;
    }

    public TOutArrowSet GetDirectSuccessorArrows(TNode node)
    {
        throw new NotImplementedException();
    }
}
