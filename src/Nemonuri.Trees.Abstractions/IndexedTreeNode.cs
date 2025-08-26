namespace Nemonuri.Trees;

public readonly struct IndexedTreeNode<TTreeNode>
{
    public TTreeNode? TreeNode { get; }
    public int? Index { get; }

    public IndexedTreeNode(TTreeNode? treeNode, int? index)
    {
        TreeNode = treeNode;
        Index = index;
    }

    public IndexedTreeNode(TTreeNode? treeNode)
    : this(treeNode, default)
    { }

    public void Deconstruct(out TTreeNode? treeNode, out int? index)
    {
        treeNode = TreeNode;
        index = Index;
    }
}