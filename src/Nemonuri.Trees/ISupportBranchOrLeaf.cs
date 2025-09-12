namespace Nemonuri.Trees;

public interface ISupportBranchOrLeaf<out TBranch, out TLeaf>
{
    bool IsBranch { get; }
    TBranch AsBranch { get; }
    bool IsLeaf { get; }
    TLeaf AsLeaf { get; }
}
