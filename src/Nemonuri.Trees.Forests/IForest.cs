namespace Nemonuri.Trees.Forests;

public interface IForest<TForest, TForestKey, TForestKeyCollection> :
    IRoseTree<TForestKeyCollection, TForest>
    where TForest : IForest<TForest, TForestKey, TForestKeyCollection>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
#if NET9_0_OR_GREATER
    where TForestKey : allows ref struct
#endif
    where TForestKeyCollection : IEnumerable<TForestKey>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
}

