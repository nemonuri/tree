namespace Nemonuri.Trees;

public interface IGeneralTree : ITree<IGeneralTree>
{ }

public interface IGeneralRoseTree<out TValue> :
    IRoseTree<TValue, IGeneralRoseTree<TValue>>,
    IGeneralTree
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
{ }


public interface IGeneralBinderTree : 
    IBinderTree<IGeneralBinderTree>,
    IGeneralTree
{ }

public interface IGeneralBoundableTree :
    IBoundableTree<IGeneralBoundableTree, IGeneralBinderTree>,
    IGeneralTree
{ }


public interface IGeneralBinderRoseTree<TValue> :
    IBinderRoseTree<TValue, IGeneralBinderRoseTree<TValue>>,
    IGeneralRoseTree<TValue>,
    IGeneralBinderTree
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
{ }

public interface IGeneralBoundableTree<TValue> :
    IBoundableRoseTree<TValue, IGeneralBoundableTree<TValue>, IGeneralBinderRoseTree<TValue>>,
    IGeneralRoseTree<TValue>
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
{ }


public interface IGeneralBottomUpTree :
    IBottomUpTree<IGeneralBottomUpTree>,
    IGeneralBinderTree
{ }

public interface IGeneralBottomUpRoseTree<TValue> :
    IBottomUpRoseTree<TValue, IGeneralBottomUpRoseTree<TValue>>,
    IGeneralBinderRoseTree<TValue>
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
{ }