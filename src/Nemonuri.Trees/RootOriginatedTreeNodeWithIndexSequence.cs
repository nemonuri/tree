
using System.Collections;

namespace Nemonuri.Trees;

public class RootOriginatedTreeNodeWithIndexSequence<TTreeNode> : IReadOnlyList<TreeNodeWithIndex<TTreeNode>>
{
    public static RootOriginatedTreeNodeWithIndexSequence<TTreeNode> Empty { get; } = new();

    private readonly TTreeNode? _root;

    [MemberNotNullWhen(false, [nameof(_root)])]
    public bool IsEmpty { get; }

    private readonly ImmutableList<TTreeNode> _treeNodes;
    private readonly ImmutableList<int> _indexes;

    private RootOriginatedTreeNodeWithIndexSequence(bool isEmpty, TTreeNode? root, ImmutableList<TTreeNode> treeNodes, ImmutableList<int> indexes)
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

    private RootOriginatedTreeNodeWithIndexSequence() : this(true, default, [], [])
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

    public TreeNodeWithIndex<TTreeNode> this[int index]
    {
        get
        {
            Guard.IsInRange(index, 0, Count);

            return index == 0 ?
                new TreeNodeWithIndex<TTreeNode>(_root) :
                new TreeNodeWithIndex<TTreeNode>(_treeNodes[index - 1], _indexes[index - 1]);
        }
    }

    public IEnumerator<TreeNodeWithIndex<TTreeNode>> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool TryAppend
    (
        TreeNodeWithIndex<TTreeNode> appending,
        [NotNullWhen(true)] out RootOriginatedTreeNodeWithIndexSequence<TTreeNode>? context
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

    // note
    // - Nemonuri.Trees.Indexes.csproj 이 접근할 수 있도록 하기 위해 이 메서드를 만들었다.
    // - internal 로 선언하고, InternalVisibleTo 특성을 붙이는 것이 더 낫지 않나?
    // - 아니면, IHasImmutableIndexesList 인터페이스를 만들고, 구현한다거나.
    public ImmutableList<int> InternalIndexes => _indexes;
}
