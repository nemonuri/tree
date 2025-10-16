namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatingTheory
{
    public static TPost AggregateHomogeneousSuccessors
    <
        TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
        TChildren, TDescendants, TChild, TGraph,
        TAggregator
    >
    (
        TAggregator premise,
        TNode initialNode
    )
        where TInArrow : IArrow<TNode, TNode>
        where TOutArrow : IArrow<TNode, TNode>
        where TOutArrowSet : IOutArrowSet<TNode, TNode, TOutArrow>
        where TAggregator : IHomogeneousSuccessorAggregator
        <
            TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
            TChildren, TDescendants, TChild, TGraph
        >
    {
        TGraph graph = premise.CreateInitialGraphScopeContext();
        TDescendants descendants = premise.CreateInitialDescendantsScopeContext(ref graph);

        TPost post = AggregateHomogeneousSuccessorsCore
        <
            TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
            TChildren, TDescendants, TChild, TGraph,
            TAggregator
        >
        (premise, initialNode, ref descendants, ref graph);

        premise.DisposeDescendantsScopeContext(ref descendants, ref graph);
        premise.DisposeGraphScopeContext(ref graph);

        return post;
    }


    private static TPost AggregateHomogeneousSuccessorsCore
    <
        TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
        TChildren, TDescendants, TChild, TGraph,
        TAggregator
    >
    (
        TAggregator premise,
        InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> initialOrRecursiveInfo,
        scoped ref TDescendants descendantsScopeContext,
        scoped ref TGraph graphScopeContext
    )
        where TInArrow : IArrow<TNode, TNode>
        where TOutArrow : IArrow<TNode, TNode>
        where TOutArrowSet : IOutArrowSet<TNode, TNode, TOutArrow>
        where TAggregator : IHomogeneousSuccessorAggregator
        <
            TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
            TChildren, TDescendants, TChild, TGraph
        >
    {
        TChildren childrenScopeContext = premise.CreateInitialChildrenScopeContext(ref descendantsScopeContext, ref graphScopeContext);

        TPrevious previous;
        {
            if (initialOrRecursiveInfo.TryGetPreviousAggregation(out var v))
            {
                previous = v;
            }
            else
            {
                var outerContext = premise.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
                previous = premise.CreateInitialPrevious(ref outerContext);
                premise.DeconstructOuterAggregatorContext(ref outerContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
            }
        }

        TPost post;
        {
            var outerContext = premise.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
            post = premise.CreateInitialPost(ref outerContext);
            premise.DeconstructOuterAggregatorContext(ref outerContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
        }

        if (!initialOrRecursiveInfo.TryGetNode(out TNode? node)) { return post; }

        bool isInitial = initialOrRecursiveInfo.IsInitialInfo;

        OuterPhaseLabel outerLabel;
        OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost> outerSnapshot = new(initialOrRecursiveInfo, node, previous, post);
        LabeledPhaseSnapshot<OuterPhaseLabel, OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>> outerLabeledSnapshot;

        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPrevious : OuterPhaseLabel.RecursiveOuterPrevious;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        {
            var outerContext = premise.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
            if (premise.CanRunOuterPhase(outerLabeledSnapshot, ref outerContext))
            {
                previous = premise.AggregateOuterPrevious(previous, outerLabeledSnapshot, ref outerContext);
            }
            premise.DeconstructOuterAggregatorContext(ref outerContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
        }


        TOutArrowSet outArrowSet;
        {
            var outerContext = premise.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
            outArrowSet = premise.GetDirectSuccessorArrows(node, ref outerContext);
            premise.DeconstructOuterAggregatorContext(ref outerContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
        }
        foreach (TOutArrow outArrow in outArrowSet)
        {
            InnerPhaseLabel innerLabel;
            InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet> innerSnapshotComplement = new(outArrow, outArrowSet);
            LabeledPhaseSnapshot<InnerPhaseLabel, InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>> innerLabeledSnapshot;

            TDescendants innerDescendantsScopeContext = premise.CreateNextDescendantsScopeContext(ref descendantsScopeContext, ref childrenScopeContext, ref graphScopeContext);
            TChild childScopeContext = premise.CreateInitialChildScopeContext(ref childrenScopeContext, ref innerDescendantsScopeContext, ref graphScopeContext);

            TPost innerPost;
            
            innerLabel = InnerPhaseLabel.InnerPrevious;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            {
                var innerContext = premise.CreateInnerAggregatorContext(ref childrenScopeContext, ref innerDescendantsScopeContext, ref childScopeContext, ref graphScopeContext);
                if (premise.CanRunInnerPhase(innerLabeledSnapshot, ref innerContext))
                {
                    previous = premise.AggregateInnerPrevious(previous, innerLabeledSnapshot, ref innerContext);
                    outerSnapshot = outerSnapshot with { Previous = previous };
                }
                premise.DeconstructInnerAggregatorContext(ref innerContext, out childrenScopeContext, out innerDescendantsScopeContext, out childScopeContext, out graphScopeContext);
            }


            innerLabel = InnerPhaseLabel.InnerMoment;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            {
                var innerContext = premise.CreateInnerAggregatorContext(ref childrenScopeContext, ref innerDescendantsScopeContext, ref childScopeContext, ref graphScopeContext);
                if (premise.CanRunInnerPhase(innerLabeledSnapshot, ref innerContext))
                {
                    innerPost = AggregateHomogeneousSuccessorsCore
                    <
                        TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost,
                        TChildren, TDescendants, TChild, TGraph,
                        TAggregator
                    >
                    (
                        premise, (premise.EmbedToInArrow(outArrow, ref innerContext), previous),
                        ref innerDescendantsScopeContext,
                        ref graphScopeContext
                    );
                }
                else
                {
                    innerPost = premise.CreateFallbackPost(ref innerContext);
                }
                premise.DeconstructInnerAggregatorContext(ref innerContext, out childrenScopeContext, out innerDescendantsScopeContext, out childScopeContext, out graphScopeContext);
            }

            innerLabel = InnerPhaseLabel.InnerPost;
            innerLabeledSnapshot = new(innerLabel, new(outerSnapshot, innerSnapshotComplement));
            {
                var innerContext = premise.CreateInnerAggregatorContext(ref childrenScopeContext, ref innerDescendantsScopeContext, ref childScopeContext, ref graphScopeContext);
                if (premise.CanRunInnerPhase(innerLabeledSnapshot, ref innerContext))
                {
                    post = premise.AggregateInnerPost(innerPost, innerLabeledSnapshot, ref innerContext);
                    outerSnapshot = outerSnapshot with { Post = post };
                }
                premise.DeconstructInnerAggregatorContext(ref innerContext, out childrenScopeContext, out innerDescendantsScopeContext, out childScopeContext, out graphScopeContext);
            }


            premise.DisposeChildScopeContext(ref childScopeContext, ref graphScopeContext);
            premise.DisposeDescendantsScopeContext(ref innerDescendantsScopeContext, ref graphScopeContext);
        }

        outerLabel = isInitial ? OuterPhaseLabel.InitialOuterPost : OuterPhaseLabel.RecursiveOuterPost;
        outerLabeledSnapshot = new(outerLabel, outerSnapshot);
        {
            var outerContext = premise.CreateOuterAggregatorContext(ref childrenScopeContext, ref descendantsScopeContext, ref graphScopeContext);
            if (premise.CanRunOuterPhase(outerLabeledSnapshot, ref outerContext))
            {
                post = premise.AggregateOuterPost(post, outerLabeledSnapshot, ref outerContext);
            }            
            premise.DeconstructOuterAggregatorContext(ref outerContext, out childrenScopeContext, out descendantsScopeContext, out graphScopeContext);
        }

        premise.DisposeChildrenScopeContext(ref childrenScopeContext, ref graphScopeContext);
        return post;
    }
}
