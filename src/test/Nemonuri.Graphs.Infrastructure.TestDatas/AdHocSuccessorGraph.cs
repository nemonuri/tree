namespace Nemonuri.Graphs.Infrastructure.TestDatas;

public readonly struct AdHocSuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet> :
    ISuccessorGraph<TNode, THead, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, THead>
    where TOutArrowSet : IOutArrowSet<TNode, THead, TOutArrow>
{
    private readonly Func<TNode, TOutArrowSet> _getDirectSuccessorArrowsImpl;

    public AdHocSuccessorGraph(Func<TNode, TOutArrowSet> getDirectSuccessorArrowsImpl)
    {
        _getDirectSuccessorArrowsImpl = getDirectSuccessorArrowsImpl;
    }

    public TOutArrowSet GetDirectSuccessorArrows(TNode node)
    {
        return _getDirectSuccessorArrowsImpl(node);
    }
}