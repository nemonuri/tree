namespace Nemonuri.Trees.Forests;

public interface IAlterableTree
<
    out TTree,
    out TAlternateTreeKey,
    out TAlternateTreeMap
> :
    ITree<TTree>
    where TAlternateTreeMap : IReadOnlyDictionary<TAlternateTreeKey, TTree>
    where TTree : IAlterableTree<TTree, TAlternateTreeKey, TAlternateTreeMap>
{
    bool HasAlternateTreeMap { get; }
    TAlternateTreeMap GetAlternateTreeMap();
}

public interface ISupportLeafValue<out TLeafValue>
{
    bool IsLeaf { get; }
    TLeafValue GetLeafValue();
}

public interface ISupportBranchValue<out TBranchValue>
{
    bool IsBranch { get; }
    TBranchValue GetBranchValue();
}

public interface ITreeStructuredMap<TLeafValue, out TTree> :
    ITree<TTree>,
    IReadOnlyDictionary<IIndexPath, TLeafValue>,
    ISupportLeafValue<TLeafValue>
    where TTree : ITreeStructuredMap<TLeafValue, TTree>
{
}

public interface ISupportUnitValue<out TUnitValue>
{
    bool IsUnit { get; }
    TUnitValue GetUnitValue();
}

public interface ITreeStructuredForest<out TUnitValue, out TSum, TForest> :
    IAlterableTree<TForest, IIndexPath, TSum>,
    ISupportUnitValue<TUnitValue>
    where TSum : ITreeStructuredMap<TForest, TSum>
    where TForest : ITreeStructuredForest<TUnitValue, TSum, TForest>
{
}
