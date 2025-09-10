namespace Nemonuri.Trees.Forests;

public interface IForest<out TForest, out TTree, out TTreeChildren> :
    ISupportChildren<TTreeChildren>
    where TTree : ITree<TTree>
    where TForest : IForest<TForest, TTree, TTreeChildren>
    where TTreeChildren : IEnumerable<TTree>
{
}

public interface IRoseForest<out TValue, out TForest, out TTree, out TTreeChildren> :
    ISupportChildren<TTreeChildren>,
    ISupportValue<TValue>
    where TTree : ITree<TTree>
    where TForest : IForest<TForest, TTree, TTreeChildren>
    where TTreeChildren : IEnumerable<TTree>
{
}