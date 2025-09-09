namespace Nemonuri.Trees;

public interface IBinderRoseTree<out TValue, out TTree> :
    IRoseTree<TValue, TTree>,
    IBinderTree<TTree>
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
    where TTree : IBinderRoseTree<TValue, TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{ 
}

public interface IBoundableRoseTree<out TValue, out TTree, TBinder> :
    IRoseTree<TValue, TTree>,
    IBoundableTree<TTree, TBinder>
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
    where TTree : IBoundableRoseTree<TValue, TTree, TBinder>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TBinder : IBinderRoseTree<TValue, TBinder>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
}



public interface IBottomUpTree<TTree> :
    IBoundableTree<TTree, TTree>,
    IBinderTree<TTree>,
    ISupportUnboundChildren<TTree>
    where TTree : IBottomUpTree<TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{ }

public interface IBottomUpRoseTree<out TValue, TTree> :
    IBottomUpTree<TTree>,
    IBoundableRoseTree<TValue, TTree, TTree>,
    IBinderRoseTree<TValue, TTree>
    where TTree : IBottomUpRoseTree<TValue, TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
    where TValue : allows ref struct
#endif
{ }