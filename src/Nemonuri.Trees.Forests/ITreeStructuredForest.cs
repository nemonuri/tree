namespace Nemonuri.Trees.Forests;

public interface IAlterableTree
<
    out TTree,
    out TAlternateTreeMap, out TAlternateTreeKey
> :
    ITree<TTree>
    where TAlternateTreeMap : IReadOnlyDictionary<TAlternateTreeKey, TTree>
    where TTree : IAlterableTree<TTree, TAlternateTreeMap, TAlternateTreeKey>
{
    bool HasAlternateTreeMap { get; }
    TAlternateTreeMap GetAlternateTreeMap();
}

public interface ITreeStructuredSum<T, out TTree> :
    ITree<TTree>,
    IReadOnlyDictionary<IIndexPath, T>
    where TTree : ITreeStructuredSum<T, TTree>
{ 

}

public interface ITreeStructuredForest<TTree, out TSumTree> :
    IAlterableTree<TTree, TSumTree, IIndexPath>
    where TSumTree : ITreeStructuredSum<TTree, TSumTree>
    where TTree : ITreeStructuredForest<TTree, TSumTree>
{
}
