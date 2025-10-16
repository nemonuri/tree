namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost>
(
    InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> InitialOrRecursiveInfo,
    TNode OuterNode,
    TPrevious Previous,
    TPost Post
)
    where TInArrow : IArrow<TNode, TNode>
;

public readonly record struct InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet>
(
    TOutArrow OutArrow,
    TOutArrowSet OutArrowSet
)
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TNode, TNode, TOutArrow>
;

public readonly record struct InnerPhaseSnapshot<TNode, TInArrow, TOutArrow, TOutArrowSet, TPrevious, TPost>
(
    OuterPhaseSnapshot<TNode, TInArrow, TPrevious, TPost> OuterPhaseSnapshot,
    InnerPhaseSnapshotComplement<TNode, TOutArrow, TOutArrowSet> InnerPhaseSnapshotComplement
)
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TNode, TNode, TOutArrow>
{
    public InitialOrRecursiveInfo<TNode, TNode, TInArrow, TPrevious> InitialOrRecursiveInfo => OuterPhaseSnapshot.InitialOrRecursiveInfo;
    public TNode OuterNode => OuterPhaseSnapshot.OuterNode;
    public TPrevious Previous => OuterPhaseSnapshot.Previous;
    public TPost Post => OuterPhaseSnapshot.Post;
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