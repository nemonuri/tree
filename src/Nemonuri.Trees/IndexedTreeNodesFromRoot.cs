
using System.Collections;

namespace Nemonuri.Trees;

public class IndexedTreeNodesFromRoot<TTreeNode> : 
    IReadOnlyList<IndexedTreeNode<TTreeNode>>,
    IIndexesFromRoot<TTreeNode>
{
    public static IndexedTreeNodesFromRoot<TTreeNode> Empty { get; } = new();

    private readonly TTreeNode? _root;

    [MemberNotNullWhen(false, [nameof(_root)])]
    public bool IsEmpty { get; }

    private readonly ImmutableList<TTreeNode> _treeNodes;
    private readonly ImmutableList<int> _indexes;

    private IndexedTreeNodesFromRoot(bool isEmpty, TTreeNode? root, ImmutableList<TTreeNode> treeNodes, ImmutableList<int> indexes)
    {
        Debug.Assert(treeNodes is not null);
        Debug.Assert(indexes is not null);

        IsEmpty = isEmpty;
        _root = root;
        _treeNodes = treeNodes;
        _indexes = indexes;

        Debug.Assert(_treeNodes.Count == _indexes.Count);
        Debug.Assert(IsEmpty || _root is not null);
    }

    private IndexedTreeNodesFromRoot() : this(true, default, [], [])
    { }

    public int Count
    {
        get
        {
            if (IsEmpty) { return 0; }

            Debug.Assert(_root is not null);
            Debug.Assert(_treeNodes.Count == _indexes.Count);

            return 1 + _treeNodes.Count;
        }
    }

    public IndexedTreeNode<TTreeNode> this[int index]
    {
        get
        {
            Guard.IsInRange(index, 0, Count);

            return index == 0 ?
                new IndexedTreeNode<TTreeNode>(_root) :
                new IndexedTreeNode<TTreeNode>(_treeNodes[index - 1], _indexes[index - 1]);
        }
    }

    public IEnumerator<IndexedTreeNode<TTreeNode>> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool TryAppend
    (
        IndexedTreeNode<TTreeNode> appending,
        [NotNullWhen(true)] out IndexedTreeNodesFromRoot<TTreeNode>? context
    )
    {
        var (node, index) = appending;

        if (node is null) { goto Fail; }

        if (index is null)
        {
            if (!IsEmpty) { goto Fail; }
            context = new(isEmpty: false, root: node, treeNodes: [], indexes: []);
            return true;
        }

        if (IsEmpty) { goto Fail; }

        context = new(isEmpty: false, root: _root, treeNodes: _treeNodes.Add(node), indexes: _indexes.Add(index.Value));
        return true;

    Fail:
        context = default;
        return false;
    }

    public bool TryGetRootTreeNode([NotNullWhen(true)] out TTreeNode? rootTreeNode)
    {
        if (IsEmpty)
        {
            rootTreeNode = default;
            return false;
        }
        else
        {
            rootTreeNode = _root;
            return true;
        }
    }

    public bool TryGetLastTreeNode([NotNullWhen(true)] out TTreeNode? lastTreeNode)
    {
        if (IsEmpty)
        {
            lastTreeNode = default;
            return false;
        }
        else
        {
            lastTreeNode = this[^1].TreeNode;
            return lastTreeNode is not null;
        }
    }

    IEnumerable<int> IHasIndexes.Indexes => _indexes;

    public TTreeNode? Root => _root;
}
