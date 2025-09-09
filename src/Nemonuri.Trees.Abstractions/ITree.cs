namespace Nemonuri.Trees;

public interface ITree<out TTree> : ISupportChildren<TTree>
    where TTree : ITree<TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
}

public interface IRoseTree<out TValue, out TTree> : ITree<TTree>, ISupportValue<TValue>
    where TTree : IRoseTree<TValue, TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
    where TValue : allows ref struct
#endif
{
}

public interface IBoundableTree<out TTree, TBinder> : ITree<TTree>, ISupportParent<TBinder>
    where TTree : IBoundableTree<TTree, TBinder>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TBinder : ITree<TBinder>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TTree BindParent(TBinder parent);
}

public interface IBinderTree<out TTree> : ITree<TTree>, ISupportParent<TTree>
    where TTree : IBinderTree<TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
}