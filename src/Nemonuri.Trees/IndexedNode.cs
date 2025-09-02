#if false

namespace Nemonuri.Trees;

public readonly struct IndexedNode<TNode>
{
    public TNode? TreeNode { get; }
    public int? Index { get; }

    public IndexedNode(TNode? treeNode, int? index)
    {
        TreeNode = treeNode;
        Index = index;
    }

    public IndexedNode(TNode? treeNode)
    : this(treeNode, default)
    { }

    public void Deconstruct(out TNode? treeNode, out int? index)
    {
        treeNode = TreeNode;
        index = Index;
    }
}

#endif