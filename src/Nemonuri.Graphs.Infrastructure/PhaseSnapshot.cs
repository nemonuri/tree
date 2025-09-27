namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct PhaseSnapshot<TNode, TInArrow, TOutArrow, TPrevious, TPost>
(
    InitialOrPreviousInfo<TNode, TNode, TInArrow, TPrevious> InitialNodeOrInArrow,
    TOutArrow OutArrow,
    TPrevious PreviousAggregation,
    TPost PostAggregation
)
    where TInArrow : IArrow<TNode, TNode>
    where TOutArrow : IArrow<TNode, TNode>
;
