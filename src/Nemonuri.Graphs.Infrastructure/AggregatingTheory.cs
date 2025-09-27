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

            if (premise.CanRunPhase(snapshot, AggregatingPhase.Pre))
            {
                previousAggregation = premise.AggregatePrevious(ref mutableContext, previousAggregation, snapshot);
                snapshot = snapshot with { PreviousAggregation = previousAggregation };
            }

            TPost childrenAggregation;
            if (premise.CanRunPhase(snapshot, AggregatingPhase.Children))
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

            if (premise.CanRunPhase(snapshot, AggregatingPhase.Post))
            {
                var v2 = premise.AggregatePost(ref mutableContext, postAggregation, snapshot);
                snapshot = snapshot with { PostAggregation = v2 };
            }

            if (premise.CanRunPhase(snapshot, AggregatingPhase.Assign))
            {
                postAggregation = snapshot.PostAggregation;
            }
        }
        
        if (isInitial)
        { 
            postAggregation = premise.AggregatePostToInitialNode(ref mutableContext, postAggregation, node);
        }

        return postAggregation;
    }
}
