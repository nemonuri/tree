namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatingTheory
{

    public static TPost AggregateHomogeneousSuccessors
    <
        TAggregator,
        TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
        TNode, TInArrow, TOutArrow, TOutArrowSet
    >
    (
        TAggregator premise,
        scoped ref TMutableGraphContext mutableGraphContext,
        TMutableDepthContext depthContext,
        InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> initialOrRecursiveInfo
    )
        where TAggregator : IHomogeneousSuccessorAggregator
        <
            TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
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
        TMutableSiblingContext mutableSiblingContext = premise.EmptyMutableSiblingContext;
        TMutableDepthContext mutableDepthContext1 = premise.CloneMutableDepthContext(depthContext);
        scoped MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableContext = default;

        OuterPhaseLabel outerLabel;
        OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost> outerSnapshot = new(initialOrRecursiveInfo, node, previousAggregation, postAggregation);
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> outerLabeledSnapshot;

        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPrevious : OuterPhaseLabel.RecursiveOuterPrevious;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        mutableContext = new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext1);
        if (premise.CanRunOuterPhase(in mutableContext, outerLabeledSnapshot))
        {
            previousAggregation = premise.AggregateOuterPrevious(ref mutableContext, previousAggregation, outerLabeledSnapshot);
            mutableContext.Deconstruct(out mutableGraphContext, out mutableSiblingContext, out mutableDepthContext1);
        }

        TOutArrowSet outArrowSet = premise.GetDirectSuccessorArrows(node);
        foreach (TOutArrow outArrow in outArrowSet)
        {
            InnerPhaseLabel innerLabel;
            InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet> innerSnapshotComplement = new(outArrow, outArrowSet);
            LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> innerLabeledSnapshot;
            TPost succesorsAggregation;
            scoped MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext> mutableInnerContext = default;
            TMutableDepthContext mutableDepthContext2 = premise.CloneMutableDepthContext(depthContext);
            TMutableInnerSiblingContext mutableInnerSiblingContext = premise.EmptyMutableInnerSiblingContext;


            innerLabel = InnerPhaseLabel.InnerPrevious;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            mutableInnerContext = new(new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext2), ref mutableInnerSiblingContext);
            if (premise.CanRunInnerPhase(in mutableInnerContext, innerLabeledSnapshot))
            {
                previousAggregation = premise.AggregateInnerPrevious(ref mutableInnerContext, previousAggregation, innerLabeledSnapshot);
                mutableInnerContext.Deconstruct(out mutableGraphContext, out mutableSiblingContext, out mutableDepthContext2, out mutableInnerSiblingContext);
                outerSnapshot = outerSnapshot with { PreviousAggregation = previousAggregation };
            }


            innerLabel = InnerPhaseLabel.InnerMoment;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            mutableInnerContext = new(new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext2), ref mutableInnerSiblingContext);
            if (premise.CanRunInnerPhase(in mutableInnerContext, innerLabeledSnapshot))
            {
                succesorsAggregation = AggregateHomogeneousSuccessors
                <
                    TAggregator,
                    TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext, TPrevious, TPost,
                    TNode, TInArrow, TOutArrow, TOutArrowSet
                >
                (
                    premise, ref mutableGraphContext, mutableDepthContext2, (premise.EmbedToInArrow(outArrow), previousAggregation)
                );
            }
            else
            {
                succesorsAggregation = premise.EmptyPostAggregation;
            }


            innerLabel = InnerPhaseLabel.InnerPost;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            mutableInnerContext = new(new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext2), ref mutableInnerSiblingContext);
            if (premise.CanRunInnerPhase(in mutableInnerContext, innerLabeledSnapshot))
            {
                postAggregation = premise.AggregateInnerPost(ref mutableInnerContext, succesorsAggregation, innerLabeledSnapshot);
                mutableInnerContext.Deconstruct(out mutableGraphContext, out mutableSiblingContext, out mutableDepthContext2, out mutableInnerSiblingContext);
                outerSnapshot = outerSnapshot with { PostAggregation = postAggregation };
            }
        }
        
        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPost : OuterPhaseLabel.RecursiveOuterPost;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        mutableContext = new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext1);
        if (premise.CanRunOuterPhase(in mutableContext, outerLabeledSnapshot))
        {
            mutableContext = new(ref mutableGraphContext, ref mutableSiblingContext, ref mutableDepthContext1);
            postAggregation = premise.AggregateOuterPost(ref mutableContext, postAggregation, outerLabeledSnapshot);
            mutableContext.Deconstruct(out mutableGraphContext, out mutableSiblingContext, out mutableDepthContext1);
        }

        return postAggregation;
    }
}
