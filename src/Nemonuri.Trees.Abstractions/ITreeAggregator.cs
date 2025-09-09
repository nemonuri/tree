namespace Nemonuri.Trees;

public interface ITreeAggregator<TTree, TAggregation, TAncestor, TAncestorsAggregation> :
    IAggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation>,
    IAncestorConverter<TTree, TAncestor>
    where TTree : ITree<TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
}