namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>
(
    InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> InitialOrRecursiveInfo,
    TPrevious PreviousAggregation,
    TPost PostAggregation
)
    where TInArrow : IArrow<TNode, TNode>
;

public readonly record struct InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet>
(
    TOutArrow OutArrow,
    TOutArrowSet OutArrowSet
)
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
;

public readonly record struct InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>
(
    OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost> OuterPhaseSnapshot,
    InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet> InnerPhaseSnapshotComplement
)
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    public InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> InitialOrRecursiveInfo => OuterPhaseSnapshot.InitialOrRecursiveInfo;
    public TPrevious PreviousAggregation => OuterPhaseSnapshot.PreviousAggregation;
    public TPost PostAggregation => OuterPhaseSnapshot.PostAggregation;
    public TOutArrow OutArrow => InnerPhaseSnapshotComplement.OutArrow;
    public TOutArrowSet OutArrowSet => InnerPhaseSnapshotComplement.OutArrowSet;
}
;

public readonly record struct LabeledPhaseSnapshot<TLabel, TSnapshot>
(
    TLabel PhaseLabel,
    TSnapshot Snapshot
)
    where TLabel : struct
;