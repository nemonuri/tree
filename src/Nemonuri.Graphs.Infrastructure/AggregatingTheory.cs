namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatingTheory
{

    public static TPost AggregateHomogeneousSuccessors
    <
        TAggregator,
        TMutableContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    (
        TAggregator premise,
        scoped ref TMutableContext mutableContext,
        InitialOrPreviousInfo<TNode, TNode, TInArrow, TPrevious> initialOrPreviousInfo
    )
        where TAggregator : IHomogeneousSuccessorAggregator
        <
            TMutableContext, TPrevious, TPost,
            TNode, TInArrow, TOutArrow, TOutArrowSet
        >
        where TInArrow : IArrow<TNode, TNode>
        where TOutArrow : IArrow<TNode, TNode>
        where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
    {
        if (!initialOrPreviousInfo.TryGetNode(out TNode? node)) { return premise.EmptyPostAggregation; }

        bool isInitial = initialOrPreviousInfo.IsInitialInfo;

        TPrevious previousAggregation = initialOrPreviousInfo.TryGetPreviousAggregation(out var v1) ? v1 : premise.EmptyPreviousAggregation;
        TPost postAggregation = premise.EmptyPostAggregation;

        if (isInitial)
        {
            previousAggregation = premise.AggregatePreviousToInitialNode(ref mutableContext, previousAggregation, node);
        }

        foreach (TOutArrow outArrow in premise.GetDirectSuccessorArrows(node))
        {
            PhaseSnapshot<TNode, TInArrow, TOutArrow, TPrevious, TPost> snapshot =
                new(initialOrPreviousInfo, outArrow, previousAggregation, postAggregation);

            if (premise.CanRunPhase(snapshot, AggregatingPhase.AggregatePrevious))
            {
                var tempPreviousAggregation = premise.AggregatePrevious(ref mutableContext, previousAggregation, snapshot);
                snapshot = snapshot with { PreviousAggregation = tempPreviousAggregation };

                if (premise.CanRunPhase(snapshot, AggregatingPhase.AssignPrevious))
                {
                    previousAggregation = snapshot.PreviousAggregation;
                }
                else
                { 
                    snapshot = snapshot with { PreviousAggregation = previousAggregation };
                }
            }

            TPost childrenAggregation;
            if (premise.CanRunPhase(snapshot, AggregatingPhase.AggregateAndAssignChildren))
            {
                childrenAggregation = AggregateHomogeneousSuccessors
                <
                    TAggregator,
                    TMutableContext, TPrevious, TPost,
                    TNode, TInArrow, TOutArrow, TOutArrowSet
                >
                (
                    premise, ref mutableContext, (premise.EmbedToInArrow(outArrow), previousAggregation)
                );
            }
            else
            {
                childrenAggregation = premise.EmptyPostAggregation;
            }

            if (premise.CanRunPhase(snapshot, AggregatingPhase.AggregatePost))
            {
                var tempPostAggregation = premise.AggregatePost(ref mutableContext, childrenAggregation, snapshot);
                snapshot = snapshot with { PostAggregation = tempPostAggregation };

                if (premise.CanRunPhase(snapshot, AggregatingPhase.AssignPost))
                {
                    postAggregation = snapshot.PostAggregation;
                }
            }
        }
        
        if (isInitial)
        { 
            postAggregation = premise.AggregatePostToInitialNode(ref mutableContext, postAggregation, node);
        }

        return postAggregation;
    }
}
