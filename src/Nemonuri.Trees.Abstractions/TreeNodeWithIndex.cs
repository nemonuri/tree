namespace Nemonuri.Trees;

public readonly struct TreeNodeWithIndex<TTreeNode>
{
    public TTreeNode? TreeNode { get; }
    public int? Index { get; }

    public TreeNodeWithIndex(TTreeNode? treeNode, int? index)
    {
        TreeNode = treeNode;
        Index = index;
    }

    public TreeNodeWithIndex(TTreeNode? treeNode)
    : this(treeNode, default)
    { }

    public void Deconstruct(out TTreeNode? treeNode, out int? index)
    {
        treeNode = TreeNode;
        index = Index;
    }
}