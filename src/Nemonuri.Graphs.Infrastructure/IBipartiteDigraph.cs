namespace Nemonuri.Graphs.Infrastructure;

#if false
public interface IBipartiteSuccessorGraph
<
    TGraph1, TNode1, TArrow1, TArrowSet1,
    TGraph2, TNode2, TArrow2, TArrowSet2
> :
    ISupportFixedPoint<TGraph1>,
    ISuccessorGraph<TNode1, TNode2, TArrow1, TArrowSet1>
    where TGraph1 : IBipartiteSuccessorGraph
    <
        TGraph1, TNode1, TArrow1, TArrowSet1,
        TGraph2, TNode2, TArrow2, TArrowSet2
    >
    where TArrow1 : IArrow<TNode1, TNode2>
    where TArrowSet1 : IEnumerable<TArrow1>
    where TGraph2 : IBipartiteSuccessorGraph
    <
        TGraph2, TNode2, TArrow2, TArrowSet2,
        TGraph1, TNode1, TArrow1, TArrowSet1
    >
    where TArrow2 : IArrow<TNode2, TNode1>
    where TArrowSet2 : IEnumerable<TArrow2>
{
    TGraph2 GetNext();
}
#endif