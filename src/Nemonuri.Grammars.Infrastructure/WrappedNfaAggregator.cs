#if false

using CommunityToolkit.Diagnostics;

namespace Nemonuri.Grammars.Infrastructure;

internal class WrappedNfaAggregator
<
    TNfaAggregator,
    TIdeal, TMutableSiblingContext, TPrevious, TPost,
    TNode, TInArrow, TOutArrow, TOutArrowSet,
    TBound, TLogicalSet
> :
    IHomogeneousSuccessorAggregator
    <
        TIdeal, TMutableSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    where TNfaAggregator : INfaPremise
    <
        TIdeal, TMutableSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet,
        TBound, TLogicalSet
    >
    where TIdeal : IIdeal<TBound>
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    private readonly TNfaAggregator _nfa;
    private bool _graphEntered;

    public WrappedNfaAggregator(TNfaAggregator nfaAggregator)
    {
        Guard.IsNotNull(nfaAggregator);
        _nfa = nfaAggregator;
        _graphEntered = false;
    }

    public TMutableSiblingContext EmptyMutableSiblingContext => _nfa.EmptyMutableSiblingContext;

    public TPrevious EmptyPreviousAggregation => _nfa.EmptyPreviousAggregation;

    public TPrevious AggregateOuterPrevious(scoped ref MutableContextRecord<TIdeal, TMutableSiblingContext> mutableContext, TPrevious source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        //--- 
        if (value.PhaseLabel.IsInitial())
        {
            _nfa.ClearMemoized();
            _graphEntered = true;
        }
        if (_nfa.CanEnter(value.Snapshot.OuterNode, mutableContext.MutableGraphContext))
        {
            _nfa.AggregateOuterPrevious(ref mutableContext, source, value);
            
        }
        return _nfa.AggregateOuterPrevious(ref mutableContext, source, value);
    }

    public TPrevious AggregateInnerPrevious(scoped ref MutableContextRecord<TIdeal, TMutableSiblingContext> mutableContext, TPrevious source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        return _nfa.AggregateInnerPrevious(ref mutableContext, source, value);
    }

    public TPost EmptyPostAggregation => throw new NotImplementedException();

    public TPost AggregateInnerPost(scoped ref MutableContextRecord<TIdeal, TMutableSiblingContext> mutableContext, TPost source, LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TPost AggregateOuterPost(scoped ref MutableContextRecord<TIdeal, TMutableSiblingContext> mutableContext, TPost source, LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> value)
    {
        throw new NotImplementedException();
    }

    public TInArrow EmbedToInArrow(TOutArrow outArrow)
    {
        throw new NotImplementedException();
    }

    public bool CanRunOuterPhase(LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public bool CanRunInnerPhase(LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> phaseSnapshot)
    {
        throw new NotImplementedException();
    }

    public TOutArrowSet GetDirectSuccessorArrows(TNode node)
    {
        throw new NotImplementedException();
    }
}

#endif