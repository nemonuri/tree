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
        InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> initialOrRecursiveInfo
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
        if (!initialOrRecursiveInfo.TryGetNode(out TNode? node)) { return premise.EmptyPostAggregation; }

        bool isInitial = initialOrRecursiveInfo.IsInitialInfo;

        TPrevious previousAggregation;
        { 
            previousAggregation = initialOrRecursiveInfo.TryGetPreviousAggregation(out var v) ? v : premise.EmptyPreviousAggregation;
        }
        TPost postAggregation = premise.EmptyPostAggregation;

        OuterPhaseLabel outerLabel;
        OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost> outerSnapshot = new(initialOrRecursiveInfo, node, previousAggregation, postAggregation);
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> outerLabeledSnapshot;


        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPrevious : OuterPhaseLabel.RecursiveOuterPrevious;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        if (premise.CanRunOuterPhase(outerLabeledSnapshot))
        {
            previousAggregation = premise.AggregateOuterPrevious(ref mutableContext, previousAggregation, outerLabeledSnapshot);
        }

        TOutArrowSet outArrowSet = premise.GetDirectSuccessorArrows(node);
        foreach (TOutArrow outArrow in outArrowSet)
        {
            InnerPhaseLabel innerLabel;
            InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet> innerSnapshotComplement = new(outArrow, outArrowSet);
            LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> innerLabeledSnapshot;
            TPost succesorsAggregation;


            innerLabel = InnerPhaseLabel.InnerPrevious;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            if (premise.CanRunInnerPhase(innerLabeledSnapshot))
            {
                previousAggregation = premise.AggregateInnerPrevious(ref mutableContext, previousAggregation, innerLabeledSnapshot);
                outerSnapshot = outerSnapshot with { PreviousAggregation = previousAggregation };
            }


            innerLabel = InnerPhaseLabel.InnerMoment;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            if (premise.CanRunInnerPhase(innerLabeledSnapshot))
            {
                succesorsAggregation = AggregateHomogeneousSuccessors
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
                succesorsAggregation = premise.EmptyPostAggregation;
            }


            innerLabel = InnerPhaseLabel.InnerPost;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            if (premise.CanRunInnerPhase(innerLabeledSnapshot))
            {
                postAggregation = premise.AggregateInnerPost(ref mutableContext, succesorsAggregation, innerLabeledSnapshot);
                outerSnapshot = outerSnapshot with { PostAggregation = postAggregation };
            }
        }
        
        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPost : OuterPhaseLabel.RecursiveOuterPost;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        if (premise.CanRunOuterPhase(outerLabeledSnapshot))
        {
            postAggregation = premise.AggregateOuterPost(ref mutableContext, postAggregation, outerLabeledSnapshot);
        }

        return postAggregation;
    }
}
